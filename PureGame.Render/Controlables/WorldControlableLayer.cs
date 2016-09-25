using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine;
using PureGame.Engine.World.EntityMover;
using PureGame.Render.Renderable.WorldRenderer;

namespace PureGame.Render.Controlables
{
    public class WorldControlableLayer : IControlableLayer
    {
        public WorldRenderLayer Renderer;
        public PureGameClient Client;

        public WorldControlableLayer(WorldRenderLayer renderer, PureGameClient client)
        {
            Renderer = renderer;
            Client = client;
        }
        public bool Tap(Vector2 position)
        {
            var entityMover = GetEntitymover();
            if (entityMover == null || entityMover.Complete)
            {
                Client.Entity.Running = false;
            }
            var endPosition = Renderer.WorldPosition(position);
            var entity = Client.Entity;
            Client.PureGame.AddEntityMover(entity, endPosition);
            return false;
        }

        public void DoubleTap()
        {
            var entityMover = GetEntitymover();
            if (entityMover != null)
            {
                entityMover.Entity.Running = true;
            }
        }

        public void Drag(Vector2 dragBy)
        {
            Renderer.FocusStack.MoveFocusBy(dragBy);
        }

        public void ReleaseDrag()
        {
            Renderer.FocusStack.EndMove();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Renderer.Draw(spriteBatch);
        }

        public void Update(GameTime time)
        {
            Renderer.Update(time);
        }

        public void ControllerDPad(Direction direction)
        {
            Client.ControllerDPad(direction);
        }

        public void Cancel(bool cancelValue)
        {
            Client.Running = cancelValue;
        }

        public bool Interact()
        {
            return Client.Interact();
        }

        public void UnLoad()
        {
            Renderer.UnLoad();
        }

        public void Zoom(float zoomBy)
        {
            var camera = Renderer.Camera;
            var zoom = camera.Zoom;
            zoom += zoomBy;
            if (zoom >= camera.MinimumZoom && zoom <= camera.MaximumZoom)
            {
                camera.Zoom = zoom;
                Renderer.RefreshToDraw();
            }
        }

        public EntityMover GetEntitymover()
        {
            var entityDict = Client.PureGame.EntitiyMover.EntityMoverDict;
            var player = Client.Entity;
            EntityMover toReturn = null;
            if (entityDict.ContainsKey(player))
            {
                toReturn = entityDict[player];
            }
            return toReturn;
        }
    }
}
