using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    internal class TiledLevel : GameObject
    {

        private Player Player;
        private Killer Killer;

        public TiledLevel(string MapName)
        {
            TiledLoader loader = new TiledLoader(MapName);
            loader.LoadTileLayers(0);
            loader.rootObject = this;
            loader.addColliders = false;
            loader.LoadObjectGroups(1);
            loader.addColliders = true;
            loader.LoadObjectGroups(0);
            loader.autoInstance = true;

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
                            newObj = new Collectable(5, false);
                            break;
                        case "Movable":
                            newObj = new Movable("circle.png", 1, 1);
                            break;
                        case "Wind":
                            newObj = new Wind("colors.png", 1, 1);
                            break;
                    }
                    if (newObj != null)
                    {
                        newObj.x = obj.X + newObj.width / 2;
                        newObj.y = obj.Y - newObj.height / 2;
                        AddChild(newObj);
                        Console.WriteLine(obj.Name);
                    }
                }
            }


            /*
            Killer = FindObjectOfType<Killer>();
            Player = FindObjectOfType<Player>();
            */
        }
    }
}
