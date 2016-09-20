﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PureGame.Render.Controlables
{
    public class ControlLayerManager
    {
        // from biggest to smallest
        public Dictionary<int, SortedControlableLayer> ControlableDict;
        public List<SortedControlableLayer> ControlLayers;

        public ControlLayerManager()
        {
            ControlableDict = new Dictionary<int, SortedControlableLayer>();
            ControlLayers = new List<SortedControlableLayer>();
        }

        public void AddController(IControlableLayer layer, int layerDepth)
        {
            if (ControlableDict.ContainsKey(layerDepth))
            {
                var toRemove = ControlableDict[layerDepth];
                ControlLayers.Remove(toRemove);
                toRemove.UnLoad();
            }
            var sortedLayer = new SortedControlableLayer(layer, layerDepth);
            ControlableDict[layerDepth] = sortedLayer;

            ControlLayers.Add(sortedLayer);
            ControlLayers.Sort((layer1, layer2) => layer2.LayerDepth.CompareTo(layer1.LayerDepth));
        }

        public void Update(GameTime time)
        {
            foreach (var controlLayer in ControlLayers)
            {
                controlLayer.Update(time);
            }
        }
    }
}
