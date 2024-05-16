using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GrappleHook : Projectile
{
    private bool hasTriggered = false;
    private bool attachedToMovable = false;
    
    AnimationSprite grappleRope;
    Player player;

    Vec2 ownPos;
    Vec2 playerPos;
    public Vec2 deltaPos;
    Vec2 parentPos;
    FMODSoundSystem soundSystem;
    private IntPtr grapplePullSound;

    public GrappleHook(Vec2 vel, Vec2 pos, string fileName = "projectile-animation-spritesheet.png", int cols = 3, int rows = 2, int frames = 6) : base(vel, pos, fileName, cols, rows, frames)
    {
        soundSystem = Constants.soundSystem;
        collider.isTrigger = true;
        player = Constants.player;
        grappleRope = new AnimationSprite("grappling-slime-animation-spritesheet-3.png", 4, 3, 10);  // GRAPPLE ROPE ANIMATION THING
        grappleRope.SetOrigin(width, height / 2);
        grappleRope.collider.isTrigger = true;
        AddChild(grappleRope);
        SetChildIndex(grappleRope, 0);
        GrappleRopeStuff();

        grapplePullSound = soundSystem.LoadSound("audio/Grappling_Pull.mp3", true);
    }


    void Update()
    {
        /*if (Input.GetKeyDown(Key.Y)) Constants.soundSystem.StopChannel(12);
        if (Constants.soundSystem.ChannelIsPlaying(12))
        {
            Console.WriteLine("true");
        }
        else
        {
            Console.WriteLine("false");
        }*/
        //if (player == null) player = game.FindObjectOfType<Player>();

        if (!hasTriggered) Step();

        ownPos = new Vec2(x, y);
        playerPos = new Vec2(player.x, player.y);
        if (!attachedToMovable)
        {
            deltaPos = ownPos - playerPos;
        }
        else
        {
            if (parent != null) parentPos = new Vec2(parent.x, parent.y);
            deltaPos = parentPos + ownPos - playerPos;
        }

        GrappleRopeStuff();

        if (!hasTriggered)
        {
            if (hitSomething)
            {
                //Constants.soundSystem.PlaySound(grapplePullSound, 12, true, Constants.sound12Volume, 0);
                //Constants.soundSystem.SetChannelPaused(12, false);

                if (collision != null)
                {
                    collision.other.SetChildIndex(this, 0);
                    x -= collision.other.x; 
                    y -= collision.other.y;
                    ownPos = new Vec2(x, y);
                    parentPos = new Vec2(parent.x, parent.y);
                    deltaPos = ownPos + parentPos - playerPos;
                    attachedToMovable = true;
                    hasTriggered = true;
                }
                else
                {
                    GoForIt();
                }
                
            }
        }
    }

    void GrappleRopeStuff()
    {
        float dist = deltaPos.Length();
        grappleRope.width = Mathf.Round(dist);
        grappleRope.rotation = deltaPos.GetAngleDegrees();
        grappleRope.SetCycle(0, 10);
        if (grappleRope.currentFrame < 9) grappleRope.Animate(.2f);
    }

    void GoForIt()
    {
        player.GrappleHit(deltaPos);
        hasTriggered = true;
    }




}