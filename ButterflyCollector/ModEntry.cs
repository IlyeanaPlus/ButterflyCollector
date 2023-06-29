using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System.Collections.Generic;
using System.IO;
using Pathoschild.Stardew.Common.Integrations.JsonAssets;
using HarmonyLib;
using StardewValley.Locations;
using StardewValley.Monsters;
using Microsoft.Xna.Framework;

namespace ButterflyCollector

{

    /// <summary>
    ///  The mod entry point
    /// </summary>
    public partial class ModEntry : Mod
    {
        /// <summary>
        /// Create instance
        /// </summary>
        public static Mod instance;

        public override void Entry(IModHelper helper)
        {

            helper.Events.GameLoop.DayStarted += this.OnDayStarted;
            helper.Events.GameLoop.GameLaunched += GameLoop_GameLaunched;

        }

        /// <summary>
        /// TEST HUD Message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            Game1.addHUDMessage(new HUDMessage("It Kinda Works!", HUDMessage.achievement_type));
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


        /// List of IDs for butterflies in grass
        internal static HashSet<int> GrassyButterflyIDs { get; } = new();

        /// List of IDs for butterflies that drop from mobs
        internal static HashSet<int> MonsterButterflyIDs { get; } = new();

    }
}