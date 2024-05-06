using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SimplePlatform : Sprite
{

    public SimplePlatform(float xPos, float yPos) : base("colors.png")
    {
        x = xPos;
        y = yPos;

    }

}