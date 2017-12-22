using SLB.WellMaps.WinPhoneSilverlight.Resources;

namespace SLB.WellMaps.WinPhoneSilverlight
{
    /// <summary>
    /// Provides access to string resources.
    /// </summary>
    public class LocalizedStrings
    {
        private static AppResources _localizedResources = new AppResources();

        public AppResources LocalizedResources { get { return _localizedResources; } }
    }
}