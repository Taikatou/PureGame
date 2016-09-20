using System.Collections.Generic;

namespace PureGame.Render.ControlLayers
{
    public class ControlLayerManager
    {
        public Dictionary<int, IControlAbleLayer> ControlableDict;
        public List<IControlAbleLayer> ControlLayers;

        public ControlLayerManager()
        {
            ControlableDict = new Dictionary<int, IControlAbleLayer>();
            ControlLayers = new List<IControlAbleLayer>();
        }

        public void AddController(IControlAbleLayer layer, int layerDepth)
        {
            if (ControlableDict.ContainsKey(layerDepth))
            {
                var toRemove = ControlableDict[layerDepth];
                ControlLayers.Remove(toRemove);
                toRemove.UnLoad();
            }
            ControlableDict[layerDepth] = layer;
            ControlLayers.Add(layer);
        }
    }
}
