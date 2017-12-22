using GalaSoft.MvvmLight.Messaging;
using SLB.WellMaps.Models;
using Xamarin.Forms.Maps;
using Xamarin.Forms;

namespace SLB.WellMaps.Views
{
    public partial class MapPage : ContentPage
    {
        Map myMap;

        public MapPage(Credential credential)
        {
            InitializeComponent();
            BindingContext = App.Locator.Map;
            App.Locator.Map._credential = credential;

            Messenger.Default.Register<NotificationMessage>(this, (message) =>
            {
                DisplayAlert("Notification", message.Notification, "OK");
            });

            Messenger.Default.Register<Pin>(this, (pin) =>
            {
                myMap.Pins.Add(pin);
                myMap.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromMiles(.2)));
            });

            Messenger.Default.Register<MapType>(this, (type) =>
            {
                myMap.MapType = type;
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            myMap = new Map(MapSpan.FromCenterAndRadius(Configs.AppConfig.MapStartPos, Configs.AppConfig.MapStartZoom));
            myMap.IsShowingUser = true;

            Grid.SetRow(myMap, 1);
            Grid.SetColumnSpan(myMap, 5);
            mainGrid.Children.Add(myMap);

            var activityIndicator = new ActivityIndicator
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Scale = 1.0
            };

            activityIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");

            Grid.SetRow(activityIndicator, 1);
            Grid.SetColumnSpan(activityIndicator, 5);

            mainGrid.Children.Add(activityIndicator);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Messenger.Default.Unregister<NotificationMessage>(this);
            Messenger.Default.Unregister<Pin>(this);

            //App.Locator.Map.Cleanup(); 
        }
    }
}
