using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Collectable : AnimationSprite
    {
        //Collectables are goo pickups. You touch them to get more Goo. Simple as that.

        public int value;
        public float xPos;
        public float yPos;

        public Button linkedButton;
        public bool spawned;

        public Collectable(int value, bool spawned, string fileName = "goo.png", int cols = 1, int rows = 1) : base(fileName, cols, rows)
        {

            SetOrigin(width / 2, height / 2);
            collider.isTrigger = true;
            this.value = value;

        }

        public int collect()
        {
            return value;
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
                }
            }
        }
    }
}
