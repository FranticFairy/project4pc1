using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    static class Constants
    {
        public static UI ui;
        //highScore will be reworked later.
        public static int goo;

        public static bool dead;

        public static List<Button> buttons = new List<Button>();
        public static List<bool> buttonStates = new List<bool>();

        public static float acceleration = .5f;
        public static float deceleration = .1f;
    }

}