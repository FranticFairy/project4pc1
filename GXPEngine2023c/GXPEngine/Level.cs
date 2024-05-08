using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class Level : GameObject
{
    private int xBoundarySize = 200;    // player distance until scrolling
    private int yBoundarySize = 200;    // same but y

    private float levelWidth = 2000;
    private float levelHeight = 1000;

    private List<Collectable> items = new List<Collectable>();

    private Player player;
    private Sprite background;
    private UI ui;
    private Killer killer;
    private Button button;



    public Level()
    {
        background = new Sprite("square.png", false, false);
        background.scaleX = 15f;
        background.scaleY = 15f;
        AddChild(background);

        player = new Player(new Vec2(400, 400));
        AddChild(player);

        for (int i = 0; i < 20; i++)
        {
            SimplePlatform simplePlatform = new SimplePlatform(0, 0);
            simplePlatform.x = i * simplePlatform.width;
            simplePlatform.y = 900;
            AddChild(simplePlatform);
        }
        AddChild(new SimplePlatform(400, 850));

        Collectable item = new Collectable(false, 200, 800);
        items.Add(item);

        killer = new Killer(1000, 800);
        AddChild(killer);

        button = new Button(700, 800);
        button.addToConstants();
        AddChild(button);

        item.linkedButton = button;

        ui = new UI();
        AddChild(ui);
    }

    public void HandleScroll()
    {
        if (player == null) return;

        if (player.x + x < xBoundarySize) x = xBoundarySize - player.x;
        if (player.y + y < yBoundarySize) y = yBoundarySize - player.y;

        if (player.x + x > game.width - xBoundarySize) x = game.width - xBoundarySize - player.x;
        if (player.y + y > game.height - yBoundarySize) y = game.height - yBoundarySize - player.y;

        if (x > 0) x = 0;   // making sure the camera doesn't see the void on the left
        if (y > 0) y = 0;   // same but on top

        if (-x > levelWidth - game.width) x = -(levelWidth - game.width);   // right
        if (-y > levelHeight - game.height) y = -(levelHeight - game.height); // bottom

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
