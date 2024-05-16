using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class SceneSwitcher : AnimationSprite
{
    public string nextLevel;
    public bool spawned = true;
    public string buttonID;
    public string plateGroupID;


    public SceneSwitcher(string fileName, int cols, int rows, TiledObject tiledObject = null) : base(fileName, cols, rows)
    {

        nextLevel = tiledObject.GetStringProperty("nextLevel", "map.tmx");
        spawned = tiledObject.GetBoolProperty("spawned", false);
        buttonID = tiledObject.GetStringProperty("buttonID", "");
        plateGroupID = tiledObject.GetStringProperty("plateGroupID", "");
        collider.isTrigger = true;
        Console.WriteLine("AAA");
        Console.WriteLine(spawned);

    }

    void Update()
    {

        checkSpawned();
    }

    public void checkSpawned()
    {
        if (!spawned)
        {
            bool toSpawn = true;

            if (buttonID != null && buttonID != "")
            {
                foreach (Button button in Constants.buttons)
                {
                    if (button.buttonID == buttonID)
                    {
                        if (button.triggered == false)
                        {
                            toSpawn = false;
                        }
                    }
                }
            }
            if (plateGroupID != null && plateGroupID != "")
            {
                foreach (PressurePlate plate in Constants.plates)
                {
                    if (plate.plateGroupID == plateGroupID)
                    {
                        if (plate.completed == false)
                        {
                            toSpawn = false;
                        }
                    }
                }
            }
            if (toSpawn)
            {
                spawned = true;
            }

        }
    }

}