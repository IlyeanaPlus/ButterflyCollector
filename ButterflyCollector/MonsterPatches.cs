using System.Collections.Generic;
using StardewValley;
using StardewValley.Monsters;
using StardewValley.Objects;
using StardewValley.Locations;
using StardewModdingAPI;
using HarmonyLib;
using Pathoschild.Stardew.Common.Integrations.JsonAssets;
using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;

namespace Butterfly_Collector
{
    public static class MonsterPatches
    {

        private static IMonitor Monitor;

        // call this method from your Entry class
        public static void Initialize(IMonitor monitor)
        {
            Monitor = monitor;
        }

        // Method to apply harmony patch
        public static void Apply(Harmony harmony)
        {
            {

                harmony.Patch(
                    original: AccessTools.Method(typeof(Monster), nameof(Monster.InitializeForLocation)),
                    postfix: new HarmonyMethod(typeof(MonsterPatches), nameof(MonsterPatches.InitializeForLocation))
                );
            }
        }
        
        private static void InitializeForLocation(GameLocation location)
        {
            if (location is BugLand);
            {

            }
        }

    }
}
