using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {

	Level level;
	public MyGame() : base(1920, 1080, false/*, true, 1920, 1080, true*/)     
	{
		level = new Level();
		Constants.level = level;
		AddChild(level);
	}

    public Level GetLevel()
    {
        return level;
    }
    void Update() 
	{
        if (Constants.dead)
        {
			foreach(var child in GetChildren())
			{
				child.Destroy();
            }
            Level levell = new Level();
			Constants.level = levell;
            AddChild(levell);
            Constants.dead = false;
        }
    }

	static void Main()                          
	{
		new MyGame().Start();                   
	}

}