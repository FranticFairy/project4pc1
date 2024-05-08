using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Button : AnimationSprite
    {
        public float xPos;
        public float yPos;
        public bool triggered = false;

        public Button(float xPos, float yPos, string fileName = "button.png", int cols = 1, int rows = 1) : base(fileName, cols, rows)
        {
            SetOrigin(width / 2, height / 2);
            SetXY(xPos, yPos);
            collider.isTrigger = true;

        }

        public void checkToggle()
        {
            GameObject[] collisions = GetCollisions();
            for (int i = 0; i < collisions.Length; i++)
            {
                if (collisions[i].GetType() == typeof(Projectile))
                {
                    triggered = true;
                    int index = Constants.buttons.FindIndex(a => a == this);
                    Constants.buttonStates[index] = true;
                }
            }
        }

        public void addToConstants()
        {
            Constants.buttons.Add(this);
            Constants.buttonStates.Add(false);
        }

    }
}
