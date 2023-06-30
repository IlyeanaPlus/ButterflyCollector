using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System.Collections.Generic;
using HarmonyLib;
using System.IO;
using SpaceShared.APIs;
using System;
using StardewValley.Menus;
using StardewValley.TerrainFeatures;
using Microsoft.Xna.Framework;
using SObject = StardewValley.Object;

namespace ButterflyCollector
{
    /// <summary> The mod entry point </summary>
    internal sealed class ModEntry : Mod
    {
        /// <summary> Create instance </summary>
        public static Mod instance;

        // JsonAssets API
        private static IJsonAssetsApi JA_API;

        // JsonAssets IDs
        public static int BlueButterflyID => JA_API.GetObjectId("IlyBlueButterfly");
        public static int BlueEmperorID => JA_API.GetObjectId("IlyBlueEmperor");
        public static int CabbageWhiteID => JA_API.GetObjectId("IlyCabbageWhite");
        public static int MonarchID => JA_API.GetObjectId("IlyMonarch");
        public static int MorningCloakID => JA_API.GetObjectId("IlyMorningCloak");
        public static int OrangeSulphurID => JA_API.GetObjectId("IlyOrangeSulphur");
        public static int PaintedLadyID => JA_API.GetObjectId("IlyPaintedLady");
        public static int TigerSwallowtailID => JA_API.GetObjectId("IlyTigerSwallowtail");

        //Butterfly Type Lists
        public static Dictionary<ISalable, int[]> grassyButterflies;
        public static Dictionary<ISalable, int[]> cropButterflies;
        public static Dictionary<ISalable, int[]> shopButterflies;
        public static Dictionary<ISalable, int[]> questButterflies;
        public static Dictionary<ISalable, int[]> monsterButterflies;

        // Helper
        private static IModHelper helperStatic;


        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
            helper.Events.GameLoop.SaveCreated += this.OnSaveCreated;
            helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;
            helper.Events.GameLoop.DayStarted += this.OnDayStarted;
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;

            // Set up some things
            var harmony = new Harmony(this.ModManifest.UniqueID);

            helperStatic = helper;
        }


        // Load JA Stuff
        private void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {
            JA_API = Helper.ModRegistry.GetApi<IJsonAssetsApi>("spacechase0.JsonAssets");
            JA_API.LoadAssets(Path.Combine(Helper.DirectoryPath, "assets", "json-assets"), Helper.Translation);
        }



        // Testing Butterflies from farm grass
        private void OnSaveCreated(object sender, SaveCreatedEventArgs e)
        {
            Game1.getFarm().resourceClumps.OnValueRemoved += this.OnClumpRemoved;
        }

        private void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            Game1.getFarm().resourceClumps.OnValueRemoved += this.OnClumpRemoved;
        }

        private void OnClumpRemoved(ResourceClump value)
        {
            if (value.parentSheetIndex.Value == ResourceClump.boulderIndex)
            {
                Random r = new Random((int)value.tile.X * 1000 + (int)value.tile.Y);
                Game1.createMultipleObjectDebris(BlueEmperorID, (int)value.tile.X, (int)value.tile.Y, 7 + r.Next(15));
            }

        }

            /// <summary>Initialize Critters</summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>        
            public void OnDayStarted(object sender, DayStartedEventArgs e)
        { 

            Game1.addHUDMessage(new HUDMessage("It Kinda Works!", HUDMessage.achievement_type)); //message to help track if code is working
            grassyButterflies = getGrassyButterflies();
            cropButterflies = getCropButterflies();
            shopButterflies = getShopButterflies();
            questButterflies = getQuestButterflies();
            monsterButterflies = getMonsterButterflies();


        }
        //Grassy Butterflies
        private static Dictionary<ISalable, int[]> getGrassyButterflies()
        {
            Dictionary<ISalable, int[]> extraItems = new Dictionary<ISalable, int[]>
            {
                { new StardewValley.Object(BlueButterflyID, 1), new int[2] { 100, 1 } },
                { new StardewValley.Object(CabbageWhiteID, 1), new int[2] { 200, 1 } },
                { new StardewValley.Object(OrangeSulphurID, 1), new int[2] { 200, 1 } },
                { new StardewValley.Object(MorningCloakID, 1), new int[2] { 200, 1 } }

            };

            return extraItems;
        }

        //Crop Butterflies
        private static Dictionary<ISalable, int[]> getCropButterflies()
        {
            Dictionary<ISalable, int[]> extraItems = new Dictionary<ISalable, int[]>
            {
                { new StardewValley.Object(CabbageWhiteID, 1), new int[2] { 500, 1 } },
                { new StardewValley.Object(PaintedLadyID, 1), new int[2] { 500, 1 } },
                { new StardewValley.Object(MonarchID, 1), new int[2] { 500, 1 } }
            };
            return extraItems;
        }

        //Shop Butterflies
        private static Dictionary<ISalable, int[]> getShopButterflies()
        {
            Dictionary<ISalable, int[]> extraItems = new Dictionary<ISalable, int[]>
            {
                { new StardewValley.Object(BlueEmperorID, 1), new int[2] { 10000, 1 } }
            };
            return extraItems;
        }

        //Quest Butterflies
        private static Dictionary<ISalable, int[]> getQuestButterflies()
        {
            Dictionary<ISalable, int[]> extraItems = new Dictionary<ISalable, int[]>
            {
                { new StardewValley.Object(BlueEmperorID, 1), new int[2] { 5000, 1 } }
            };
            return extraItems;
        }

        //Monster Butterflies
        private static Dictionary<ISalable, int[]> getMonsterButterflies()
        {
            Dictionary<ISalable, int[]> extraItems = new Dictionary<ISalable, int[]>
            {
                { new StardewValley.Object(TigerSwallowtailID, 1), new int[2] { 5000, 1 } }
            };
            return extraItems;
        }
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {

            // Exit if something is in progress/player shouldn't be able to interact with things
            if (Game1.activeClickableMenu != null)
            {
                return;
            }

            if (e.Button.IsActionButton())
            {
                if ((e.Cursor.GrabTile.X == 47 || e.Cursor.GrabTile.X == 48) && e.Cursor.GrabTile.Y == 34)
                {
                    suppressClick();
                    ShopMenu ilyMapleShop  = new ShopMenu(shopButterflies, 0, "Maple Bear", null, null, "Ilyeana.MapleShopSTF");
                }



            }
        }
        private static void suppressClick()
        {
            helperStatic.Input.Suppress(Game1.options.actionButton[0].ToSButton());
            helperStatic.Input.Suppress(Game1.options.useToolButton[0].ToSButton());
            helperStatic.Input.Suppress(SButton.MouseLeft);
            helperStatic.Input.Suppress(SButton.MouseRight);
        }

    }
}