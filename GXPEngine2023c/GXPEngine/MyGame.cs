using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {
	public MyGame() : base(800, 600, false)     
	{
		Level level = new Level();
		AddChild(level);
	}


	void Update() {
		
	}

	static void Main()                          
	{
		new MyGame().Start();                   
	}
}