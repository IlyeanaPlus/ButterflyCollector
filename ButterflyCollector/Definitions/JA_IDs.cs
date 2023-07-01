using SpaceShared.APIs;

namespace Butterfly_Collector.Definitions
{
    public class JA_IDs
    {
        //JsonAssets API
        private static IJsonAssetsApi JA_API;

        //JsonAssets IDs
        public static int BlueButterflyID => JA_API.GetObjectId("IlyBlueButterfly");
        public static int BlueEmperorID => JA_API.GetObjectId("IlyBlueEmperor");
        public static int CabbageWhiteID => JA_API.GetObjectId("IlyCabbageWhite");
        public static int MonarchID => JA_API.GetObjectId("IlyMonarch");
        public static int MorningCloakID => JA_API.GetObjectId("IlyMorningCloak");
        public static int OrangeSulphurID => JA_API.GetObjectId("IlyOrangeSulphur");
        public static int PaintedLadyID => JA_API.GetObjectId("IlyPaintedLady");
        public static int TigerSwallowtailID => JA_API.GetObjectId("IlyTigerSwallowtail");

    }
}
