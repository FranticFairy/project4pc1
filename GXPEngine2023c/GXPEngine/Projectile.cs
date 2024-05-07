﻿using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Projectile : Sprite
{
    private float gravity = .1f;
    private Vec2 velocity;

    public Projectile(Vec2 vel, Vec2 pos, string fileName = "circle.png") : base(fileName)
    {
        SetOrigin(width/2, height/2);
        velocity = vel;
        x = pos.x;
        y = pos.y;
    }

    void Update()
    {
        Step();
    }
    int counter;

    public void Step()
    {

        counter++;

        if (!Input.GetKey(Key.O) || counter >= 60)
        {
            velocity.y += gravity;

            x += velocity.x;
            y += velocity.y;

            //Console.WriteLine("Projectile coordinates: "+x+" "+y);

            if (x < 0 || x > 5000 || y < 0 || y > 5000)
            {

                LateDestroy();      //WATCH OUT, NEEDS PROPER COLLISIONS
            }
            counter = 0;
        }



    }


}