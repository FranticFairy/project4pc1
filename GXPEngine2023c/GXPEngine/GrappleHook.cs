using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GrappleHook : Projectile
{
    private bool hasTriggered = false;
    
    AnimationSprite grappleRope;
    Player player;

    Vec2 ownPos;
    Vec2 playerPos;
    Vec2 deltaPos;

    public GrappleHook(Vec2 vel, Vec2 pos, string fileName = "projectile-animation-spritesheet.png", int cols = 3, int rows = 2, int frames = 6) : base(vel, pos, fileName, cols, rows, frames)
    {
        
        collider.isTrigger = true;
        player = Constants.player;
        grappleRope = new AnimationSprite("grappling-slime-animation-spritesheet-3.png", 4, 3, 10);  // GRAPPLE ROPE ANIMATION THING
        grappleRope.SetOrigin(width, height / 2);
        grappleRope.collider.isTrigger = true;
        AddChild(grappleRope);
        GrappleRopeStuff();
    }


    void Update()
    {

        //if (player == null) player = game.FindObjectOfType<Player>();

        Step();

        ownPos = new Vec2(x, y);
        playerPos = new Vec2(player.x, player.y);
        deltaPos = ownPos - playerPos;

        GrappleRopeStuff();

        if (!hasTriggered)
        {
            if (hitSomething)
            {
                GoForIt();
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