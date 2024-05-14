using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    static class Constants
    {

        // MYGAME VALUES
        public static TiledLevel level;



        // LEVEL VALUES
        public static int xBoundarySize = 200;          // (pixels) player distance until scrolling
        public static int yBoundarySize = 200;          // (pixels) same but y
        public static float levelWidth = 2000;          // width of the whole thing (AAAAAAAA)
        public static float levelHeight = 2000;         // HEIGHT AAAAAAA

        // no need to change
        public static Player player;

        // PLAYER VALUES

        // Movement
        public static float xMaxSpeedPlayer = 5f;       // movement speed
        public static float acceleration = .5f;         // movement acceleration
        public static float deceleration = .1f;         // lower value = more slippery
        public static float grappleSpeed = 10f;         // speed for pulling self with grapple
        public static float jumpSpeedPlayer = 7f;       // the force propelling you upwards
        public static float gravityPlayer = .2f;        // the force pulling you down again

        // Shooting
        public static float minShootForce = 4f;         // min force applied to proj
        public static float maxShootForce = 9f;         // max force applied to proj
        public static float minShootForceGrapple = 6f;  // min force applied to grapple hook
        public static float maxShootForceGrapple = 13f; // max force applied to grapple hook
        public static float minShootMouseDist = 70f;    // moving mouse closer than this value in pixels to player doesn't decrease power anymore
        public static float maxShootMouseDist = 300f;   // same but the other side

        public static int aimTrajectoryAmount = 5;      // amount of white circles for aim trajectory
        public static int aimTrajectoryDist = 5;        // amount of frames between the circles

        // No need to change
        public static Vec2 positionPlayer;




        // PROJECTILE VALUES
        public static float gravityProj = .1f;          // projectile gravity value
        public static float bounciness = .98f;          // force given back after bounce. 1 = 100%
        public static int bouncyProjLimit = 3;          // max amount of bouncy proj



        public static UI ui;
        //highScore will be reworked later.
        public static int goo;

        public static bool dead = false;

        public static List<Button> buttons = new List<Button>();
        public static List<bool> buttonStates = new List<bool>();

    }

}