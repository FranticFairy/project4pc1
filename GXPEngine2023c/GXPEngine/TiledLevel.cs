using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

internal class TiledLevel : GameObject
{

    private Player Player;
    private Killer Killer;

    Sprite background;

    public TiledLevel(string MapName)
    {
        TiledLoader loader = new TiledLoader(MapName);

        /*
        loader.LoadTileLayers(0);
        loader.rootObject = this;
        loader.addColliders = false;
        loader.LoadObjectGroups(1);
        loader.LoadObjectGroups(0);
        loader.autoInstance = true;
        */

        loader.addColliders = false;
        loader.LoadImageLayers();
        loader.addColliders = true;
        loader.rootObject = this;
        loader.LoadTileLayers(0);
        loader.autoInstance = true;
        loader.addColliders = false;
        loader.LoadObjectGroups();

        Player = FindObjectOfType<Player>();
        Constants.positionPlayer = new Vec2(Player.x + Player.width / 2, Player.y - Player.height / 2);
        Constants.player = Player;

        Constants.levelHeight = loader.map.Height * loader.map.TileHeight;
        Constants.levelWidth = loader.map.Width * loader.map.TileWidth;



        /*
        Map map = MapParser.ReadMap(MapName);
        //ObjectGroup objectGroup = map.ObjectGroups[0];

        foreach(ObjectGroup objectGroup in map.ObjectGroups) {
            foreach (TiledObject obj in objectGroup.Objects)
            {
                Sprite newObj = null;
                switch (obj.Name)
                {
                    case "Player":
                        Player player = new Player();
                        newObj = player;
                        Constants.positionPlayer = new Vec2(obj.X + newObj.width / 2, obj.Y - newObj.height / 2);
                        Constants.player = player;
                        break;
                    case "Killer":
                        newObj = new Killer();
                        break;
                    case "Button":
                        newObj = new Button();
                        break;
                    case "Collectable":
                        newObj = new Collectable(1, false);
                        break;
                }
                if (newObj != null)
                {
                    newObj.x = obj.X + newObj.width / 2;
                    newObj.y = obj.Y - newObj.height / 2;
                    AddChild(newObj);
                }
            }
        }*/


        /*
        Killer = FindObjectOfType<Killer>();
        Player = FindObjectOfType<Player>();
        */

        //background = new Sprite("BG1.png", false, false);
        //AddChild(background);
    }

    public void HandleScroll()
    {
        if (Player == null) return;

        if (Player.x + x < Constants.xBoundarySize) x = Constants.xBoundarySize - Player.x;
        if (Player.y + y < Constants.yBoundarySize) y = Constants.yBoundarySize - Player.y;

        if (Player.x + x > game.width - Constants.xBoundarySize) x = game.width - Constants.xBoundarySize - Player.x;
        if (Player.y + y > game.height - Constants.yBoundarySize) y = game.height - Constants.yBoundarySize - Player.y;

        if (x > 0) x = 0;   // making sure the camera doesn't see the void on the left
        if (y > 0) y = 0;   // same but on top

        if (-x > Constants.levelWidth - game.width) x = -(Constants.levelWidth - game.width);   // right
        if (-y > Constants.levelHeight - game.height) y = -(Constants.levelHeight - game.height); // bottom

    }

    void checkPuzzles()
    {

    }

    public void Update()
    {

        HandleScroll();
        checkPuzzles();
        Constants.ui.updateHUD();
        foreach (Button button in Constants.buttons)
        {

            if (!button.triggered)
            {

                button.checkToggle();
            }
        }
        foreach (PressurePlate plate in Constants.plates)
        {

            if (!plate.completed)
            {

                plate.checkToggle();
            }
        }

    }

}