using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class UI : GameObject
    {
        private int goo = 0;

        private Sprite gooCounter;
        private Sprite gooCounterBG;
        private Sprite gooCounterBGB;
        EasyDraw gooNum;

        public UI()
        {
            gooCounter = new Sprite("goo bar front.png");
            gooCounter.collider.isTrigger = true;
            gooCounter.SetOrigin(gooCounter.width / 2, gooCounter.height / 2);
            gooCounter.SetXY(50, 50);

            gooCounterBG = new Sprite("goo bar back.png");
            gooCounterBG.collider.isTrigger = true;
            gooCounterBG.SetOrigin(gooCounterBG.width / 2, gooCounterBG.height / 2);
            gooCounterBG.SetXY(50, 50);

            gooCounterBGB = new Sprite("goo bar back.png");
            gooCounterBGB.collider.isTrigger = true;
            gooCounterBGB.SetOrigin(gooCounterBG.width / 2, gooCounterBG.height / 2);
            gooCounterBGB.SetXY(50, 50);
            gooCounterBGB.alpha = 0;


            AddChild(gooCounterBG);
            AddChild(gooCounter);
            AddChild(gooCounterBGB);

            gooNum = new EasyDraw(25, 25);
            gooNum.SetXY(-10, -10);
            Console.WriteLine(gooNum.color);
            gooNum.SetColor(0,0,0);
            gooNum.Text(Constants.goo.ToString());
            gooCounterBGB.AddChild(gooNum);
        }

        public void updateHUD()
        {

            gooCounterBGB.RemoveChild(gooNum);
            gooNum = new EasyDraw(25, 25);
            gooNum.SetXY(-10, -10);
            gooNum.SetColor(0, 0, 0);
            gooNum.Text(Constants.goo.ToString());
            gooCounterBGB.AddChild(gooNum);

            switch (Constants.goo)
            {
                case 0:
                    gooCounter.scale = 0.0f;
                    break;
                case 1:
                    gooCounter.scale = 0.1f;
                    break;
                case 2:
                    gooCounter.scale = 0.2f;
                    break;
                case 3:
                    gooCounter.scale = 0.3f;
                    break;
                case 4:
                    gooCounter.scale = 0.4f;
                    break;
                case 5:
                    gooCounter.scale = 0.5f;
                    break;
                case 6:
                    gooCounter.scale = 0.6f;
                    break;
                case 7:
                    gooCounter.scale = 0.7f;
                    break;
                case 8:
                    gooCounter.scale = 0.8f;
                    break;
                case 9:
                    gooCounter.scale = 0.9f;
                    break;
                default:
                    gooCounter.scale = 1f;
                    break;
            }
        }
    }
}
