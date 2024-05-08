using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Collectable : AnimationSprite
    {

        private int value;
        public float xPos;
        public float yPos;

        public Button linkedButton;
        public bool spawned;

        public Collectable(bool spawned, float xPos, float yPos, string fileName = "goo.png", int cols = 1, int rows = 1) : base(fileName, cols, rows)
        {

            SetOrigin(width / 2, height / 2);
            SetXY(xPos, yPos);
            collider.isTrigger = true;

        }

        public int collect()
        {
            return value;
        }
    }
}
