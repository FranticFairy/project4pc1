using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

            /*
            loader.LoadTileLayers(0);
            loader.rootObject = this;
            loader.addColliders = false;
            loader.LoadObjectGroups(1);
            loader.LoadObjectGroups(0);
            loader.autoInstance = true;
            */

            loader.addColliders = true;
            loader.rootObject = this;
            loader.LoadTileLayers(0);
            loader.autoInstance = true;
            loader.LoadObjectGroups();

            Player = FindObjectOfType<Player>();
            Constants.positionPlayer = new Vec2(Player.x + Player.width / 2, Player.y - Player.height / 2);
            Constants.player = Player;

            loader.OnObjectCreated += handleCreate;

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
        }

        private void handleCreate(Sprite sprite, TiledObject obj)
        {
            Console.WriteLine("--");
        }

    }
}
