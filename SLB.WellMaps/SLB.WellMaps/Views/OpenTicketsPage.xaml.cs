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
    public partial class OpenTicketsPage : ContentPage
    {
        public OpenTicketsPage(Credential credential)
        {
            InitializeComponent();
            BindingContext = App.Locator.OpenTickets;

            Messenger.Default.Register<NotificationMessage>(this, (message) =>
            {
                DisplayAlert("Notification", message.Notification, "OK");
            });

            App.Locator.OpenTickets.Credential = credential;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.Locator.OpenTickets.LoadTickets();
        }        

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Messenger.Default.Unregister<NotificationMessage>(this);           
        }
    }
}
