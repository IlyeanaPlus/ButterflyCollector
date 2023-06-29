using StardewValley;
using StardewValley.Monsters;
using StardewValley.Locations;
using StardewValley.Objects;
using StardewModdingAPI;
using HarmonyLib;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Butterfly_Collector
{
    public static class MonsterPatches
    {
       
        private static List<Item> GetExtraDropItems() 
        {
            List<Item> itemList = new List<Item>(); //create a blank list            
            if (getExtraDropItems() != null && location is BugLand)
            {
                itemList.Add(new StardewValley.Object(276, 1, false, -1, 4)); //add 1 iridium-quality pumpkin
            }
            return itemList; //return the completed list

        }

        public static void Enable(IModHelper helper)
        {
            if (enabled) //if this class is already enabled
                return; //do nothing

        }
        private static bool enabled = false;
        /// <summary>While true, this class will attempt to find and customize the mobDrops.</summary>
        private static bool editExtraDropItems = false;
    }
}
