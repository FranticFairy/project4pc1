using GXPEngine.Core;
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
        public static int xBoundarySize = 800;          // (pixels) player distance until scrolling
        public static int yBoundarySize = 500;          // (pixels) same but y
        public static float levelWidth = 2000;          // width of the whole thing (AAAAAAAA)
        public static float levelHeight = 2000;         // HEIGHT AAAAAAA

        // no need to change
        public static Player player;


        // PLAYER VALUES

        // Movement
        public static float xMaxSpeedPlayer = 4f;       // movement speed
        public static float acceleration = .5f;         // movement acceleration
        public static float deceleration = .15f;         // lower value = more slippery
        public static float grappleSpeed = 10f;         // speed for pulling self with grapple
        public static float jumpSpeedPlayer = 7f;       // the force propelling you upwards
        public static float gravityPlayer = .2f;        // the force pulling you down again

        // Shooting
        public static float minShootForce = 4f;         // min force applied to proj
        public static float maxShootForce = 13f;         // max force applied to proj
        public static float minShootForceGrapple = 6f;  // min force applied to grapple hook
        public static float maxShootForceGrapple = 13f; // max force applied to grapple hook
        public static float minShootMouseDist = 70f;    // moving mouse closer than this value in pixels to player doesn't decrease power anymore
        public static float maxShootMouseDist = 400f;   // same but the other side

        public static int aimTrajectoryAmount = 5;      // amount of white circles for aim trajectory
        public static int aimTrajectoryDist = 5;        // amount of frames between the circles

        // Animation                                       bigger value = faster animation
        public static float animPlayerShootingSpd = .5f;// animation speed for shooting
        public static float animPlayerInAirSpd = .1f;   // same for when player is in the air
        public static float animPlayerWalkingSpd = .3f; // same but walking
        public static float animPlayerIdleSpd = .1f;    // same but idle (doing nothing)

        // No need to change
        public static Vec2 positionPlayer;




        // PROJECTILE VALUES
        public static float gravityProj = .1f;          // projectile gravity value
        public static float bounciness = .98f;          // force given back after bounce. 1 = 100%
        public static int bouncyProjLimit = 3;          // max amount of bouncy proj
        public static float windPower = .5f;              // bigger number = stronger wind
        public static float maxProjSpeed = 20f;         // max speed a projectile can have

        public static float animProjSpd = .1f;          // projectile animation speed

        // WIND ANIMATION SPEED
        public static float animWindSpd = .1f;          // wind animation speed

        // MAP NAMES FOR TILED
        public static string level1 = "Level1Bo.tmx";
        public static string level2 = "2.tmx";
        public static string level3 = "3.tmx";
        public static string level4 = "4.tmx";
        public static string level5 = "5.tmx";
        public static string level6 = "6.tmx";
        public static string level7 = "7.tmx";


        // AUDIO VALUES
        // Background sounds (number is same as room it should be in)
        public static float sound1Volume = 1f;
        public static float sound2Volume = 1f;
        public static float sound3Volume = 1f;
        public static float sound4Volume = 1f;
        public static float sound5Volume = 1f;
        public static float sound6Volume = 1f;
        public static float sound7Volume = 1f;

        // Projectile related
        public static float sound8Volume = 1f;      // shoot bouncy
        public static float sound9Volume = 1f;      // shoot grapple
        public static float sound10Volume = 1f;     // proj bounces
        public static float sound11Volume = 1f;     // proj doesn't bounce
        public static float sound12Volume = 1f;     // grapple rope active

        // Player
        public static float sound13Volume = 1f;     // player moving
        public static float sound14Volume = 1f;     // player absorbing goo
        public static float sound15Volume = 1f;     // ded

        public static float sound16Volume = 1f;     // start/win music
        public static float sound17Volume = 1f;     // lose music

        // Ambience sounds (same as background sounds)
        public static float sound18Volume = 1f;
        public static float sound19Volume = 1f;
        public static float sound20Volume = 1f;
        public static float sound21Volume = 1f;
        public static float sound22Volume = 1f;
        public static float sound23Volume = 1f;
        public static float sound24Volume = 1f;

        public static float sound25Volume = 1f;     // menu and outro song
        public static float sound26Volume = 1f;     // death song

        public static float sound27Volume = 1f;     // victory noise

        public static float sound28Volume = 1f;     // box noise

        public static float sound29Volume = 1f;     // interface button sound

        public static float sound30Volume = 1f;     // box kill noise (not implemented)


        public static int frameCounter;

        public static UI ui;

        public static int goo; //How much Goo does the player spawn with?
        public static int startGoo = 8; //How much Goo does the player spawn with?

        public static int shotCost = 1; //How much goo is consumed on firing?
        public static int hookCost = 1; //How much goo is consumed upon grappling?

        public static bool dead = false;

        public static List<Button> buttons = new List<Button>();
        public static List<PressurePlate> plates = new List<PressurePlate>();

        public static FMODSoundSystem soundSystem = new FMODSoundSystem();

    }

}