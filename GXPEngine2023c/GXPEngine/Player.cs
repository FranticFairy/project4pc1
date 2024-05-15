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

    private TiledLevel level;
    private Vec2 velocity = new Vec2(0,0);
    private bool isGrounded;

    private int coyoteTime;
    private int coyoteTimeMax = 10;         // number of frames usable for coyote time

    private int goo;

    private float deltaTimeFun;

    // ANIMATIONS
    private bool animShooting;
    private bool animInAir;
    private bool animWalking;

    private AnimationSprite animLegs;
    private AnimationSprite animGoo;
    private AnimationSprite animBody;

    private string lastAnim;
    private string lastInput;

    public Player(string fileName = "empty.png", int cols = 7, int rows = 1, TiledObject tiledObject = null) : base(fileName, cols, rows)
    {
        Console.WriteLine("MakePlayer");
        alpha = 0;

        SetOrigin(width / 2, height / 2);
        //Constants.positionPlayer = pos;
        //scale = 2f;
        alpha = 0;

        level = Constants.level;
        collider.isTrigger = true;

        animLegs = new AnimationSprite("circle.png", 1, 1);
        animGoo = new AnimationSprite("circle.png", 1, 1);
        animBody = new AnimationSprite("circle.png", 1, 1);

        animLegs.collider.isTrigger = true;
        animGoo.collider.isTrigger= true;
        animBody.collider.isTrigger = true;

        animLegs.SetOrigin(animLegs.width / 2, animLegs.height / 2);
        animGoo.SetOrigin(animGoo.width / 2, animGoo.height / 2);
        animBody.SetOrigin(animBody.width / 2, animBody.height / 2);

        AddChild(animLegs);
        AddChild(animGoo);
        AddChild(animBody);

    }

    protected override Collider createCollider()    // Custom hitbox THIS MIGHT SCREW THINGS UP
    {
        // set to half that width and height
        EasyDraw BaseShape = new EasyDraw(256, 128, false); // width and height of hitbox
        BaseShape.SetXY(-128, -64);                         // set to half that width and height
        BaseShape.Clear(ColorTranslator.FromHtml("#55ff0000"));
        //BaseShape.ClearTransparent();     // Comment this out to see custom hitbox, uncomment to hide
        AddChild(BaseShape);

        return new BoxCollider(BaseShape);
    }

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
        deltaTimeFun = (float)deltaTimeClamped / 1000 * 120;
        //deltaTimeFun = 1f;

        //SetCycle(0, 7);

        if (!grappleActive)
        {

            // Inputs
            if (Input.GetKey(Key.A))
            {
                velocity.x -= Constants.acceleration*deltaTimeFun;
                Mirror(true, false);
                animWalking = true; // ANIMATION WALKING
                lastInput = "left";
            }
            else if (Input.GetKey(Key.D))
            {
                velocity.x += Constants.acceleration*deltaTimeFun;
                Mirror(false, false);               
                animWalking = true; // ANIMATION WALKING
                lastInput = "right";
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
 
            if (MoveUntilCollision(0, velocity.y * deltaTimeFun) != null)
            {
                velocity.y = 0;
                isGrounded = true;
                grappleAirborne = false;
                coyoteTime = coyoteTimeMax;
            }
            else animInAir = true;  // ANIMATION IN AIR
            Collision col = MoveUntilCollision(velocity.x * deltaTimeFun, 0);
            if (col != null)
            {
                if (col.other.GetType() == typeof(Movable))
                {
                    col.other.MoveUntilCollision(velocity.x * deltaTimeFun, 0);
                }

                float aaa = y + 64 - col.other.y;
                if (aaa < .150f && aaa > 0) y -= aaa;
                velocity.x = 0;
            }


            if (!isGrounded && coyoteTime > 0) coyoteTime--;

            if (Input.GetKeyDown(Key.W) && coyoteTime > 0)
            {
                velocity.y = -Constants.jumpSpeedPlayer;
                coyoteTime = 0;
                animInAir = true; // ANIMATION IN AIR
            }

            Constants.positionPlayer += velocity*deltaTimeFun;
            x = Constants.positionPlayer.x; 
            if (!isGrounded) y = Constants.positionPlayer.y;
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
            animInAir = true;
        }


        //Animate(.1f);
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
                animShooting = true;    // ANIMATION SHOOTING
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
                Constants.goo -= Constants.shotCost; //Use up goo when firing.
                BouncyProjectile[] foundProjs = game.FindObjectsOfType<BouncyProjectile>();
                if (foundProjs.Length > Constants.bouncyProjLimit)  // limiting proj number
                {
                    foundProjs[0].LateDestroy();
                }

            }
            else if (Input.GetMouseButtonUp(1))
            {
                level.AddChild(new GrappleHook(getProjVec("grapple"), Constants.positionPlayer));
                grappleIsShot = true;
                Constants.goo -= Constants.hookCost; //Use up goo when firing.
                level.SetChildIndex(this, 999); // Making sure the grapple rope is behind player
            }
        }

    }

    void Animation()
    {

        if (animShooting)
        {
            if (lastAnim != "shooting")
            {
                animLegs.Destroy();
                animGoo.Destroy();
                animBody.Destroy();
                
                animLegs = new AnimationSprite("character-shooting-leg-animation-spritesheet.png", 4, 3, 11, true);
                animGoo = new AnimationSprite("character-idle-slime-animation-spritesheet.png", 4, 4, 16, true);
                animBody = new AnimationSprite("character-shooting-animation-spritesheet.png", 4, 3, 11, true);

                animLegs.collider.isTrigger = true;
                animGoo.collider.isTrigger = true;
                animBody.collider.isTrigger = true;

                animLegs.SetOrigin(animLegs.width / 2, animLegs.height / 2);
                animGoo.SetOrigin(animGoo.width / 2, animGoo.height / 2);
                animBody.SetOrigin(animBody.width / 2, animBody.height / 2);

                AddChild(animLegs);
                AddChild(animGoo);
                AddChild(animBody);
            }
            animLegs.SetCycle(0, 11);
            animGoo.SetCycle(0, 16);
            animBody.SetCycle(0, 11);

            if (animLegs.currentFrame != animLegs.frameCount-1) animLegs.Animate(Constants.animPlayerShootingSpd * deltaTimeFun);
            animGoo.Animate(Constants.animPlayerShootingSpd * deltaTimeFun);
            if (animBody.currentFrame != animBody.frameCount-1) animBody.Animate(Constants.animPlayerShootingSpd * deltaTimeFun);
            lastAnim = "shooting";
        }
        else if (animInAir)
        {
            if (lastAnim != "inair")
            {
                animLegs.Destroy();
                animGoo.Destroy();
                animBody.Destroy();

                animLegs = new AnimationSprite("character-shooting-leg-animation-spritesheet.png", 4, 3, 12, true);
                animGoo = new AnimationSprite("character-idle-slime-animation-spritesheet.png", 4, 4, 16, true);
                animBody = new AnimationSprite("character-in-air-animation-spritesheet.png", 2, 2, 4, true);

                animLegs.collider.isTrigger = true;
                animGoo.collider.isTrigger = true;
                animBody.collider.isTrigger = true;

                animLegs.SetOrigin(animLegs.width / 2, animLegs.height / 2);
                animGoo.SetOrigin(animGoo.width / 2, animGoo.height / 2);
                animBody.SetOrigin(animBody.width / 2, animBody.height / 2);

                AddChild(animLegs);
                AddChild(animGoo);
                AddChild(animBody);


            }
            //animLegs.SetCycle(0, 11);
            animGoo.SetCycle(0, 16);
            animBody.SetCycle(0, 4);

            animLegs.currentFrame = 11; //animLegs.Animate(.5f);
            animGoo.Animate(Constants.animPlayerInAirSpd * deltaTimeFun);
            animBody.Animate(Constants.animPlayerInAirSpd * deltaTimeFun);

            lastAnim = "inair";
        }
        else if (animWalking)
        {
            if (lastAnim != "walking")
            {
                animLegs.Destroy();
                animGoo.Destroy();
                animBody.Destroy();

                animLegs = new AnimationSprite("character-walking-leg-animation-spritesheet.png", 5, 4, 17, true);
                animGoo = new AnimationSprite("character-walking-slime-animation-spritesheet.png", 5, 4, 17, true);
                animBody = new AnimationSprite("character-walking-animation-spritesheet.png", 5, 4, 17, true);

                animLegs.collider.isTrigger = true;
                animGoo.collider.isTrigger = true;
                animBody.collider.isTrigger = true;

                animLegs.SetOrigin(animLegs.width / 2, animLegs.height / 2);
                animGoo.SetOrigin(animGoo.width / 2, animGoo.height / 2);
                animBody.SetOrigin(animBody.width / 2, animBody.height / 2);

                AddChild(animLegs);
                AddChild(animGoo);
                AddChild(animBody);


            }
            animLegs.SetCycle(0, 17);
            animGoo.SetCycle(0, 17);
            animBody.SetCycle(0, 17);

            animLegs.Animate(Constants.animPlayerWalkingSpd * deltaTimeFun);
            animGoo.Animate(Constants.animPlayerWalkingSpd * deltaTimeFun);
            animBody.Animate(Constants.animPlayerWalkingSpd * deltaTimeFun);
            lastAnim = "walking";
        }
        else
        {
            if (lastAnim != "idle")
            {
                animLegs.Destroy();
                animGoo.Destroy();
                animBody.Destroy();

                animLegs = new AnimationSprite("character-idle-leg-animation-spritesheet.png", 4, 4, 16, true);
                animGoo = new AnimationSprite("character-idle-slime-animation-spritesheet.png", 4, 4, 16, true);
                animBody = new AnimationSprite("character-idle-animation-spritesheet.png", 4, 4, 16, true);

                animLegs.collider.isTrigger = true;
                animGoo.collider.isTrigger = true;
                animBody.collider.isTrigger = true;

                animLegs.SetOrigin(animLegs.width / 2, animLegs.height / 2);
                animGoo.SetOrigin(animGoo.width / 2, animGoo.height / 2);
                animBody.SetOrigin(animBody.width / 2, animBody.height / 2);

                AddChild(animLegs);
                AddChild(animGoo);
                AddChild(animBody);


            }
            animLegs.SetCycle(0, 16);
            animGoo.SetCycle(0, 16);
            animBody.SetCycle(0, 16);

            animLegs.Animate(Constants.animPlayerIdleSpd * deltaTimeFun);
            animGoo.Animate(Constants.animPlayerIdleSpd * deltaTimeFun);
            animBody.Animate(Constants.animPlayerIdleSpd * deltaTimeFun);
            lastAnim = "idle";
        }


        if (lastInput == "left")
        {
            animLegs.Mirror(true, false);
            animGoo.Mirror(true, false);
            animBody.Mirror(true, false);
        }
        else if (lastInput == "right")
        {
            animLegs.Mirror(false, false);
            animGoo.Mirror(false, false);
            animBody.Mirror(false, false);
        }

        animShooting = false;
        animInAir = false;
        animWalking = false;
    }

    int counter;

    void Update()
    {

        counter++;

        if (!Input.GetKey(Key.O) || counter >= 60) {

            MovePlayer();
            Shooting();
            counter = 0;
            checkCollision();
            Animation();
            if(Constants.goo < 1)
            {
                Constants.dead = true;
            }
        }

    }

    void checkCollision()
    {
        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++)
        {

            if (collisions[i].GetType() == typeof(Killer))
            {
                Constants.dead = true;
            }
        }
    }



}