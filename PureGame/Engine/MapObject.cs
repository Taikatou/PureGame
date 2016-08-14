using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Maps.Tiled;
using SmallGame;
using System.Diagnostics;

namespace PureGame.Engine
{
    public class MapObject : BaseIGameObject
    {
        public string MapName;
        public MapObject()
        {
            
        }

        public TiledMap GetTiledMap(ContentManager content)
        {
            Debug.WriteLine("Load map: " + MapName);
            string file_name = string.Format("TileMaps/{0}", MapName);
            TiledMap tiled_map = content.Load<TiledMap>(file_name);
            return tiled_map;
        }
    }
}
