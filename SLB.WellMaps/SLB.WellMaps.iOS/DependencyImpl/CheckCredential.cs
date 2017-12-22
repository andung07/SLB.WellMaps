using SLB.WellMaps.iOS.DependencyImpl;
using SLB.WellMaps.Services;
using SLB.WellMaps.Models;
using Xamarin.Auth;
using System.Collections.Generic;
using SLB.WellMaps.Configs;
using System.Linq;

[assembly: Xamarin.Forms.Dependency(typeof(CheckCredential))]
namespace SLB.WellMaps.iOS.DependencyImpl
{
    public class CheckCredential : ICheckCredential
    {
        public Credential Check()
        {
            IEnumerable<Account> accounts = AccountStore.Create().FindAccountsForService(AppConfig.AppName);

            if (accounts.Count() > 0)
            {
                var account = accounts.FirstOrDefault();

                return (new Credential
                {
                    UserName = account.Username.ToString(),
                    URL = account.Properties["URL"].ToString(),
                    Password = account.Properties["Password"].ToString()
                });
            }
            else
                return null;
        }
    }
}
