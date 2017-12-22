using SLB.WellMaps.Services;
using SLB.WellMaps.Configs;
using SLB.WellMaps.Droid.DependencyImpl;
using Xamarin.Auth;
using Xamarin.Forms;
using System.Collections.Generic;

[assembly: Xamarin.Forms.Dependency(typeof(SaveCredential))]
namespace SLB.WellMaps.Droid.DependencyImpl
{
    public class SaveCredential : ISaveCredential
    {
        public bool Save(string URL, string userName, string password)
        {
            if (!string.IsNullOrWhiteSpace(URL) && !string.IsNullOrWhiteSpace(userName))
            {
                ClearCredential();

                Account account = new Account { Username = userName };
                account.Properties.Add("Password", password);
                account.Properties.Add("URL", URL);

                try
                {
                    AccountStore.Create(Forms.Context).Save(account, AppConfig.AppName);
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
            IEnumerable<Account> accounts = AccountStore.Create(Forms.Context).FindAccountsForService(AppConfig.AppName);

            if(accounts != null)
            {
                foreach (var account in accounts)
                {
                    AccountStore.Create(Forms.Context).Delete(account, AppConfig.AppName);
                }
            }            
        }
    }
}