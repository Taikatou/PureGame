using Microsoft.Xna.Framework;
using MonoGame.Extended;
using PureGame.Common;
using PureGame.Render.Renderable.WorldRenderer;
using System;
using System.Collections.Generic;

namespace PureGame.Render.Renderable
{
    public class FocusStack
    {
        public Stack<IFocusable> Stack;
        public Camera2D Camera;
        private bool _moving;
        public IFocusable Focus => Stack.Peek();

        public event EventHandler RefreshEvent;

        public FocusStack(Camera2D camera)
        {
            Camera = camera;
            Stack = new Stack<IFocusable>();
        }

        public void Push(IFocusable focusEntity)
        {
            Stack.Push(focusEntity);
        }

        public void Pop()
        {
            Stack.Pop();
        }

        public void BeginMove()
        {
            if (!_moving)
            {
                _moving = true;
                var position = Focus.Position;
                Push(new FocusVector(position));
            }
        }

        public void EndMove()
        {
            if (_moving)
            {
                Pop();
                _moving = false;
                RefreshEvent?.Invoke(this, null);
            }
        }

        public void MoveFocusBy(Vector2 moveBy)
        {
            if (_moving)
            {
                var focusVector = Focus as FocusVector;
                if (focusVector != null)
                {

                    moveBy /= Camera.Zoom;
                    focusVector.Position -= moveBy;
                    RefreshEvent?.Invoke(this, null);
                }
            }
        }
    }
}
