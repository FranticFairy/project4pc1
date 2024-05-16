using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using System.Collections.Generic;
using GXPEngine.Core;

public class MyGame : Game
{

    private string nextLevel;
    private string currentLevel;
    FMODSoundSystem soundSystem;

    private IntPtr channel1;
    private IntPtr channel2;
    private IntPtr channel3;
    private IntPtr channel4;
    private IntPtr channel5;
    private IntPtr channel6;
    private IntPtr channel7;

    private IntPtr channel18;
    private IntPtr channel19;
    private IntPtr channel20;
    private IntPtr channel21;
    private IntPtr channel22;
    private IntPtr channel23;
    private IntPtr channel24;

    private IntPtr deathSound;

    public MyGame() : base(1920, 1080, false/*, true, 1920, 1080, true*/)
    {
        soundSystem = Constants.soundSystem;

        channel1 = soundSystem.CreateStream("audio/Song_start_location.mp3", true);
        channel2 = soundSystem.CreateStream("audio/Song_hallway_with_doors.mp3", true);
        channel3 = soundSystem.CreateStream("audio/Song_higher_background_with_two_platforms.mp3", true);
        channel4 = soundSystem.CreateStream("audio/Song_longer_rooms_with_on_the_end_a_glass_room.mp3", true);
        channel5 = soundSystem.CreateStream("audio/Song_longer_room_with_roof_that_goes_up_a_little.mp3", true);
        channel6 = soundSystem.CreateStream("audio/Song_long_hallway.mp3", true);
        channel7 = soundSystem.CreateStream("audio/Song_end_location.mp3", true);

        channel18 = soundSystem.CreateStream("audio/Ambient_noise_start_location.mp3", true);
        channel19 = soundSystem.CreateStream("audio/Ambient_noise_hallway_with_doors.mp3", true);
        channel20 = soundSystem.CreateStream("audio/Ambient_noise_higher_background_with_two_platforms.mp3", true);
        channel21 = soundSystem.CreateStream("audio/Ambient_noise_longer_rooms_with_on_the_end_a_glass_room.mp3", true);
        channel22 = soundSystem.CreateStream("audio/Ambient_noise_longer_room_with_roof_that_goes_up_a_little.mp3", true);
        channel23 = soundSystem.CreateStream("audio/Ambient_noise_long_hallway.mp3", true);
        channel24 = soundSystem.CreateStream("audio/Ambient_noise_end_location.mp3", true);

        deathSound = soundSystem.LoadSound("audio/Death_Noise.mp3", false);

        //soundSystem.PlaySound(channel1, 1, false, 0, 0);
        //soundSystem.PlaySound(channel2, 2, false, 0, 0);
        // soundSystem.PlaySound(channel3, 3, false, 0, 0);
        //soundSystem.PlaySound(channel4, 4, false, 0, 0);
        //soundSystem.PlaySound(channel5, 5, false, 0, 0);
        //soundSystem.PlaySound(channel6, 6, false, 0, 0);
        //soundSystem.PlaySound(channel7, 7, false, 0, 0);

        /*
        // put these wherever or copy the above setup and do the same shit
        soundSystem.PlaySound(soundSystem.CreateStream("audio/Menu_And_Outro_Song.mp3", true), 16, false, Constants.sound16Volume, 0);
        soundSystem.PlaySound(soundSystem.CreateStream("audio/Death_Song.mp3", true), 17, false, Constants.sound17Volume, 0);


        /**/



        OnAfterStep += CheckLoadLevel;
        LoadLevel(Constants.level1);
        Constants.goo = Constants.startGoo;
    }

    public void LoadLevel(string filename)
    {
        nextLevel = filename;
    }


