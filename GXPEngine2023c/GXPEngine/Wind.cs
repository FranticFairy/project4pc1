using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class Wind : AnimationSprite
{
    private Vec2 windDirection;
    private Projectile projectile;
    public Wind(string fileName = "colors.png", int cols = 1, int rows = 1, TiledObject tiledObject = null) : base(fileName, cols, rows)
    {
        collider.isTrigger = true;
        //scale = 2f;
        //alpha = .1f;
        SetOrigin(width / 2, height / 2);
        windDirection = new Vec2(1, 0);

    }


    void Update()
    {
        GameObject[] collisions = GetCollisions();
        /*foreach (GameObject p in collisions)
        {
            if (p.wind = windDirection;
        }*/
        
        for (int i = 0; i < collisions.Length; i++)
        {

            if (collisions[i].GetType() == typeof(GrappleHook) || collisions[i].GetType() == typeof(BouncyProjectile))
            {
                projectile = (Projectile)collisions[i];
                projectile.wind = windDirection;
            }
        }

    }

}