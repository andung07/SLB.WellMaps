using GalaSoft.MvvmLight.Messaging;
using Xamarin.Forms;

namespace SLB.WellMaps.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = App.Locator.Home;

            Messenger.Default.Register<NotificationMessage>(this, (message) =>
            {
                DisplayAlert("Notification", message.Notification, "OK");
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.Locator.Home.GetCredential();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Messenger.Default.Unregister<NotificationMessage>(this);
        }
        
    }
}
