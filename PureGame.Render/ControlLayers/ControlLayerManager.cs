using System.Collections.Generic;

namespace PureGame.Render.ControlLayers
{
    public class ControlLayerManager
    {
        public List<SortedController> Controllers;

        public ControlLayerManager()
        {
            Controllers = new List<SortedController>();
        }

        public void AddController(IControlLayer controller)
        {
            var depth = Controllers.Count;
            var sortedController = new SortedController(controller, depth);
            Controllers.Add(sortedController);
            Sort();
        }

        public void Sort()
        {
            Controllers.Sort((node1, node2) => node1.LayerDepth.CompareTo(node2.LayerDepth));
        }

        public void AddController(IControlLayer layer, int layerDepth)
        {
            if (layerDepth < Controllers.Count)
            {
                var toRemove = Controllers[layerDepth];
                toRemove.UnLoad();
                Controllers.Remove(toRemove);
            }
            var controlLayer = new SortedController(layer, layerDepth);
            Controllers.Add(controlLayer);
            Sort();
        }
    }
}
