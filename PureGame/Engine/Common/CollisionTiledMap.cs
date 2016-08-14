using Microsoft.Xna.Framework;
using MonoGame.Extended.Maps.Tiled;
using System.Diagnostics;

namespace PureGame.Engine.Common
{
    public class CollisionTiledMap
    {
        public TiledMap TiledMap;
        public CollisionTiledMap(TiledMap TiledMap)
        {
            this.TiledMap = TiledMap;
        }

        public TiledTileLayer TileCollisionLayer
        {
            get
            {
                return TiledMap.GetLayer<TiledTileLayer>("Collision Layer");
            }
        }

        public bool CheckCollision(int x, int y)
        {
            TiledTile t = TileCollisionLayer.GetTile(x, y);
            if (t == null)
            {
                Debug.WriteLine("Tile does not exist at : " + x + ", " + y);
                return false;
            }
            else
            {
                Debug.WriteLine("Tile's id at : " + x + ", " + y + " is " + t.Id);
                return t.Id != 0;
            }
        }

        public bool CheckCollision(Vector2 position)
        {
            return CheckCollision((int)(position.X), (int)(position.Y));
        }
    }
}
