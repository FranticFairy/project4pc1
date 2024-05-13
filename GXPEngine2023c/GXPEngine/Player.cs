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

    private bool grappleActive = false;
    private bool grappleAirborne = false;
    public bool grappleIsShot = false;

    private Level level;
    private Vec2 velocity = new Vec2(0,0);
    private bool isGrounded;

    private int coyoteTime;
    private int coyoteTimeMax = 10;         // number of frames usable for coyote time

    private int goo;


    public Player(Vec2 pos, string fileName = "barry.png", int cols = 7, int rows = 1, TiledObject tiledObject = null) : base(fileName, cols, rows)
    {

        SetOrigin(width / 2, height / 2);
        Constants.positionPlayer = pos;
        scale = 2f;

        level = Constants.level;
        collider.isTrigger = true;

    }

    //int timeTracker;

    public void GrappleHit(Vec2 vec)
    {
        grappleActive = true;
        grappleAirborne = true;
        velocity = vec.Normalized() * Constants.grappleSpeed;
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

            // Inputs
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


            // Deceleration
            if (!grappleAirborne)
            {
                if (velocity.x > -Constants.deceleration * deltaTimeFun && 
                    velocity.x <  Constants.deceleration * deltaTimeFun) velocity.x = 0;
                if (velocity.x > 0) velocity.x -= Constants.deceleration * deltaTimeFun;
                if (velocity.x < 0) velocity.x += Constants.deceleration * deltaTimeFun;
            }

            velocity.x = Mathf.Clamp(velocity.x, -Constants.xMaxSpeedPlayer, Constants.xMaxSpeedPlayer);


            velocity.y += Constants.gravityPlayer *deltaTimeFun;
            isGrounded = false;

            //Collision collision = MoveUntilCollision(vx, 0);    
            if (MoveUntilCollision(0, velocity.y*deltaTimeFun) != null)
            {
                velocity.y = 0;
                isGrounded = true;
                grappleAirborne = false;
                coyoteTime = coyoteTimeMax;

            }
            if (MoveUntilCollision(velocity.x*deltaTimeFun, 0) != null)
            {
                velocity.x = 0;
            }


            if (!isGrounded && coyoteTime > 0) coyoteTime--;

            if (Input.GetKeyDown(Key.W) && coyoteTime > 0)
            {
                velocity.y = -Constants.jumpSpeedPlayer;
                coyoteTime = 0;

            }

            Constants.positionPlayer += velocity*deltaTimeFun;
            x = Constants.positionPlayer.x; 
            y = Constants.positionPlayer.y;
        }
        else
        {
            Collision col = MoveUntilCollision(velocity.x*deltaTimeFun, velocity.y*deltaTimeFun);
            Constants.positionPlayer.x = x;
            Constants.positionPlayer.y = y;
            
            if (col != null || Input.GetKeyDown(Key.SPACE))
            {
                GrappleHook grappleHook = game.FindObjectOfType<GrappleHook>();
                if (grappleHook != null) grappleHook.Destroy();
                grappleActive = false;
                grappleIsShot = false;
            }
        }


        Animate(.1f);
    }

    private Vec2 getProjVec(string projType)
    {
        Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);
        Vec2 levelPos = new Vec2(Constants.level.x, Constants.level.y);
        Vec2 deltaPos = mousePos - (Constants.positionPlayer + levelPos);

        float shootPower = Mathf.Clamp(deltaPos.Length(), Constants.minShootMouseDist, Constants.maxShootMouseDist);
        shootPower -= Constants.minShootMouseDist;
        if (projType == "bounce")
        {
            shootPower *= (Constants.maxShootForce - Constants.minShootForce) / Constants.maxShootMouseDist;
            shootPower += Constants.minShootForce;
        }
        else if (projType == "grapple")
        {
            shootPower *= (Constants.maxShootForceGrapple - Constants.minShootForceGrapple) / Constants.maxShootMouseDist;
            shootPower += Constants.minShootForceGrapple;
        }
        else return new Vec2(0, 0);

        return deltaPos.Normalized() * shootPower;
    }

    void Shooting()
    {

        level = Constants.level;
        
        AimTrajectory[] foundAimTraj = game.FindObjectsOfType<AimTrajectory>();
        foreach (AimTrajectory aimTrajectory in foundAimTraj)
        {
            aimTrajectory.LateDestroy();
        }
        if (game.FindObjectOfType<GrappleHook>() == null && !grappleActive) grappleIsShot = false;

        // The input part
        if (!grappleIsShot)
        {
            if ((Input.GetMouseButton(0) || Input.GetMouseButton(1)))
            {
                string projType = Input.GetMouseButton(0) ? "bounce" : "grapple";
                for (int i = 0; i < Constants.aimTrajectoryAmount; i++)
                {
                    level.AddChild(new AimTrajectory(getProjVec(projType), Constants.positionPlayer));
                    AimTrajectory[] foundAimTrajec = game.FindObjectsOfType<AimTrajectory>();
                    foreach (AimTrajectory aimTrajec in foundAimTrajec)
                    {
                        for (int j = 0; j < Constants.aimTrajectoryDist; j++)
                        {
                            aimTrajec.useDeltaTime = false;
                            aimTrajec.Step();
                        }
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                level.AddChild(new BouncyProjectile(getProjVec("bounce"), Constants.positionPlayer));
            }
            else if (Input.GetMouseButtonUp(1))
            {
                level.AddChild(new GrappleHook(getProjVec("grapple"), Constants.positionPlayer));
                grappleIsShot = true;
            }
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