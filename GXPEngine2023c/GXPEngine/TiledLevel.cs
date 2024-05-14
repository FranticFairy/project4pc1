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
            loader.addColliders = true;
            loader.LoadObjectGroups();

            Map map = MapParser.ReadMap(MapName);
            ObjectGroup objectGroup = map.ObjectGroups[0];

            foreach(TiledObject obj in objectGroup.Objects)
            {
                Sprite newObj = null;
                switch(obj.Name)
                {
                    case "Player":
                        Player Player = new Player();
                        newObj = Player;
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
                }
                if(newObj != null)
                {
                    newObj.x = obj.X + newObj.width / 2;
                    newObj.y = obj.Y - newObj.height / 2;
                    AddChild(newObj);
                }
            }
            /*
            Killer = FindObjectOfType<Killer>();
            Player = FindObjectOfType<Player>();
            */
        }
    }
}
