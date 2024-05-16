using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class SceneSwitcher : AnimationSprite
{
    public string nextLevel;


    public SceneSwitcher(string fileName, int cols, int rows, TiledObject tiledObject = null) : base(fileName, cols, rows)
    {

        nextLevel = tiledObject.GetStringProperty("nextLevel", "Level1.tmx");


    }

}