using GalaSoft.MvvmLight.Messaging;
using SLB.WellMaps.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace SLB.WellMaps.Views
{
    public partial class TicketDetailPage : ContentPage
    {
        Map myMap;

        public TicketDetailPage(TicketPageParam param)
        {
            InitializeComponent();
            BindingContext = App.Locator.TicketDetail;

            App.Locator.TicketDetail.Param = param;

            Messenger.Default.Register<NotificationMessage>(this, (message) =>
            {
                DisplayAlert("Notification", message.Notification, "OK");
            });

            Messenger.Default.Register<Pin>(this, (pin) =>
            {
                myMap.Pins.Add(pin);
                myMap.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromMiles(.2)));
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            myMap = new Map(MapSpan.FromCenterAndRadius(Configs.AppConfig.MapStartPos, Configs.AppConfig.MapStartZoom));
            myMap.MapType = MapType.Hybrid;
            myMap.IsShowingUser = true;

            Grid.SetRow(myMap, 0);
            Grid.SetColumnSpan(myMap, 5);
            mainGrid.Children.Add(myMap);

            App.Locator.TicketDetail.GetWellDetail();

            foreach (var x in App.Locator.TicketDetail.TicketCrewInCharge)
            {
                Label label = new Label()
                {
                    Text = x.Name,
                    HorizontalOptions = LayoutOptions.End
                };

                crewHolder.Children.Add(label);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Messenger.Default.Unregister<NotificationMessage>(this);
            Messenger.Default.Unregister<Pin>(this);
        }
    }
}
