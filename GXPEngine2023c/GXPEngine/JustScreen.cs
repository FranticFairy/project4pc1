using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Channels;

public class JustScreen : GameObject
{
    private int theSoundNumber;
    FMODSoundSystem soundSystem;
    private IntPtr winSound;
    private IntPtr loseSound;
    private string bgImg;

    public JustScreen(int screenNr = 0)
    {
        switch (screenNr)
        {
            case 0: bgImg = "menu 2.png";
                break;
            case 1: bgImg = "end screen 2.png";
                break;
            case 2: bgImg = "character end screen goobert has run out of goo 2.2.png";
                break;
        }
        AddChild(new Sprite(bgImg));
        theSoundNumber = screenNr;

        soundSystem = new FMODSoundSystem();
        winSound = soundSystem.CreateStream("audio/Menu_And_Outro_Song.mp3", true);
        loseSound = soundSystem.CreateStream("audio/Death_Song.mp3", true);

        MakeSound();
    }


    void MakeSound()
    {
        switch (theSoundNumber)
        {
            case 0:
                soundSystem.PlaySound(winSound, 25, false, Constants.sound25Volume, 0);
                break;
            case 1:
                soundSystem.PlaySound(winSound, 25, false, Constants.sound25Volume, 0);
                break; 
            case 2:
                soundSystem.PlaySound(loseSound, 26, false, Constants.sound26Volume, 0);
                break;
        }
    }

    void BegoneSound()
    {
        switch (theSoundNumber)
        {
            case 0:
                soundSystem.PlaySound(winSound, 25, false, 0, 0);
                break;
            case 1:
                soundSystem.PlaySound(winSound, 25, false, 0, 0);
                break;
            case 2:
                soundSystem.PlaySound(loseSound, 26, false, 0, 0);
                break;
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(Key.ENTER))
        {
            BegoneSound();
            LateDestroy();
            MyGame supergame = game.FindObjectOfType<MyGame>();
            if (theSoundNumber == 0) supergame.LoadLevel(Constants.level1);
            else supergame.AddChild(new JustScreen(0));
        }

    }

}