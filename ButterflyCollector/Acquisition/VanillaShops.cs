using StardewModdingAPI.Events;
using StardewModdingAPI;
using StardewValley.Menus;
using StardewValley;
using System.Collections.Generic;
using Butterfly_Collector.Definitions;
using SpaceShared.APIs;

namespace Butterfly_Collector.Acquisition
{
    internal class VanillaShops
    {

        //Butterfly Type Lists
        public static Dictionary<ISalable, int[]> grassyButterflies;
        public static Dictionary<ISalable, int[]> cropButterflies;
        public static Dictionary<ISalable, int[]> shopButterflies;
        public static Dictionary<ISalable, int[]> questButterflies;
        public static Dictionary<ISalable, int[]> monsterButterflies;


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


    //Adding Critters to Shops (?) Maybe
    private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
    {
        // Exit if something is in progress/player shouldn't be able to interact with things
        if (Game1.activeClickableMenu != null)
        {
            return;
        }

        if (e.Button.IsActionButton())
        {
            if ((e.Cursor.GrabTile.X == 47 || e.Cursor.GrabTile.X == 48) && e.Cursor.GrabTile.Y == 34)
            {
                suppressClick();
                ShopMenu ilyMapleShop = new ShopMenu(shopButterflies, 0, "Maple Bear", null, null, "Ilyeana.MapleShopSTF");
            }

        }
    }
    //Something from Firework Festival to stop clicking when you shouldnt be able to?
    private static void suppressClick()
    {
        helperStatic.Input.Suppress(Game1.options.actionButton[0].ToSButton());
        helperStatic.Input.Suppress(Game1.options.useToolButton[0].ToSButton());
        helperStatic.Input.Suppress(SButton.MouseLeft);
        helperStatic.Input.Suppress(SButton.MouseRight);
    }
}
}
