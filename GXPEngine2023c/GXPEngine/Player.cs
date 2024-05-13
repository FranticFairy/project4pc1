using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class Player : AnimationSprite
{

    // Movement
    private float xMaxSpeed = 5f;
    private float jumpSpeed = 7f;           // the force propelling you upwards
    private float gravity = .2f;            // the force pulling you down again
    private float grappleSpeed = 10f;

    // Shooting
    private float minShootForce = 4f;       // min force applied to proj
    private float maxShootForce = 9f;       // max force applied to proj
    private float minShootForceGrapple = 6f;    // min force applied to grapple hook
    private float maxShootForceGrapple = 13f;   // max force applied to grapple hook
    private float minShootMouseDist = 70f;  // moving mouse closer than this value in pixels to player doesn't decrease power anymore
    private float maxShootMouseDist = 300f; // same but the other side

    private int aimTrajectoryAmount = 5;    // amount of white circles for aim trajectory
    private int aimTrajectoryDist = 5;      // amount of frames between the circles

    private bool grappleActive = false;


    public Vec2 position;

    private Level level;
    private Vec2 velocity = new Vec2(0,0);
    private bool isGrounded;

    private int coyoteTime;
    private int coyoteTimeMax = 10;         // number of frames usable for coyote time

    private int goo;


    public Player(Vec2 pos, string fileName = "barry.png", int cols = 7, int rows = 1, TiledObject tiledObject = null) : base(fileName, cols, rows)
    {

        SetOrigin(width / 2, height / 2);
        position = pos;
        scale = 2f;

        level = game.FindObjectOfType<Level>();
        collider.isTrigger = true;

    }

    //int timeTracker;

    public void GrappleHit(Vec2 vec)
    {
        grappleActive = true;
        velocity = vec.Normalized() * grappleSpeed;
        Console.WriteLine(velocity);
    }


    private void MovePlayer()
    {

        int deltaTimeClamped = Mathf.Min(Time.deltaTime, 40);
        float deltaTimeFun = (float)deltaTimeClamped / 1000 * 120;
        //deltaTimeFun = 1f;

        SetCycle(0, 7);

        if (!grappleActive)
        {


            if (Input.GetKey(Key.A))
            {
                velocity.x -= Constants.acceleration*deltaTimeFun;
                Mirror(true, false);
            }
            else if (Input.GetKey(Key.D))
            {
                velocity.x += Constants.acceleration*deltaTimeFun;
                Mirror(false, false);
            }



            if (velocity.x > -Constants.deceleration * deltaTimeFun && 
                velocity.x <  Constants.deceleration * deltaTimeFun) velocity.x = 0;
            if (velocity.x > 0) velocity.x -= Constants.deceleration * deltaTimeFun;
            if (velocity.x < 0) velocity.x += Constants.deceleration * deltaTimeFun;

            velocity.x = Mathf.Clamp(velocity.x, -xMaxSpeed, xMaxSpeed);


            velocity.y += gravity*deltaTimeFun;
            isGrounded = false;

            //Collision collision = MoveUntilCollision(vx, 0);    
            if (MoveUntilCollision(0, velocity.y*deltaTimeFun) != null)
            {
                velocity.y = 0;
                isGrounded = true;
                coyoteTime = coyoteTimeMax;
                //if (timeTracker > 0) Console.WriteLine("timeTracker: "+timeTracker);
                //timeTracker = 0;
            }
            //else timeTracker += Time.deltaTime;

            if (!isGrounded && coyoteTime > 0) coyoteTime--;

            if (Input.GetKeyDown(Key.W) && coyoteTime > 0)
            {
                velocity.y = -jumpSpeed;
                coyoteTime = 0;

            }

            position += velocity*deltaTimeFun;
            x = position.x; 
            y = position.y;
        }
        else
        {
            Collision col = MoveUntilCollision(velocity.x*deltaTimeFun, velocity.y*deltaTimeFun);
            position.x = x;
            position.y = y;
            Console.WriteLine("col = "+col);
            if (col != null || Input.GetKeyDown(Key.SPACE))
            {
                GrappleHook grappleHook = game.FindObjectOfType<GrappleHook>();
                if (grappleHook != null) grappleHook.Destroy();
                grappleActive = false;
            }
        }


        //Console.WriteLine("Player coordinates: " + position);

        Animate(.1f);
    }

    private Vec2 getProjVec(Level l, string projType)
    {
        Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);
        Vec2 levelPos = new Vec2(l.x, l.y);
        Vec2 deltaPos = mousePos - (position + levelPos);

        float shootPower = Mathf.Clamp(deltaPos.Length(), minShootMouseDist, maxShootMouseDist);
        shootPower -= minShootMouseDist;
        if (projType == "bounce")
        {
            shootPower *= (maxShootForce - minShootForce) / maxShootMouseDist;
            shootPower += minShootForce;
        }
        else if (projType == "grapple")
        {
            shootPower *= (maxShootForceGrapple - minShootForceGrapple) / maxShootMouseDist;
            shootPower += minShootForceGrapple;
        }
        else return new Vec2(0, 0);

        return deltaPos.Normalized() * shootPower;
    }

    void Shooting()
    {
        level = game.FindObjectOfType<Level>();

        AimTrajectory[] foundAimTraj = game.FindObjectsOfType<AimTrajectory>();
        foreach (AimTrajectory aimTrajectory in foundAimTraj)
        {
            aimTrajectory.LateDestroy();
        }

        // The input part
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            string projType = Input.GetMouseButton(0) ? "bounce" : "grapple";
            for (int i = 0; i < aimTrajectoryAmount; i++)
            {
                level.AddChild(new AimTrajectory(getProjVec(level, projType), position));
                AimTrajectory[] foundAimTrajec = game.FindObjectsOfType<AimTrajectory>();
                foreach (AimTrajectory aimTrajec in foundAimTrajec)
                {
                    for (int j = 0; j < aimTrajectoryDist; j++)
                    {
                        aimTrajec.useDeltaTime = false;
                        aimTrajec.Step();
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            level.AddChild(new BouncyProjectile(getProjVec(level, "bounce"), position));
        }
        else if (Input.GetMouseButtonUp(1))
        {
            level.AddChild(new GrappleHook(getProjVec(level, "grapple"), position));
        }

    }



    int counter;

    void Update()
    {

        counter++;

        if (!Input.GetKey(Key.O) || counter >= 60) {

            Constants.goo = goo;
            MovePlayer();
            Shooting();
            counter = 0;
            checkCollision();
        }

    }

    void checkCollision()
    {
        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i].GetType() == typeof(Collectable))
            {
                goo++;
                collisions[i].LateDestroy();
            }
            if (collisions[i].GetType() == typeof(Killer))
            {
                Constants.dead = true;
            }
        }
    }



}