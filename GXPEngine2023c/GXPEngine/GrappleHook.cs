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

    public GrappleHook(Vec2 vel, Vec2 pos, string fileName = "circle.png") : base(vel, pos, fileName)
    {
        
        collider.isTrigger = true;
        player = game.FindObjectOfType<Player>();
        grappleRope = new AnimationSprite("circle.png", 1, 1);  // GRAPPLE ROPE ANIMATION THING
        grappleRope.SetOrigin(width, height / 2);
        grappleRope.collider.isTrigger = true;
        AddChild(grappleRope);

    }


    void Update()
    {

        if (player == null) player = game.FindObjectOfType<Player>();

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
    }

    void GoForIt()
    {
        player.GrappleHit(deltaPos);
        hasTriggered = true;
    }




}