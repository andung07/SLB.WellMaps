using SLB.WellMaps.Services;
using SLB.WellMaps.Configs;
using SLB.WellMaps.iOS.DependencyImpl;
using Xamarin.Auth;
using System.Collections.Generic;

[assembly: Xamarin.Forms.Dependency(typeof(SaveCredential))]
namespace SLB.WellMaps.iOS.DependencyImpl
{
    public class SaveCredential : ISaveCredential
    {
        public bool Save(string URL, string userName, string password)
        {
            if (!string.IsNullOrWhiteSpace(URL) && !string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password))
            {
                ClearCredential();

                Account account = new Account { Username = userName };
                account.Properties.Add("Password", password);
                account.Properties.Add("URL", URL);

                try
                {
                    AccountStore.Create().Save(account, AppConfig.AppName);
                }
                catch (System.Exception)
                {
                    return false;
                }
                return true;
            }
            else
                return false;
        }

        private void ClearCredential()
        {
            IEnumerable<Account> accounts = AccountStore.Create().FindAccountsForService(AppConfig.AppName);

            if (accounts != null)
            {
                foreach (var account in accounts)
                {
                    AccountStore.Create().Delete(account, AppConfig.AppName);
                }
            }
        }
    }
}
