using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Maps.Tiled;
using PureGame.SmallGame;
using System.Diagnostics;

namespace PureGame.Engine
{
    public class MapObject : GameObject
    {
        public string MapName;

        public string CollisionLayerName;

        public TiledMap Map;

        private readonly ContentManager _content;

        public TiledTileLayer TileCollisionLayer;

        public bool CheckCollision(int x, int y)
        {
            TiledTile t = TileCollisionLayer.GetTile(x, y);
            if (t == null)
            {
                Debug.WriteLine("Tile does not exist at : " + x + ", " + y);
            }
            else
            {
                Debug.WriteLine("Tile's id at : " + x + ", " + y + " is " + t.Id);
            }
            return !(t == null || t.Id == 0);
        }

        public bool CheckCollision(Vector2 position)
        {
            return CheckCollision((int)(position.X), (int)(position.Y));
        }

        public MapObject()
        {
            _content = ContentManagerManager.RequestContentManager();
        }

        public void OnInit()
        {
            Debug.WriteLine("Load map: " + MapName);
            string fileName = $"TileMaps/{MapName}";
            Map = _content.Load<TiledMap>(fileName);
            TileCollisionLayer =  Map.GetLayer<TiledTileLayer>(CollisionLayerName);
        }

        public void UnLoad()
        {
            _content.Unload();
        }
    }
}
