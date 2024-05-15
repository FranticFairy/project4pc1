using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Box: AnimationSprite
    {
        //Unfortunately, not with legs.
        //Boxes are obstacles. You cannot walk through boxes.
        //You can push boxes, though.
        //You can also tether boxes.
        //Boxes kill you if they fall on you.
        //Boxes trigger pressure plates, but not buttons, but that's their issue.
        //The box simply boxes.
        //Also projectiles bounce off of boxes.

        public Box(float xPos, float yPos, string fileName = "danger.png", int cols = 1, int rows = 1) : base(fileName, cols, rows)
        {
            SetOrigin(width / 2, height / 2);
            SetXY(xPos, yPos);
        }
    }
}
