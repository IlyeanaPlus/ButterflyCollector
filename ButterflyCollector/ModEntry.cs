using Butterfly_Collector.Acquisition;
using HarmonyLib;
using SpaceShared.APIs;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System.IO;


namespace Butterfly_Collector
{
    /// <summary> The mod entry point </summary>
    public class ModEntry : Mod
    {
        /// <summary> Create instance </summary>
        public static Mod instance;

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
            helper.Events.GameLoop.GameLaunched += OnGameLaunched;
            helper.Events.GameLoop.DayStarted += OnDayStarted;
            helper.Events.World.TerrainFeatureListChanged += TerrainFeatureListChanged;


            // Set up some things
            var harmony = new Harmony(this.ModManifest.UniqueID);
            //GameLocations.Apply(harmony);
            //HarvestingCrops.Apply(harmony);
            TerrainFeaturePatches.Apply(harmony);
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
        public void TerrainFeatureListChanged(object sender, TerrainFeatureListChangedEventArgs e)
        {

        }
    }
}