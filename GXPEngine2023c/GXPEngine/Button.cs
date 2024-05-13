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

        //Buttons should be able to be triggered by PROJECTILES and PLAYERS
        //Buttons are either Toggled or not
        //Buttons are toggled the moment something comes in contact with them
        //Projectiles stick to a button the moment it is triggered.

        public float xPos;
        public float yPos;
        public bool triggered = false;

        public Button(string fileName = "button.png", int cols = 1, int rows = 1) : base(fileName, cols, rows)
        {
            SetOrigin(width / 2, height / 2);
            //SetXY(xPos, yPos);
            collider.isTrigger = true;

        }

        public void checkToggle()
        {
            GameObject[] collisions = GetCollisions();
            for (int i = 0; i < collisions.Length; i++)
            {
                if (collisions[i].GetType() == typeof(Player) || collisions[i].GetType() == typeof(Projectile))
                {
                    triggered = true;
                    int index = Constants.buttons.FindIndex(a => a == this);
                    Constants.buttonStates[index] = true;

                    if(collisions[i].GetType() == typeof(Projectile))
                    {
                        //Here we will handle stopping projectiles
                    }
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
