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
    private Vec2 velocity = new Vec2(0,0);


    private float xMaxSpeed = 5f;


    private float minShootForce = 4f;       // min force applied to proj
    private float maxShootForce = 9f;       // max force applied to proj
    private float minShootMouseDist = 70f;  // moving mouse closer than this value in pixels to player doesn't decrease power anymore
    private float maxShootMouseDist = 300f; // same but the other side

    private int aimTrajectoryAmount = 5;    // amount of white circles for aim trajectory
    private int aimTrajectoryDist = 5;      // amount of frames between the circles


    private float jumpSpeed = 7f;           // the force propelling you upwards
    private float gravity = .2f;            // the force pulling you down again

    private Level level;

    public Vec2 position;

    private bool isGrounded;
    public bool isSprinting;

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
    private void MovePlayer()
    {

        int deltaTimeClamped = Mathf.Min(Time.deltaTime, 40);
        float deltaTimeFun = (float)deltaTimeClamped / 1000 * 120;
        //deltaTimeFun = 1f;

        SetCycle(0, 7);


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

        //Console.WriteLine("Player coordinates: " + position);

        Animate(.1f);
    }

    private Vec2 getProjVec(Level l)
    {
        Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);
        Vec2 levelPos = new Vec2(l.x, l.y);
        Vec2 deltaPos = mousePos - (position + levelPos);

        float shootPower = Mathf.Clamp(deltaPos.Length(), minShootMouseDist, maxShootMouseDist);
        shootPower -= minShootMouseDist;
        shootPower *= (maxShootForce - minShootForce) / maxShootMouseDist;
        shootPower += minShootForce;

        /*Console.WriteLine("deltaPos: "+deltaPos);
        /*Console.WriteLine("shoot");
        Console.WriteLine(" ");
        Console.WriteLine("mousePos = "+mousePos);
        Console.WriteLine("position = "+position);
        Console.WriteLine("deltaPos = "+deltaPos);
        Console.WriteLine("level pos = "+level.x+" "+level.y);
        Console.WriteLine("shootPower = "+shootPower);
        Console.WriteLine("projVec = "+projVec);
        Console.WriteLine(" ");*/

        return deltaPos.Normalized() * shootPower;
    }

    void Shooting()
    {
        /*Console.WriteLine("Target FPS: "+game.targetFps);
        Console.WriteLine("Current FPS: " + game.currentFps);*/
        level = game.FindObjectOfType<Level>();
        AimTrajectory[] foundAimTraj = game.FindObjectsOfType<AimTrajectory>();
        foreach (AimTrajectory aimTrajectory in foundAimTraj)
        {
            aimTrajectory.LateDestroy();
        }

        // I AM REPEATING MYSELF HERE. I REPEAT, I AM REPEATING MYSELF HERE
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            for (int i = 0; i < aimTrajectoryAmount; i++)
            {
                level.AddChild(new AimTrajectory(getProjVec(level), position));
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
            level.AddChild(new BouncyProjectile(getProjVec(level), position));
        }
        else if (Input.GetMouseButtonUp(1))
        {
            level.AddChild(new Projectile(getProjVec(level), position));
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