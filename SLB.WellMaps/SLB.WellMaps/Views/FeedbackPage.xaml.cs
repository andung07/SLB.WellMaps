using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SLB.WellMaps.Views
{
    public partial class FeedbackPage : ContentPage
    {
        public FeedbackPage()
        {
            InitializeComponent();
            //BindingContext = App.Locator.Feedback;

            Messenger.Default.Register<NotificationMessage>(this, (message) =>
            {
                DisplayAlert("Notification", message.Notification, "OK");
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Messenger.Default.Unregister<NotificationMessage>(this);
        }
    }
}
