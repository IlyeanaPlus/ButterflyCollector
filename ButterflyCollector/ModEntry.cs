using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using System.Collections.Generic;
using Pathoschild.Stardew.Common.Integrations.JsonAssets;
using HarmonyLib;
using System;
using System.IO;
using SpaceShared.APIs;
using System.Net;

namespace ButterflyCollector

{

    /// <summary> The mod entry point </summary>
    public class ModEntry : Mod
    {
        /// <summary> Create instance </summary>
        public static Mod instance;

        // JsonAssets API
        private static SpaceShared.APIs.IJsonAssetsApi JA_API;

        //Useful Strings
        private static string jsonAssetsModID = "Ilyeana.ButterflyCollectorJA";
        private static string contentPackMoDID = "Ilyeana.ButterflyCollectorCP";

        // JsonAssets Names
        private static string blueButterflyName = jsonAssetsModID + "Blue Butterfly";
        private static string blueEmperorName = jsonAssetsModID + "Blue Emperor Butterfly";
        private static string cabbageWhiteName = jsonAssetsModID + "Cabbage White Butterfly";
        private static string monarchName = jsonAssetsModID + "Monarch Butterfly";
        private static string morningCloakName = jsonAssetsModID + "Morning Cloak Butterfly";
        private static string orangeSulphurName = jsonAssetsModID + "Orange Suphur Butterfly";
        private static string paintedLadyName = jsonAssetsModID + "Painted Lady Butterfly";
        private static string tigerSwallowtailName = jsonAssetsModID + "Tiger Swallowtail Butterfly";

        // JsonAssets IDs
        public static int BlueButterflyID = JA_API.GetObjectId(blueButterflyName);
        public static int BlueEmperorID = JA_API.GetObjectId(blueEmperorName);
        public static int CabbageWhiteID = JA_API.GetObjectId(cabbageWhiteName);
        public static int MonarchID = JA_API.GetObjectId(monarchName);
        public static int MorningCloakID = JA_API.GetObjectId(morningCloakName);
        public static int OrangeSulphurID = JA_API.GetObjectId(orangeSulphurName);
        public static int PaintedLadyID = JA_API.GetObjectId(paintedLadyName);
        public static int TigerSwallowtailID = JA_API.GetObjectId(tigerSwallowtailName);

        //Butterfly Type Lists
        private static Dictionary<ISalable, int[]> grassyButterflies;
        private static Dictionary<ISalable, int[]> cropButterflies;
        private static Dictionary<ISalable, int[]> shopButterflies;
        private static Dictionary<ISalable, int[]> questButterflies;
        private static Dictionary<ISalable, int[]> monsterButterflies;


        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
            helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;
            helper.Events.GameLoop.DayStarted += this.OnDayStarted;
            var harmony = new Harmony(this.ModManifest.UniqueID);
        }

        private void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {

        }


        /// <summary> Test Message & Initialize critters</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            this.Helper.ModRegistry.GetApi<ContentPatcher.IContentPatcherAPI>("Pathoschild.ContentPatcher");
            this.Helper.ModRegistry.GetApi<SpaceShared.APIs.IJsonAssetsApi>("spacechase0.JsonAssets");
        }

         
        private void OnDayStarted(object sender, DayStartedEventArgs e)
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
    }
}