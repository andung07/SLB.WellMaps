using Xamarin.Forms.Maps;

namespace SLB.WellMaps.Configs
{
    public static class AppConfig
    {
        public readonly static string AppName = "SLB WellMaps";
        public readonly static Position MapStartPos = new Position(0.885742023, 101.36338);
        public static Distance MapStartZoom = Distance.FromMiles(12);
        public static Distance MapMoveToRegion = Distance.FromMiles(0.3);
    }
}
