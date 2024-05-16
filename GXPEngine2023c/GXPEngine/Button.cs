using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class Button : AnimationSprite
{

    //Buttons should be able to be triggered by PROJECTILES and PLAYERS
    //Buttons are either Toggled or not
    //Buttons are toggled the moment something comes in contact with them
    //Projectiles stick to a button the moment it is triggered.

    public float xPos;
    public float yPos;
    public bool triggered = false;
    public string buttonID;
    private Sprite toggledButton = new Sprite("PressedButton.png",false,false);

    public Button(string imageFile, int columns, int rows, TiledObject tiledObject = null) : base(imageFile, columns, rows)
    {
        SetOrigin(width / 2, height / 2);
        //SetXY(xPos, yPos);
        collider.isTrigger = true;

        buttonID = tiledObject.GetStringProperty("buttonID", "");
        Constants.buttons.Add(this);
        toggledButton.visible = false;
        toggledButton.SetOrigin(toggledButton.width/2,toggledButton.height/2);
        AddChild(toggledButton);
    }

    public void checkToggle()
    {
        triggered = false;
        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i].GetType() == typeof(Player) || collisions[i].GetType() == typeof(BouncyProjectile))
            {


                triggered = true;
                alpha = 0;
                toggledButton.visible = true;
                int index = Constants.buttons.FindIndex(a => a == this);


                if (collisions[i].GetType() == typeof(BouncyProjectile))
                {

                    //Here we will handle stopping projectiles
                }
            }
        }
    }


}