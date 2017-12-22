using SLB.WellMaps.WinPhoneSilverlight.DependencyImpl;
using SLB.WellMaps.Services;
using SLB.WellMaps.Models;
using SLB.WellMaps.Configs;
using Windows.Security.Credentials;
using Windows.Storage;
using System;

[assembly: Xamarin.Forms.Dependency(typeof(CheckCredential))]
namespace SLB.WellMaps.WinPhoneSilverlight.DependencyImpl
{
    public class CheckCredential : ICheckCredential
    {
        public Credential Check()
        {
            var vault = new PasswordVault();
            Credential result = new Credential();

            try
            {
                var accounts = vault.FindAllByResource(AppConfig.AppName);

                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

                if (accounts.Count > 0)
                {
                    PasswordCredential account = accounts[0];
                    account.RetrievePassword();
                    result =  (new Credential
                    {
                        UserName = account.UserName,
                        Password = account.Password.ToString(),
                        URL = localSettings.Values["URL"].ToString()
                    });
                }
            }
            catch
            {
                return null;
            }

            return result;
        }
    }
}
