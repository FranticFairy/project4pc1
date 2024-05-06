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
    public Level()
    {

        player = new Player();
        player.xPos = 400;
        player.yPos = 400;
        AddChild(player);


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



    }
}
