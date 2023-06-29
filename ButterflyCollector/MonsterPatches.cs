using System.Collections.Generic;
using StardewValley;
using StardewValley.Monsters;
using StardewValley.Objects;
using StardewValley.Locations;
using StardewModdingAPI;
using HarmonyLib;
using Pathoschild.Stardew.Common.Integrations.JsonAssets;
using Microsoft.Xna.Framework;

namespace Butterfly_Collector
{
    public static class MonsterPatches
    {
        [HarmonyPatch(nameof(AddmonsterDrop))]


    }
}
