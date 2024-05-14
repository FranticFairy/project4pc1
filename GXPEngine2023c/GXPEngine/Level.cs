using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class Level : GameObject
{
    

    private List<Collectable> items = new List<Collectable>();

    private Player player;
    private Sprite background;
    private UI ui;
    private Killer killer;
    private Button button;


    List<SimplePlatform> _platforms;

    public int GetNumberOfLines()
    {
        return _platforms.Count;
    }

    public SimplePlatform GetLine(int index)
    {
        if (index >= 0 && index < _platforms.Count)
        {
            return _platforms[index];
        }
        return null;
    }

    public Level()
    {
        /*
        background = new Sprite("square.png", false, false);
        background.scaleX = 15f;
        background.scaleY = 15f;
        AddChild(background);
        

        player = new Player(new Vec2(400, 400));
        Constants.player = player;
        AddChild(player);


        _platforms = new List<SimplePlatform>();
        for (int i = 0; i < 20; i++)
        {
            SimplePlatform simplePlatform = new SimplePlatform(0, 0);
            simplePlatform.x = i * simplePlatform.width;
            simplePlatform.y = 900;
            AddChild(simplePlatform);
            AddPlatform(i, 900, true);
        }
        //AddChild(new SimplePlatform(400, 850));
        //AddPlatform(400, 850);
        AddPlatform(700, 400);

        Collectable item = new Collectable(false, 200, 800);
        items.Add(item);

        killer = new Killer(1000, 800);
        AddChild(killer);

        button = new Button(700, 800);
        button.addToConstants();
        AddChild(button);

        item.linkedButton = button;

        ui = new UI();
        AddChild(ui);*/
    }

    void AddPlatform(float xPosPlatform, float yPosPlatform, bool useWidth = false)
    {
        SimplePlatform simplePlatform = new SimplePlatform(xPosPlatform, yPosPlatform);
        if (useWidth) simplePlatform.x *= simplePlatform.width;
        AddChild(simplePlatform);
        _platforms.Add(simplePlatform);
    }

    public void HandleScroll()
    {
        if (player == null) return;

        if (player.x + x < Constants.xBoundarySize) x = Constants.xBoundarySize - player.x;
        if (player.y + y < Constants.yBoundarySize) y = Constants.yBoundarySize - player.y;

        if (player.x + x > game.width - Constants.xBoundarySize) x = game.width - Constants.xBoundarySize - player.x;
        if (player.y + y > game.height - Constants.yBoundarySize) y = game.height - Constants.yBoundarySize - player.y;

        if (x > 0) x = 0;   // making sure the camera doesn't see the void on the left
        if (y > 0) y = 0;   // same but on top

        if (-x > Constants.levelWidth - game.width) x = -(Constants.levelWidth - game.width);   // right
        if (-y > Constants.levelHeight - game.height) y = -(Constants.levelHeight - game.height); // bottom

    }

    void checkPuzzles()
    {
        foreach(Collectable item in items)
        {
            if (item.spawned == false)
            {
                int index = Constants.buttons.FindIndex(a => a == item.linkedButton);
                if (Constants.buttonStates[index] == true)
                {
                    AddChild(item);
                    item.spawned = true;
                }
            }
        }
    }

    void Update()
    {
        
        HandleScroll();
        checkPuzzles();
        ui.updateHUD();
        foreach(Button button in Constants.buttons)
        {
            if(!button.triggered)
            {
                button.checkToggle();
            }
        }

    }
}
