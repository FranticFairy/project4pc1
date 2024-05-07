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

    private float minShootPower = 4f;
    private float maxShootPower = 9f;
    private float revShootPwrMulti = 25f; // bigger number -> further aim for same result


    private float jumpSpeed = 7f;
    private float gravity = .2f;

    private Level level;

    public Vec2 position;

    private bool isGrounded;
    public bool isSprinting;

    private int coyoteTime;
    private int coyoteTimeMax = 10;





    public Player(Vec2 pos, string fileName = "barry.png", int cols = 7, int rows = 1, TiledObject tiledObject = null) : base(fileName, cols, rows)
    {

        SetOrigin(width / 2, height / 2);
        position = pos;

        level = game.FindObjectOfType<Level>();

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
        if (MoveUntilCollision(0, velocity.y) != null /*|| position.y > 500*/)
        {
            velocity.y = 0;
            isGrounded = true;
            coyoteTime = coyoteTimeMax;
            //position.y = 500;
        }

        if (!isGrounded && coyoteTime > 0) coyoteTime--;

        if (Input.GetKeyDown(Key.W) && coyoteTime > 0)
        {
            velocity.y = -jumpSpeed;
            coyoteTime = 0;

        }

        position += velocity;


        x = position.x; 
        y = position.y;

        //Console.WriteLine("Player coordinates: " + position);

        Animate(.1f);
    }


    void Shooting()
    {

        if (Input.GetKeyDown(Key.L))
        {
            level = game.FindObjectOfType<Level>();
            Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);
            Vec2 levelPos = new Vec2(level.x, level.y);
            Vec2 deltaPos = mousePos - (position+levelPos);
            float shootPower = Mathf.Clamp(deltaPos.Length()/revShootPwrMulti, minShootPower, maxShootPower);

            Vec2 projVec = deltaPos.Normalized()*shootPower;

            level.AddChild(new Projectile(projVec, position));


            /*Console.WriteLine("shoot");
            Console.WriteLine(" ");
            Console.WriteLine("mousePos = "+mousePos);
            Console.WriteLine("position = "+position);
            Console.WriteLine("deltaPos = "+deltaPos);
            Console.WriteLine("level pos = "+level.x+" "+level.y);
            Console.WriteLine("shootPower = "+shootPower);
            Console.WriteLine("projVec = "+projVec);
            Console.WriteLine(" ");*/


        }

    }



    int counter;

    void Update()
    {

        counter++;

        if (!Input.GetKey(Key.O) || counter >= 60) {

            MovePlayer();
            Shooting();
            counter = 0;
        }

    }



}