using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;
using System.Runtime.CompilerServices;

public class Collectable : AnimationSprite
{
    //Collectables are goo pickups. You touch them to get more Goo. Simple as that.

    public int value;
    public float xPos;
    public float yPos;


    public bool spawned = true;
    public string buttonID;
    public string plateGroupID;

    private IntPtr absorbSound;

    public Collectable(string imageFile, int columns, int rows, TiledObject tiledObject = null) : base(imageFile, columns, rows)
    {

        SetOrigin(width / 2, height / 2);
        collider.isTrigger = true;


        value = tiledObject.GetIntProperty("value", 4);
        spawned = tiledObject.GetBoolProperty("spawned", true);
        buttonID = tiledObject.GetStringProperty("buttonID", "");
        plateGroupID = tiledObject.GetStringProperty("plateGroupID", "");

        if(plateGroupID == "" && buttonID == "") { spawned = true; }

        absorbSound = Constants.soundSystem.LoadSound("audio/Monster_Absorb_Goo.mp3", false);
    }

    public int collect()
    {
        return value;
    }
    void Update()
    {

        checkSpawned();
    }

    public void checkSpawned()
    {
        if (!spawned)
        {
            visible = false;
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
            if(toSpawn)
            {
                spawned = true;
                visible = true;
            }

        }
        else
        {
            checkCollision();
        }
    }

    public void checkCollision()
    {
        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i].GetType() == typeof(Player))
            {
                Constants.goo = Constants.goo + value;
                Constants.ui.updateHUD();
                value = 0;
                LateDestroy();

                Constants.soundSystem.PlaySound(absorbSound, 14, false, Constants.sound14Volume, 0);
            }
        }
    }
}