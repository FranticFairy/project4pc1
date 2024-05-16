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
                //soundSystem.SetChannelVolume(1, 0);
                soundSystem.PlaySound(channel1, 1, false, 0, 0);
                break;
            case var value when value == Constants.level2:
                //soundSystem.SetChannelVolume(2, 0);
                soundSystem.PlaySound(channel2, 2, false, 0, 0);
                break;
            case var value when value == Constants.level3:
                //soundSystem.SetChannelVolume(3, 0);
                soundSystem.PlaySound(channel3, 3, false, 0, 0);
                break;
            case var value when value == Constants.level4:
                //soundSystem.SetChannelVolume(4, 0);
                soundSystem.PlaySound(channel4, 4, false, 0, 0);
                break;
            case var value when value == Constants.level5:
                //soundSystem.SetChannelVolume(5, 0);
                soundSystem.PlaySound(channel5, 5, false, 0, 0);
                break;
            case var value when value == Constants.level6:
                //soundSystem.SetChannelVolume(6, 0);
                soundSystem.PlaySound(channel6, 6, false, 0, 0);
                break;
            case var value when value == Constants.level7:
                //soundSystem.SetChannelVolume(7, 0);
                soundSystem.PlaySound(channel7, 7, false, 0, 0);
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
                //soundSystem.SetChannelVolume(1, Constants.sound1Volume);
                soundSystem.PlaySound(channel1, 1, false, Constants.sound1Volume, 0);
                break;
            case var value when value == Constants.level2:
                //soundSystem.SetChannelVolume(2, Constants.sound2Volume);
                soundSystem.PlaySound(channel2, 2, false, Constants.sound2Volume, 0);
                break;
            case var value when value == Constants.level3:
                //soundSystem.SetChannelVolume(3, Constants.sound3Volume);
                soundSystem.PlaySound(channel3, 3, false, Constants.sound3Volume, 0);
                break;
            case var value when value == Constants.level4:
                //soundSystem.SetChannelVolume(4, Constants.sound4Volume);
                soundSystem.PlaySound(channel4, 4, false, Constants.sound4Volume, 0);
                break;
            case var value when value == Constants.level5:
                //soundSystem.SetChannelVolume(5, Constants.sound5Volume);
                soundSystem.PlaySound(channel5, 5, false, Constants.sound5Volume, 0);
                break;
            case var value when value == Constants.level6:
                //soundSystem.SetChannelVolume(6, Constants.sound6Volume);
                soundSystem.PlaySound(channel6, 6, false, Constants.sound6Volume, 0);
                break;
            case var value when value == Constants.level7:
                //soundSystem.SetChannelVolume(7, Constants.sound7Volume);
                soundSystem.PlaySound(channel7, 7, false, Constants.sound7Volume, 0);
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
    }

	static void Main()                          
	{
		new MyGame().Start();                   
	}

}