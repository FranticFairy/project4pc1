using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class Collectable : AnimationSprite
{
    //Collectables are goo pickups. You touch them to get more Goo. Simple as that.

    public int value;
    public float xPos;
    public float yPos;

    public Button linkedButton;
    public bool spawned;

    public Collectable(string imageFile, int columns, int rows, TiledObject tiledObject = null) : base(imageFile, columns, rows)
    {

        SetOrigin(width / 2, height / 2);
        collider.isTrigger = true;
        //this.value = value;
        value = tiledObject.GetIntProperty("value",4);

    }

    public int collect()
    {
        return value;
    }
    void Update()
    {
        checkCollision();

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

                Constants.soundSystem.PlaySound(Constants.soundSystem.LoadSound("audio/Monster_Absorb_Goo.mp3", false), 14, false, Constants.sound14Volume, 0);
            }
        }
    }
}