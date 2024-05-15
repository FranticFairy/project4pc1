using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Door : AnimationSprite
    {
        //The OG Gatekeepers
        //Doors block movement until they are OPENED
        //Doors are OPENED by the completion of puzzles! Shocker.
        //Ideally, they get a funky animation to make them open but like.
        //That comes later.

        public bool opened = false;

        public Door(float xPos, float yPos, string fileName = "danger.png", int cols = 1, int rows = 1) : base(fileName, cols, rows)
        {
            SetOrigin(width / 2, height / 2);
            SetXY(xPos, yPos);
        }

        public void setOpen()
        {
            opened = true;
            collider.isTrigger = true; //This should make it pass-through
        }

    }
}
