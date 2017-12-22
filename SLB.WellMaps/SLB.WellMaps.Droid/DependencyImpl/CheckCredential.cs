using SLB.WellMaps.Services;
using SLB.WellMaps.Models;
using SLB.WellMaps.Droid.DependencyImpl;
using SLB.WellMaps.Configs;
using Xamarin.Forms;
using Xamarin.Auth;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(CheckCredential))]
namespace SLB.WellMaps.Droid.DependencyImpl
{
    public class CheckCredential : ICheckCredential
    {
        public Credential Check()
        {
            IEnumerable<Account> accounts = AccountStore.Create(Forms.Context).FindAccountsForService(AppConfig.AppName);

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