using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Maps.Tiled;
using PureGame.SmallGame;
using System.Diagnostics;

namespace PureGame.Engine
{
    public class MapObject : IGameObject
    {
        public string MapName;
        public string Id { get; set; }
        public string Type { get; set; }
        public string CollisionLayerName;
        public TiledMap Map;
        public TiledTileLayer TileCollisionLayer;
        private readonly ContentManager _content;

        public bool CheckCollision(int x, int y)
        {
            var tile = TileCollisionLayer.GetTile(x, y);
            return !(tile == null || tile.Id == 0);
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
