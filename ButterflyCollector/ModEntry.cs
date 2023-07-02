using HarmonyLib;
using SpaceShared.APIs;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Locations;
using StardewValley.TerrainFeatures;
using System.Collections.Generic;
using System;
using System.IO;
using Microsoft.Xna.Framework;
using System.Linq;
using Netcode;

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

       

        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            Mod.Instance = this;

            helper.Events.GameLoop.GameLaunched += OnGameLaunched;
            helper.Events.GameLoop.DayStarted += OnDayStarted;
            helper.Events.World.LargeTerrainFeatureListChanged += OnLargeTerrainFeatureListChanged;
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
            Game1.addHUDMessage(new HUDMessage("Good Luck!", HUDMessage.achievement_type)); //message to help track if code is working

        }

        //Critter from Bush
        private void OnLargeTerrainFeatureListChanged(object sender, LargeTerrainFeatureListChangedEventArgs e)
        {
           foreach (LargeTerrainFeature ltf in e.Removed)
            {
                if (e.Location is Farm or IslandWest && ltf is Bush)
                {
                    //Some kind of math goes here to determine drop chance

                    Game1.player.addItemToInventory(new StardewValley.Object(MorningCloakID, 1, false, -1, 0));
                    Game1.addHUDMessage(new("You found a Butterfly in the Bushes!", HUDMessage.achievement_type));
                    Game1.playSound("pickUpItem");
                }
            }
        }
        //Critters from Grass, Trees, (ResourceClumps?)
        public void TerrainFeatureListChanged(object sender, TerrainFeatureListChangedEventArgs e) //Event.World from SMAPI
        {
            foreach (KeyValuePair<Vector2, TerrainFeature> item in e.Removed)
            {
                if (e.Location is Farm && item.Value is Grass grass && grass.numberOfWeeds.Value <= 0 && grass.grassType.Value == 1) //Grass Daytime (default)
                {
                    if (new Random((int)(Game1.uniqueIDForThisGame + item.Key.X * 1000.0 + item.Key.Y * 11.0))
                            .NextDouble() < 0.008) //0.8% chance to drop PER grass node
                    {
                        Game1.addHUDMessage(new("You found a Butterfly in the Grass!", HUDMessage.achievement_type)); //Might remove message later
                        Game1.player.addItemToInventory(new StardewValley.Object(CabbageWhiteID, 1, false, -1, 0)); //Butterfly to drop
                        Game1.playSound("pickUpItem");
                    }
                 
                }
                else if (e.Location is Farm && item.Value is Tree tree && tree.growthStage.Value == 5 && tree.treeType.Value == 1) //Tree 1 is Oak
                {
                    if (new Random((int)(Game1.uniqueIDForThisGame + (item.Key.X * 1000.0) + (item.Key.Y * 11.0)))
                            .NextDouble() < 0.5) //50% chance to drop from the Oak tree
                    {
                        Game1.addHUDMessage(new("You found a Butterfly in the Oak Tree!", HUDMessage.achievement_type)); //Might remove message later
                        Game1.player.addItemToInventory(new StardewValley.Object(MonarchID, 1, false, -1, 0)); //Butterfly to drop
                        Game1.playSound("pickUpItem");
                    }
                }
                else if (e.Location is Farm && item.Value is Tree tree2 && tree2.growthStage.Value == 5 && tree2.treeType.Value == 2) //Tree 2 is Maple
                {
                    if (new Random((int)(Game1.uniqueIDForThisGame + item.Key.X * 1000.0 + item.Key.Y * 11.0))
                            .NextDouble() < 0.5) //50% chance to drop from the Maple tree
                    {
                        Game1.addHUDMessage(new("You found a Butterfly in the Maple Tree!", HUDMessage.achievement_type)); //Might remove message later
                        Game1.player.addItemToInventory(new StardewValley.Object(TigerSwallowtailID, 1, false, -1, 0)); //Butterfly to drop
                        Game1.playSound("pickUpItem");
                    }
                }
                else if (e.Location is Farm && item.Value is Tree tree3 && tree3.growthStage.Value == 5 && tree3.treeType.Value == 3) //Tree 3 is Pine
                {
                    if (new Random((int)(Game1.uniqueIDForThisGame + item.Key.X * 1000.0 + item.Key.Y * 11.0))
                            .NextDouble() < 0.5) //50% chance to drop from the Pine tree
                    {
                        Game1.addHUDMessage(new("You found a Butterfly in the Pine Tree!", HUDMessage.achievement_type)); //Might remove message later
                        Game1.player.addItemToInventory(new StardewValley.Object(PaintedLadyID, 1, false, -1, 0)); //Butterfly to drop
                        Game1.playSound("pickUpItem");
                    }
                }
                else if (e.Location is Desert && item.Value is Tree tree6 && tree6.growthStage.Value == 5 && tree6.treeType.Value == 6) //Tree 6 is Desert Palm
                {
                    if (new Random((int)(Game1.uniqueIDForThisGame + item.Key.X * 1000.0 + item.Key.Y * 11.0))
                            .NextDouble() < 0.20) //20% chance to drop from the Desert Palm tree
                    {
                        Game1.addHUDMessage(new("You found a Butterfly in the Desert Palm Tree!", HUDMessage.achievement_type)); //Might remove message later
                        Game1.player.addItemToInventory(new StardewValley.Object(OrangeSulphurID, 1, false, -1, 0)); //Butterfly to drop
                        Game1.playSound("pickUpItem");
                    }
                }
                else if (e.Location is Farm or IslandWest or IslandNorth && item.Value is Tree tree8 && tree8.growthStage.Value == 5 && tree8.treeType.Value == 8) //Tree 8 is Mahogany
                {
                    if (new Random((int)(Game1.uniqueIDForThisGame + item.Key.X * 1000.0 + item.Key.Y * 11.0))
                            .NextDouble() < 0.10) //10% chance to drop from the Mahogany tree
                    {
                        Game1.addHUDMessage(new("You found a Butterfly in the Mahogany Tree!", HUDMessage.achievement_type)); //Might remove message later
                        Game1.player.addItemToInventory(new StardewValley.Object(BlueEmperorID, 1, false, -1, 0)); //Butterfly to drop
                        Game1.playSound("pickUpItem");
                    }
                }
                else if (e.Location is IslandWest or IslandNorth && item.Value is Tree tree9 && tree9.treeType.Value == 9) //Tree 9 is Island Palm
                {
                    if (new Random((int)(Game1.uniqueIDForThisGame + item.Key.X * 1000.0 + item.Key.Y * 11.0))
                            .NextDouble() < 0.10) //10% chance to drop from the Island Palm tree
                    {
                        Game1.addHUDMessage(new("You found a Butterfly in the Island Palm Tree!", HUDMessage.achievement_type)); //Might remove message later
                        Game1.player.addItemToInventory(new StardewValley.Object(BlueEmperorID, 1, false, -1, 0)); //Butterfly to drop
                        Game1.playSound("pickUpItem");
                    }
                }


            }


        }







    }
}