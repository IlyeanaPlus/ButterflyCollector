using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System.Collections.Generic;
using Pathoschild.Stardew.Common.Integrations.JsonAssets;
using SpaceShared.APIs;
using HarmonyLib;

namespace ButterflyCollector

{

    /// <summary> The mod entry point </summary>
    public class ModEntry : Mod
    {
        /// <summary> Create instance </summary>
        public static Mod instance;

        //Butterfly Type Lists
        private static Dictionary<ISalable, int[]> grassyButterflies;
        private static Dictionary<ISalable, int[]> cropButterflies;
        private static Dictionary<ISalable, int[]> shopButterflies;
        private static Dictionary<ISalable, int[]> questButterflies;
        private static Dictionary<ISalable, int[]> monsterButterflies;

        //Useful Strings
        private static string jsonAssetsModID = "Ilyeana.ButterflyCollectorJA";
        private static string contentPackMoDID = "Ilyeana.ButterflyCollectorCP";

        // JsonAssets item names
        private static string blue = jsonAssetsModID + "/IlyBlue";
        private static string blueEmperor = jsonAssetsModID + "/IlyBlueEmperor";
        private static string cabbageWhite = jsonAssetsModID + "/IlyCabbageWhite";
        private static string monarch= jsonAssetsModID + "/IlyMonarch";
        private static string morningCloak = jsonAssetsModID + "/IlyMorningCloak";
        private static string orangeSulphur = jsonAssetsModID + "/IlyOrangeSulphur";
        private static string paintedLady = jsonAssetsModID + "/IlyPaintedLady";
        private static string tigerSwallowtail = jsonAssetsModID + "/IlyTigerSwallowtail";

        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;
            helper.Events.GameLoop.DayStarted += this.OnDayStarted;
            var harmony = new Harmony(this.ModManifest.UniqueID);
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
            Dictionary<ISalable, int[]> extraItems = new Dictionary<ISalable, int[]>();
            extraItems.Add((ISalable)      (cabbageWhite), new int[2] { 50000, 1 });
            return extraItems;
        }
        //Crop Butterflies
        private static Dictionary<ISalable, int[]> getCropButterflies()
        {
            Dictionary<ISalable, int[]> extraItems = new Dictionary<ISalable, int[]>();
            extraItems.Add(new StardewValley.Object(268, 1), new int[2] { 5000, 1 });
            return extraItems;
        }
        //Shop Butterflies
        private static Dictionary<ISalable, int[]> getShopButterflies()
        {
            Dictionary<ISalable, int[]> extraItems = new Dictionary<ISalable, int[]>();
            extraItems.Add(new StardewValley.Object(268, 1), new int[2] { 5000, 1 });
            return extraItems;
        }
        //Quest Butterflies
        private static Dictionary<ISalable, int[]> getQuestButterflies()
        {
            Dictionary<ISalable, int[]> extraItems = new Dictionary<ISalable, int[]>();
            extraItems.Add(new StardewValley.Object(268, 1), new int[2] { 5000, 1 });
            return extraItems;
        }
        //Monster Butterflies
        private static Dictionary<ISalable, int[]> getMonsterButterflies()
        {
            Dictionary<ISalable, int[]> extraItems = new Dictionary<ISalable, int[]>();
            extraItems.Add(new StardewValley.Object(268, 1), new int[2] { 5000, 1 });
            return extraItems;
        }
    }
}