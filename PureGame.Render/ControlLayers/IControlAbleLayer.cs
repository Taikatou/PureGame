using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine;

namespace PureGame.Render.ControlLayers
{
    public interface IControlAbleLayer
    {
        void Tap(Vector2 position);
        void DoubleTap();
        void Zoom(float zoomBy);
        void Drag(Vector2 dragBy);
        void ReleaseDrag();
        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime time);
        void ControllerDPad(Direction direction);
        void Cancel(bool cancelValue);
        void Interact();
        void UnLoad();
    }
}
