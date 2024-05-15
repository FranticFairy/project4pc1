using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class PressurePlate : AnimationSprite
{
    //Pressure plates should be able to be triggered by BOXES and PLAYERS
    //Two states; Triggered and Completed
    //Triggered is active so long as something is on top of the Pressure Plate
    //Completed is set once the condition has been met to keep the Pressure Plate down
    //Example; There are two pressure plates, a box, and a door. The moment both Pressure Plates are Triggered, they are set as Completed, and the door is opened.
    public bool triggered = false;
    public bool completed = false;
    public string plateGroupID;

    //When triggered, run a funciton to check all other pressureplates with the same groupID, if they are all triggered, set all to Complete

    public PressurePlate(string imageFile, int columns, int rows, TiledObject tiledObject = null) : base(imageFile, columns, rows)
    {
        SetOrigin(width / 2, height / 2);
        collider.isTrigger = true;

        plateGroupID = tiledObject.GetStringProperty("plateGroupID", "");
        Constants.plates.Add(this);
    }

    public void setComplete()
    {
        completed = true;
    }

    public void checkToggle()
    {
        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i].GetType() == typeof(Player) || collisions[i].GetType() == typeof(Movable))
            {


                triggered = true;

                bool allTriggered = true;

                foreach(PressurePlate plate in Constants.plates)
                {
                    if(plate.plateGroupID == plateGroupID)
                    {
                        if(!plate.triggered)
                        {
                            Console.WriteLine("Not Triggered!");
                            allTriggered = false;
                        }
                    }
                }

                if(allTriggered)
                {
                    completed = true;
                    foreach (PressurePlate plate in Constants.plates)
                    {
                        if (plate.plateGroupID == plateGroupID)
                        {
                            plate.completed = true;
                        }
                    }
                }

            } else
            {
                triggered = false;
            }
        }
    }

}