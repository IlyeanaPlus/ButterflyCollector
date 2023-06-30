using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System.Collections.Generic;
using HarmonyLib;
using System.IO;
using SpaceShared.APIs;

namespace ButterflyCollector

{

    /// <summary> The mod entry point </summary>
    internal sealed class ModEntry : Mod
    {
        /// <summary> Create instance </summary>
        public static Mod instance;

        // JsonAssets API
        private static SpaceShared.APIs.IJsonAssetsApi JA_API;
        private static ContentPatcher.IContentPatcherAPI CP_API;

        // JsonAssets Names
        private static string blueButterflyName = "IlyBlueButterfly";
        private static string blueEmperorName =   "IlyBlueEmperor";
        private static string cabbageWhiteName = "IlyCabbageWhite";
        private static string monarchName =  "IlyMonarch";
        private static string morningCloakName =  "IlyMorningCloak";
        private static string orangeSulphurName =  "IlyOrangeSulphur";
        private static string paintedLadyName =  "IlyPaintedLady";
        private static string tigerSwallowtailName =  "IlyTigerSwallowtail";

        // JsonAssets IDs
        public static int BlueButterflyID => JA_API.GetObjectId(blueButterflyName);
        public static int BlueEmperorID => JA_API.GetObjectId(blueEmperorName);
        public static int CabbageWhiteID => JA_API.GetObjectId(cabbageWhiteName);
        public static int MonarchID => JA_API.GetObjectId(monarchName);
        public static int MorningCloakID => JA_API.GetObjectId(morningCloakName);
        public static int OrangeSulphurID => JA_API.GetObjectId(orangeSulphurName);
        public static int PaintedLadyID => JA_API.GetObjectId(paintedLadyName);
        public static int TigerSwallowtailID => JA_API.GetObjectId(tigerSwallowtailName);

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

            // Set up some things
            var harmony = new Harmony(this.ModManifest.UniqueID);
        }

        // Load JA & CP Stuff
        private void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {
            JA_API = Helper.ModRegistry.GetApi<IJsonAssetsApi>("spacechase0.JsonAssets");
            JA_API.LoadAssets(Path.Combine(Helper.DirectoryPath, "assets", "json-assets"), Helper.Translation);

            CP_API = Helper.ModRegistry.GetApi<ContentPatcher.IContentPatcherAPI>("Pathoschild.ContentPatcher");
            ///CP_API.??????????(Path.Combine(Helper.DirectoryPath, "assets", "cpa-assets"), Helper.Translation);
        }


        private void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
        {
        }


        /// <summary>Initialize Critters</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
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