using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class PressurePlate : AnimationSprite
    {
        //Pressure plates should be able to be triggered by BOXES and PLAYERS
        //Two states; Triggered and Completed
        //Triggered is active so long as something is on top of the Pressure Plate
        //Completed is set once the condition has been met to keep the Pressure Plate down
        //Example; There are two pressure plates, a box, and a door. The moment both Pressure Plates are Triggered, they are set as Completed, and the door is opened.
        public bool triggered = false;
        public bool completed = false;
        public int groupID;

        //When triggered, run a funciton to check all other pressureplates with the same groupID, if they are all triggered, set all to Complete

        public PressurePlate(float xPos, float yPos, int group, string fileName = "button.png", int cols = 1, int rows = 1) : base(fileName, cols, rows)
        {
            SetOrigin(width / 2, height / 2);
            SetXY(xPos, yPos);
            collider.isTrigger = true;
            this.groupID = group;
        }

        public void setComplete()
        {
            completed = true;
        }

    }
}
