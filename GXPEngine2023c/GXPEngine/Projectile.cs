using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Projectile : AnimationSprite
{
    public int bounceNumber = 0;        // number of bounces until it stops
    public bool useDeltaTime = true;    // true = use deltaTime, false = base on frames only
    public bool hitSomething;

    private int bounceCount = 0;
    private bool stopMoving = false;
    private Vec2 velocity;
    private Vec2 position;
    private Vec2 oldPosition;

    private AnimationSprite sprite;
    

    public Projectile(Vec2 vel, Vec2 pos, string fileName = "circle.png", int cols = 1, int rows = 1, int frames = 1) : base(fileName, cols, rows, frames)
    {
        SetOrigin(width/2, height/2);
        collider.isTrigger = true;
        velocity = vel;
        position = pos;
        x = pos.x;
        y = pos.y;


        if (cols > 1)
        {

            alpha = 0;
            sprite = new AnimationSprite(fileName, cols, rows, frames);
            sprite.SetOrigin(width/2, height/2);
            sprite.collider.isTrigger = true;
            AddChild(sprite);
        }
    }


    protected override Collider createCollider()        // Giving the right hitbox
    {
        EasyDraw BaseShape = new EasyDraw(64, 64, false);
        BaseShape.SetXY(-32, -32);
        BaseShape.Clear(ColorTranslator.FromHtml("#55ff0000"));
        BaseShape.ClearTransparent();
        AddChild(BaseShape);

        return new BoxCollider(BaseShape);
    }


    void Update()
    {
        Step();
    }
    /*
    bool lineEdgeHit;
    float GetEarliestTOI()
    {

        if (velocity.Length() == 0) return 0;
        MyGame myGame = (MyGame)game;
        Level level = myGame.GetLevel();
        //CollisionInfo col = null;
        float earliestTOI = 1f;


        lineEdgeHit = false;
        for (int i = 0; i < level.GetNumberOfPlatforms(); i++)
        {
            // Normal line collision check
            SimplePlatform platform = level.GetPlatform(i);
            Vec2 lineVec = line.end - line.start;
            float tOI = float.NaN;
            Vec2 dist = _oldPosition - line.start;
            Vec2 normal = lineVec.Normal();

            float a = dist.Dot(normal) - radius;
            float b = -velocity.Dot(normal);
            float d = 1f;

            if (b <= 0)
            {
                // moving away from first side, changing values to check for other side
                lineVec *= -1;
                normal = lineVec.Normal();
                a = dist.Dot(normal) - radius;
                b = -velocity.Dot(normal);
                d = -1f;
            }

            if (a >= 0) tOI = a / b;                            // normal time of impact
            else if (a >= -radius) tOI = 0;                     // center not past line yet
            // else nothing happens                             // center past line, TOI stays undefined


            if (tOI <= 1)
            {
                Vec2 POI = _oldPosition + velocity * tOI;
                d *= (POI - line.start).Dot(lineVec.Normalized());  // distance along line

                if (d >= 0 && d <= lineVec.Length())
                {
                    if (tOI < earliestTOI)
                    {
                        earliestTOI = tOI;
                        col = new CollisionInfo(normal, line, earliestTOI);
                        lineEdgeHit = false;
                    }
                }
            }


            // Checking for line edge hits
            Vec2 relPosStart = _oldPosition - line.start;
            Vec2 relPosEnd = _oldPosition - line.end;
            Vec2 relPos = (relPosStart.Length() < relPosEnd.Length()) ? relPosStart : relPosEnd; // only checking closest edge
            float relLen = relPos.Length();
            float tOItwo = float.NaN;
            bool noCol = false;

            float atwo = velocity.Length() * velocity.Length(); // |v|^2
            float btwo = 2 * relPos.Dot(velocity);              // 2(u•v)
            float ctwo = relLen * relLen - radius * radius;     // |u|^2 - (r1+r2)^2 except r2=0

            if (ctwo < 0)                                       // overlapping
            {
                if (btwo < 0) tOItwo = 0;                       // center not past moment of most overlap yet
                else noCol = true;  // NO COLLISION             // center past moment of most overlap
            }
            if (Math.Round(atwo, 5) == 0) noCol = true; // NO COLLISION

            float dtwo = (btwo * btwo) - (4 * atwo * ctwo);     // funny D = b^2 - 4ac formula

            if (dtwo < 0) noCol = true; // NO COLLISION         // 'balls' never collide

            if (tOItwo != 0 && !noCol) tOItwo = (-btwo - Mathf.Sqrt(dtwo)) / (2 * atwo); // the funny (-b +- Sqrt(D))/2a formula

            // Check if TOI is valid, then earliestTOI becomes smallest of the two
            if (tOItwo >= 0 && tOItwo < 1 && !noCol)
            {
                if (tOItwo < earliestTOI)
                {
                    earliestTOI = tOItwo;
                    col = new CollisionInfo(relPos.Normal(), line, earliestTOI);
                    lineEdgeHit = true;
                }
            }


        }

        return col;


        return 0;
    }

    */

    public void Step()
    {
        //rotation++;
        if (!stopMoving)
        {
            hitSomething = false;
            oldPosition = position;

            float deltaTimeClamped = useDeltaTime ? Mathf.Min(Time.deltaTime, 40) : 1000 / 120;
            float deltaTimeFun = (float)deltaTimeClamped / 1000 * 120;

            velocity.y += Constants.gravityProj*deltaTimeFun;

        
            Collision col = MoveUntilCollision(velocity.x * deltaTimeFun, velocity.y * deltaTimeFun);
            //position.x = x;
            //position.y = y;

            if (col != null)
            {
                if (bounceCount < bounceNumber || !useDeltaTime)
                {
                    Vec2 normal = new Vec2(col.normal.x, col.normal.y);
                    velocity.Reflect(normal, Constants.bounciness);
                    bounceCount++;
                }
                else
                {
                    stopMoving = true;
                    velocity = new Vec2(col.normal.x, col.normal.y);
                    x -= col.normal.x*16;
                    y -= col.normal.y*16;
                    Constants.level.SetChildIndex(this, 1);
                }
                hitSomething = true;

            }

        
            //position += velocity * deltaTimeFun;

            /*if (!useDeltaTime)
            {
                Collision collision = MoveUntilCollision(velocity.x * deltaTimeFun, velocity.y * deltaTimeFun);
                position.x = x;
                position.y = y;

                if (collision != null)
                {
                    Console.WriteLine(collision.timeOfImpact);


                }

            }
            else
            {
                position += velocity * deltaTimeFun;

            }*/

            //x += velocity.x * deltaTimeFun;
            //y += velocity.y * deltaTimeFun;

            UpdateScreenPosition();

        }
    }


    void UpdateScreenPosition()
    {
        //x = position.x;
        //y = position.y;

        //Console.WriteLine("Projectile coordinates: "+x+" "+y);

        if (sprite != null)
        {
            sprite.rotation = velocity.GetAngleDegrees();
            sprite.SetCycle(0, 6);
            sprite.Animate(.1f);
            if (stopMoving) sprite.SetFrame(0);
        }

        if (x < 0 || x > 5000 || y < 0 || y > 5000)
        {

            LateDestroy();      //WATCH OUT, NEEDS PROPER COLLISIONS
        }
    }


}