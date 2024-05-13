using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BouncyProjectile : Projectile
{


    public BouncyProjectile(Vec2 vel, Vec2 pos, string fileName = "circle.png") : base(vel, pos, fileName)
    {
        bounceNumber = 3;
        collider.isTrigger = true;

    }
    
    void Update()
    {
        Step();
    }
    
}