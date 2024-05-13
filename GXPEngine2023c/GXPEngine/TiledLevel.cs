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

        private Player TestGuy;

        public TiledLevel(string MapName)
        {
            TiledLoader loader = new TiledLoader(MapName);
            loader.LoadTileLayers(0);
            loader.rootObject = this;
            loader.addColliders = true;
            loader.LoadTileLayers(1);
            loader.autoInstance = true;
            loader.LoadObjectGroups();

            TestGuy = FindObjectOfType<Player>();

        }
    }
}
