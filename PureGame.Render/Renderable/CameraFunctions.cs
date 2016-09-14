using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace PureGame.Render.Renderable
{
    public class CameraFunctions
    {
        public static bool CamerasContains(Rectangle r, Camera2D camera, Camera2D tmpCamera)
        {
            var cameraContains = camera.Contains(r) != ContainmentType.Disjoint ||
                                 tmpCamera.Contains(r) != ContainmentType.Disjoint;
            return cameraContains;
        }
    }
}
