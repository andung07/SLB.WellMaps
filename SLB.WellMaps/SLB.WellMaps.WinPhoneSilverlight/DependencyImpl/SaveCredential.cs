using SLB.WellMaps.Services;
using SLB.WellMaps.Configs;
using SLB.WellMaps.WinPhoneSilverlight.DependencyImpl;
using Windows.Security.Credentials;
using Windows.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(SaveCredential))]
namespace SLB.WellMaps.WinPhoneSilverlight.DependencyImpl
{
    public class SaveCredential : ISaveCredential
    {
        public bool Save(string URL, string userName, string password)
        {
            if (!string.IsNullOrWhiteSpace(URL) && !string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password))
            {
                var vault = new PasswordVault();
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

                try
                {
                    vault.Add(new PasswordCredential(AppConfig.AppName, userName, password));
                    localSettings.Values["URL"] = URL;
                }
                catch(System.Exception)
                {
                    return false;
                }
                return true;
            }
            else
                return false;
        }
    }
}
