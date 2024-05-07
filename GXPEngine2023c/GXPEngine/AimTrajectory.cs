using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AimTrajectory : Projectile
{

    public AimTrajectory(Vec2 vel, Vec2 pos, string fileName = "triangle.png") : base(vel, pos, fileName)
    {
        collider.isTrigger = true;
    }


}
