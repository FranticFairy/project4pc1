﻿using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class Movable : AnimationSprite
{
    private GrappleHook grappleHook;
    private bool childDetected = false;
    private Vec2 deltaPos;
    private Vec2 velocity = new Vec2(0,0);
    private float gravity = .1f;
    private bool inAir = false;
    public Movable(string fileName, int cols, int rows, TiledObject tiledObject = null) : base(fileName, cols, rows)
    {
        //collider.isTrigger = true;
        //scale = 2f;
        SetOrigin(width / 2, height / 2);

    }


    void Update()
    {
        int deltaTimeClamped = Mathf.Min(Time.deltaTime, 40);
        float deltaTimeFun = (float)deltaTimeClamped / 1000 * 120;


        if (Input.GetKeyDown(Key.U)) x -= 50;
        if (!childDetected)
        {
            List<GameObject> children = GetChildren();
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].GetType() == typeof(GrappleHook))
                {
                    grappleHook = (GrappleHook)children[i];
                    childDetected = true;
                }
            }
        }
        else
        {
            deltaPos = -1*grappleHook.deltaPos;
            velocity.x = deltaPos.x * .01f;
            if (Input.GetKeyDown(Key.SPACE) && grappleHook != null) 
            {
                grappleHook.LateDestroy();
                childDetected = false;
                velocity.x = 0;
            }
        }

        velocity.y += gravity;

        if (MoveUntilCollision(0, velocity.y * deltaTimeFun) != null)
        {
            velocity.y = 0;
            if (inAir)
            {
                velocity.x = 0;
                inAir = false;
            }
        }
        else
        {
            if (grappleHook != null) grappleHook.LateDestroy();
            childDetected = false;
            velocity.x *= .99f;
            inAir = true;
        }
        MoveUntilCollision(velocity.x * deltaTimeFun, 0);


    }

}