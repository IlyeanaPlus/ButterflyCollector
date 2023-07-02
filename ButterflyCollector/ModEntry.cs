using HarmonyLib;
using SpaceShared.APIs;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Events;
using StardewValley.Locations;
using StardewValley.TerrainFeatures;
using System.Collections.Generic;
using System;
using System.IO;
using Microsoft.Xna.Framework;
using xTile.Tiles;
using static StardewValley.Debris;

namespace Butterfly_Collector
{
    /// <summary> The mod entry point </summary>
    internal class Mod : StardewModdingAPI.Mod
    {
        /// <summary> Create instance </summary>
        public static Mod Instance;

        //JsonAssets API
        public static IJsonAssetsApi JA_API;

        //JsonAssets IDs
        public static int BlueButterflyID => JA_API.GetObjectId("IlyBlueButterfly");
        public static int BlueEmperorID => JA_API.GetObjectId("IlyBlueEmperor");
        public static int CabbageWhiteID => JA_API.GetObjectId("IlyCabbageWhite");
        public static int MonarchID => JA_API.GetObjectId("IlyMonarch");
        public static int MorningCloakID => JA_API.GetObjectId("IlyMorningCloak");
        public static int OrangeSulphurID => JA_API.GetObjectId("IlyOrangeSulphur");
        public static int PaintedLadyID => JA_API.GetObjectId("IlyPaintedLady");
        public static int TigerSwallowtailID => JA_API.GetObjectId("IlyTigerSwallowtail");

        public static Dictionary<string, List<int>> indexKeys = new Dictionary<string, List<int>>
        {
            {
                "Weeds",
                new List<int>
                {
                    343, 450, 674, 675, 676, 677, 678, 679, 784, 785,
                    786, 792, 793, 794
                }
            },
            {
                "Twig",
                new List<int> { 294, 295 }
            },
            {
                "Rock",
                new List<int> { 450, 343 }
            },
            {
                "Stump",
                new List<int> { 600 }
            },
            {
                "Log",
                new List<int> { 602 }
            },
            {
                "Boulder",
                new List<int> { 672 }
            }
        };

        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            Mod.Instance = this;

            helper.Events.GameLoop.GameLaunched += OnGameLaunched;
            helper.Events.GameLoop.DayStarted += OnDayStarted;
            helper.Events.World.DebrisListChanged += OnDebrisListChanged;
            helper.Events.World.TerrainFeatureListChanged += TerrainFeatureListChanged;


            // Set up some things
            var harmony = new Harmony(this.ModManifest.UniqueID);
            //GameLocations.Apply(harmony);
            //HarvestingCrops.Apply(harmony);
            //TerrainFeaturePatches.Apply(harmony);
            //VanillaShops.Apply(harmony);
        }

        // Load JA Stuff
        public void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {
            JA_API = Helper.ModRegistry.GetApi<IJsonAssetsApi>("spacechase0.JsonAssets");
            JA_API.LoadAssets(Path.Combine(Helper.DirectoryPath, "assets", "json-assets"), Helper.Translation);
        }

        //Test Message       
        public void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            Game1.addHUDMessage(new HUDMessage("It Kinda Works!", HUDMessage.achievement_type)); //message to help track if code is working

        }

        //Critter from Hollow Log
        private void OnDebrisListChanged(object sender, DebrisListChangedEventArgs e)
        {
            foreach (GameLocation location in Game1.locations)
            {
                foreach (Debris debris in e.Removed)
                {
                    if (location is Farm or IslandWest && debris.debrisType.Value is (DebrisType)602)
                    {
                        //Some kind of math goes here to determine drop chance

                        Game1.player.addItemToInventory(new StardewValley.Object(MorningCloakID, 1, false, -1, 0));
                        Game1.addHUDMessage(new("You found a Butterfly in the Log!", HUDMessage.achievement_type));
                        Game1.playSound("pickUpItem");
                    }
                }
            }           
        }
        

        public void TerrainFeatureListChanged(object sender, TerrainFeatureListChangedEventArgs e)
        {
            foreach (KeyValuePair<Vector2, TerrainFeature> item in e.Removed)
            {
                if (item.Value is Grass grass && grass.numberOfWeeds.Value <= 0 && grass.grassType.Value == 1)
                {
                    if (new Random((int)(Game1.uniqueIDForThisGame + item.Key.X * 1000.0 + item.Key.Y * 11.0))
                             .NextDouble() < 0.008)
                    {
                        Game1.addHUDMessage(new("You found a Butterfly in the Grass!", HUDMessage.achievement_type));
                        Game1.player.addItemToInventory(new StardewValley.Object(CabbageWhiteID, 1, false, -1, 0));
                        Game1.playSound("pickUpItem");
                    }

                }
            }
        }







    }
}