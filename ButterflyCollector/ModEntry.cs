using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System.Collections.Generic;
using Pathoschild.Stardew.Common.Integrations.JsonAssets;
using HarmonyLib;
using Butterfly_Collector;

namespace ButterflyCollector

{

    /// <summary>
    ///  The mod entry point
    /// </summary>
    public class ModEntry : Mod
    {
        /// <summary>
        /// Create instance
        /// </summary>
        public static Mod instance;

        public override void Entry(IModHelper helper)
        {

            helper.Events.GameLoop.DayStarted += this.OnDayStarted;
            helper.Events.GameLoop.GameLaunched += GameLoop_GameLaunched;

            // Do the Harmony things in Dr Elizabeth Style
            var harmony = new Harmony(this.ModManifest.UniqueID);
            MonsterPatches.Apply(harmony);
        }

        /// <summary>
        /// TEST HUD Message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            Game1.addHUDMessage(new HUDMessage("It Kinda Works!", HUDMessage.achievement_type)); //message to help track if code is working
        }
        /// <summary>
        /// Link to JA Api
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameLoop_GameLaunched(object sender, GameLaunchedEventArgs e)
        {
            var api = this.Helper.ModRegistry.GetApi<ContentPatcher.IContentPatcherAPI>("Pathoschild.ContentPatcher");
            var jsonAssets = this.Helper.ModRegistry.GetApi<IJsonAssetsApi>("spacechase0.JsonAssets");

        }

        //Havent done stuff here yet
        /// List of IDs for butterflies in grass
        internal static HashSet<int> GrassyButterflyIDs { get; } = new();

        /// List of IDs for butterflies that drop from mobs
        internal static HashSet<int> MonsterButterflyIDs { get; } = new();

    }
}