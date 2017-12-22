using GalaSoft.MvvmLight.Messaging;
using SLB.WellMaps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SLB.WellMaps.Views
{
    public partial class SetupPage : ContentPage
    {
        public SetupPage(Credential credential)
        {
            InitializeComponent();
            BindingContext = App.Locator.Setup;

            Messenger.Default.Register<NotificationMessage>(this, (message) =>
            {
                DisplayAlert("Notification", message.Notification, "OK");
            });

            App.Locator.Setup.UserName = credential.UserName;
            //App.Locator.Setup.Password = "MobileIron";
            App.Locator.Setup.URL = credential.URL;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Messenger.Default.Unregister<NotificationMessage>(this);
        }
    }
}
