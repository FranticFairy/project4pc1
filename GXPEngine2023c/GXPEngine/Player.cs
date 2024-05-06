using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class Player : AnimationSprite
{
    private Vec2 velocity = new Vec2(0,0);
    private float acceleration = .5f;
    private float deceleration = .1f;

    private float xMaxSpeed = 5f;

    public float xPos;
    public float yPos;

    private float jumpSpeed = 5f;
    private float gravity = .2f;

    private bool isGrounded;
    public bool isSprinting;

    private int coyoteTime;
    private int coyoteTimeMax = 10;

    private int goo = 1;



    public Player(string fileName = "barry.png", int cols = 7, int rows = 1, TiledObject tiledObject = null) : base(fileName, cols, rows)
    {

        SetOrigin(width / 2, height / 2);

    }


    private void MovePlayer()
    {

        int deltaTimeClamped = Mathf.Min(Time.deltaTime, 40);

        SetCycle(0, 7);


        if (Input.GetKey(Key.A))
        {

            velocity.x -= acceleration;
            Mirror(true, false);
        }
        else if (Input.GetKey(Key.D))
        {

            velocity.x += acceleration;
            Mirror(false, false);
        }



        if (velocity.x > -deceleration && velocity.x < deceleration) velocity.x = 0;
        if (velocity.x > 0) velocity.x -= deceleration;
        if (velocity.x < 0) velocity.x += deceleration;
        velocity.x = Mathf.Clamp(velocity.x, -xMaxSpeed, xMaxSpeed);






        velocity.y += gravity;
        isGrounded = false;
        if (yPos > 500)
        {
            velocity.y = 0;
            isGrounded = true;
            coyoteTime = coyoteTimeMax;
            yPos = 500;
        }

        if (!isGrounded && coyoteTime > 0) coyoteTime--;

        if (Input.GetKeyDown(Key.W) && coyoteTime > 0)
        {
            velocity.y = -jumpSpeed;
            coyoteTime = 0;

        }


        xPos += velocity.x;
        yPos += velocity.y;

        x = xPos;
        y = yPos;
       

        Animate(.1f);
    }






    void Update()
    {
        MovePlayer();
        checkCollision();
        Constants.goo = goo;

    }

    void checkCollision()
    {
        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i].GetType() == typeof(Collectable))
            {
                goo++;
                collisions[i].LateDestroy();
            }
        }
    }


}