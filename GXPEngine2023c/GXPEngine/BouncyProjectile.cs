using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BouncyProjectile : Projectile
{


    public BouncyProjectile(Vec2 vel, Vec2 pos, string fileName = "projectile-animation-spritesheet.png", int cols = 3, int rows = 2, int frames = 6) : base(vel, pos, fileName, cols, rows, frames)
    {
        bounceNumber = 3;
        collider.isTrigger = true;

    }
    
    void Update()
    {
        Step();
    }
    
}