    void DestroyAll()
    {
        List<GameObject> children = GetChildren();
        Constants.buttons = new List<Button>();
        Constants.plates = new List<PressurePlate>();
        foreach (GameObject child in children)
        {
            child.Destroy();
        }
        
        switch (currentLevel)   // setting sound to 0
        {
            case var value when value == Constants.level1:
                soundSystem.PlaySound(channel1, 1, false, 0, 0);
                soundSystem.PlaySound(channel18, 18, false, 0, 0);
                break;
            case var value when value == Constants.level2:
                soundSystem.PlaySound(channel2, 2, false, 0, 0);
                soundSystem.PlaySound(channel19, 19, false, 0, 0);
                break;
            case var value when value == Constants.level3:
                soundSystem.PlaySound(channel3, 3, false, 0, 0);
                soundSystem.PlaySound(channel20, 20, false, 0, 0);
                break;
            case var value when value == Constants.level4:
                soundSystem.PlaySound(channel4, 4, false, 0, 0);
                soundSystem.PlaySound(channel21, 21, false, 0, 0);
                break;
            case var value when value == Constants.level5:
                soundSystem.PlaySound(channel5, 5, false, 0, 0);
                soundSystem.PlaySound(channel22, 22, false, 0, 0);
                break;
            case var value when value == Constants.level6:
                soundSystem.PlaySound(channel6, 6, false, 0, 0);
                soundSystem.PlaySound(channel23, 23, false, 0, 0);
                break;
            case var value when value == Constants.level7:
                soundSystem.PlaySound(channel7, 7, false, 0, 0);
                soundSystem.PlaySound(channel24, 24, false, 0, 0);
                break;

        }
        
    }

    void CheckLoadLevel()
    {
        if (nextLevel != null)
        {
            DestroyAll();
            SetSound();
            Constants.level = new TiledLevel(nextLevel);
            AddChild(Constants.level);
            Constants.ui = new UI();
            AddChild(Constants.ui);
            currentLevel = nextLevel;
            nextLevel = null;

        }
    }

    void SetSound()
    {
        switch (nextLevel)   // setting sound to 0
        {
            case var value when value == Constants.level1:
                soundSystem.PlaySound(channel1, 1, false, Constants.sound1Volume, 0);
                soundSystem.PlaySound(channel18, 18, false, Constants.sound18Volume, 0);
                break;
            case var value when value == Constants.level2:
                soundSystem.PlaySound(channel2, 2, false, Constants.sound2Volume, 0);
                soundSystem.PlaySound(channel19, 19, false, Constants.sound19Volume, 0);
                break;
            case var value when value == Constants.level3:
                soundSystem.PlaySound(channel3, 3, false, Constants.sound3Volume, 0);
                soundSystem.PlaySound(channel20, 20, false, Constants.sound20Volume, 0);
                break;
            case var value when value == Constants.level4:
                soundSystem.PlaySound(channel4, 4, false, Constants.sound4Volume, 0);
                soundSystem.PlaySound(channel21, 21, false, Constants.sound21Volume, 0);
                break;
            case var value when value == Constants.level5:
                soundSystem.PlaySound(channel5, 5, false, Constants.sound5Volume, 0);
                soundSystem.PlaySound(channel22, 22, false, Constants.sound22Volume, 0);
                break;
            case var value when value == Constants.level6:
                soundSystem.PlaySound(channel6, 6, false, Constants.sound6Volume, 0);
                soundSystem.PlaySound(channel23, 23, false, Constants.sound23Volume, 0);
                break;
            case var value when value == Constants.level7:
                soundSystem.PlaySound(channel7, 7, false, Constants.sound7Volume, 0);
                soundSystem.PlaySound(channel24, 24, false, Constants.sound24Volume, 0);
                break;

        }
    }



    void Update() 
	{
        Constants.frameCounter++;
        if(Constants.frameCounter >= 2500)
        {
            Console.WriteLine(GetDiagnostics());
            Console.WriteLine(Constants.frameCounter);
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
        }
        if (Constants.dead)
        {
            soundSystem.PlaySound(deathSound, 15, false, Constants.sound15Volume, 0);
            foreach (var child in GetChildren())
			{
                DestroyAll();
                Constants.goo = Constants.startGoo;
            }
            //LoadLevel(Constants.level1);
            LoadLevel(currentLevel);
            Constants.dead = false;
        }
        if(Constants.level != null)
        {
            Constants.level.Update();
        }
    }

	static void Main()                          
	{
		new MyGame().Start();                   
	}

}