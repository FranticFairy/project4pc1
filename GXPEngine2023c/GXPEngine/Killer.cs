using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class Killer : AnimationSprite
{
    public Killer(string fileName = "danger.png", int cols = 1, int rows = 1, TiledObject tiledObject = null) : base(fileName, cols, rows)
    {

        SetOrigin(width / 2, height / 2);
        collider.isTrigger = true;

    }
}