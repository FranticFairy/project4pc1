using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class Level : GameObject
{
    Player player;
    Collectable item;
    UI ui;
    private int xBoundarySize = 200;    // player distance until scrolling
    private int yBoundarySize = 200;    // same but y

    private float levelWidth = 2000;
    private float levelHeight = 1000;


    private Player player;
    private Sprite background;



    public Level()
    {
        player.xPos = 400;
        player.yPos = 200;
        player = new Player(new Vec2(400,400));
        AddChild(player);

        item = new Collectable(200,400);
        AddChild(item);

        ui = new UI();
        AddChild(ui);
    }

    //public void HandleScroll()
    //{
    //    if (player == null) return;

    //    if (player.x + x < xBoundarySize) x = xBoundarySize - player.x;
    //    if (player.y + y < yBoundarySize) y = yBoundarySize - player.y;

    //    if (player.x + x > game.width - xBoundarySize) x = game.width - xBoundarySize - player.x;
    //    if (player.y + y > game.height - yBoundarySize) y = game.height - yBoundarySize - player.y;

    //    if (x > 0) x = 0;   // making sure the camera doesn't see the void on the left
    //    if (y > 0) y = 0;   // same but on top

    //    if (-x > (num5 * lineCoordMulti + 2 * lineOffset.x) - game.width) x = -((num5 * lineCoordMulti + 2 * lineOffset.x) - game.width);   // right
    //    if (-y > background.height - game.height) y = -(background.height - game.height); // bottom

    //}

    void Update()
    {
        ui.updateHUD();


    }
}
