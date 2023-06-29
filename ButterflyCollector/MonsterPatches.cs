using StardewValley;
using StardewValley.Monsters;
using StardewValley.Locations;
using StardewValley.Objects;
using StardewModdingAPI;
using HarmonyLib;
using Microsoft.Xna.Framework;

namespace Butterfly_Collector
{
    public static class GameLocationPatches
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
                    original: AccessTools.Method(typeof(GameLocation), nameof(GameLocation.monsterDrop)),
                    postfix: new HarmonyMethod(typeof(GameLocationPatches), nameof(GameLocationPatches.MonsterDrop))
                );
            }
        }
        
        private static void  MonsterDrop(GameLocation location, Monster monster, int x, int y, Farmer who)
        {
            if (location is BugLand && monster is Grub && who is null && Game1.random.NextDouble() < 0.5)

            {
                      monster.ModifyMonsterLoot(
                        new Debris(
                                item: new Object(373, 1),
                                debrisOrigin: new Vector2(x,y),
                                targetLocation: who.Position));
                                
            }
        }

    }
}
