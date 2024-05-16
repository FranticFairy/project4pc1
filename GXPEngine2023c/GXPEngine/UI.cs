using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class UI : GameObject
    {
        private int goo = 0;
        EasyDraw gooCounter;

        public UI()
        {
            gooCounter = new EasyDraw(288, 32);
            gooCounter.Text("Goo: " + goo);
            AddChild(gooCounter);
        }

        public void updateHUD()
        {
            if(goo != Constants.goo)
            {
                
                RemoveChild(gooCounter);
                goo = Constants.goo;
                gooCounter = new EasyDraw(288, 32);
                gooCounter.Text("Goo: " + goo);
                AddChild(gooCounter);
            }
        }
    }
}
