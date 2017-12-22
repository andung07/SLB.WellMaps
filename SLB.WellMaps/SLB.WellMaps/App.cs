using SLB.WellMaps.Views;
using SLB.WellMaps.Helper;

using Xamarin.Forms;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using SLB.WellMaps.Services;
using SLB.WellMaps.Models;

namespace SLB.WellMaps
{
    public class App : Application
    {
        private static Locator _locator;

        public static Locator Locator
        {
            get { return _locator ?? (_locator = new Locator()); }
        }

        public App()
        {
            // The root page of your application

            var nav = new NavigationService();
            nav.Configure(Locator.AboutPage, typeof(AboutPage));
            nav.Configure(Locator.HomePage, typeof(HomePage));
            nav.Configure(Locator.MapPage, typeof(MapPage));
            nav.Configure(Locator.OpenTicketsPage, typeof(OpenTicketsPage));
            nav.Configure(Locator.SetupPage, typeof(SetupPage));
            nav.Configure(Locator.FeedbackPage, typeof(FeedbackPage));
            nav.Configure(Locator.TicketDetailPage, typeof(TicketDetailPage));

            SimpleIoc.Default.Register<INavigationService>(() => nav);

            var homePage = new NavigationPage(new HomePage())
            {
                BarBackgroundColor = Color.FromHex(Configs.ColorScheme.primary),
                BarTextColor = Color.FromHex(Configs.ColorScheme.text)
            };

            nav.Initialize(homePage);

            MainPage = homePage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts            
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps

            Credential cred = DependencyService.Get<ICheckCredential>().Check();
            if (cred != null)
            {
                DependencyService.Get<ISaveCredential>().Save(cred.URL, cred.UserName, "");
            }            
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        
    }
}
