using StardewModdingAPI.Events;
using StardewValley.TerrainFeatures;
using StardewValley;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Butterfly_Collector.Definitions;
using HarmonyLib;
using StardewModdingAPI;
using StardewValley.BellsAndWhistles;
using System.Threading;
using StardewValley.Events;

namespace Butterfly_Collector.Acquisition
{
    public class TerrainFeaturePatch
    { 
        //Terrain Feature Drops (Easy Way)
        public void TerrainFeatureListChanged(object sender, TerrainFeatureListChangedEventArgs e)
        { 
            foreach (KeyValuePair<Vector2, TerrainFeature> item in e.Removed)
            {
                if (item.Value is Grass grass && grass.numberOfWeeds.Value <= 0 && grass.grassType.Value == 1)
                {
                    if (new Random((int)(Game1.uniqueIDForThisGame + item.Key.X * 1000.0 + item.Key.Y * 11.0))
                             .NextDouble() < 0.015)
                    {
                        Game1.addHUDMessage(new("You found a Butterfly in the Grass!", HUDMessage.achievement_type));
                        Game1.player.addItemToInventory(new StardewValley.Object(268, 1, false, -1, 0));
                    }

                    else if (item.Value is HoeDirt hoedirt && Game1.player.CurrentTool.Name.Contains("Hoe"))
                    {
                        if (new Random((int)(Game1.uniqueIDForThisGame + item.Key.X * 1000.0 + item.Key.Y * 11.0))
                                 .NextDouble() < 0.5)
                        {
                            Game1.addHUDMessage(new("You found a Butterfly in the Dirt!", HUDMessage.achievement_type));
                            Game1.player.addItemToInventory(new StardewValley.Object(268, 1, false, -1, 0));
                        }
                    }
                }
            }
        }
    }
}
