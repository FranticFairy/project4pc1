using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using System.Collections.Generic;

public class MyGame : Game
{
    private string nextLevel;
    private string currentLevel;
    public MyGame() : base(1920, 1080, false/*, true, 1920, 1080, true*/)
    {
        OnAfterStep += CheckLoadLevel;
        LoadLevel("map.tmx");
    }

    public void LoadLevel(string filename)
    {
        nextLevel = filename;
    }


    void DestroyAll()
    {
        List<GameObject> children = GetChildren();
        foreach (GameObject child in children)
        {
            child.Destroy();
        }
    }

    void CheckLoadLevel()
    {

        if (nextLevel != null)
        {
            DestroyAll();
            Constants.level = new TiledLevel(nextLevel);
            AddChild(Constants.level);
            currentLevel = nextLevel;
            nextLevel = null;

        }
    }


    void Update() 
	{
        if (Constants.dead)
        {
			foreach(var child in GetChildren())
			{
                DestroyAll();
                Constants.goo = 0;
            }
            LoadLevel("map.tmx");
            Constants.dead = false;
        }
    }

	static void Main()                          
	{
		new MyGame().Start();                   
	}

}