using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using SLB.WellMaps.Services;
using SLB.WellMaps.Models;
using System;
using System.Windows.Input;
using System.Threading;
using Xamarin.Forms;
using System.Diagnostics;

using System.Net.Http;
using ModernHttpClient;

namespace SLB.WellMaps.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private Credential _credential;

        public ICommand NavOpenTicketsPageCmd { get; set; }
        public ICommand NavAboutPageCmd { get; set; }
        public ICommand NavMapPageCmd { get; set; }
        public ICommand NavSetupPageCmd { get; set; }

        //Test MI
        //public ICommand TestCommand { get; set; }        
        //private string _urlTest;
        //public string URLTest 
        //{
        //    get { return _urlTest; }
        //    set {
        //        _urlTest = value;
        //        RaisePropertyChanged(() => URLTest);
        //    }
        //}

        public string Title { get; private set; }

        public HomePageViewModel(INavigationService navigationService)
        {
            Title = "Homepage";

            if (navigationService == null)
                throw new ArgumentNullException("navigationService");

            _navigationService = navigationService;

            NavOpenTicketsPageCmd = new RelayCommand(() => { _navigationService.NavigateTo(Locator.OpenTicketsPage, _credential ?? new Credential()); });
            NavAboutPageCmd = new RelayCommand(() => { _navigationService.NavigateTo(Locator.AboutPage); });
            NavMapPageCmd = new RelayCommand(() => { _navigationService.NavigateTo(Locator.MapPage, _credential ?? new Credential()); });
            NavSetupPageCmd = new RelayCommand(() => { _navigationService.NavigateTo(Locator.SetupPage, _credential ?? new Credential()); });
            //NavFeedbackPageCmd = new RelayCommand(() => { _navigationService.NavigateTo(Locator.FeedbackPage); });

            //var popup = 

            //TestCommand = new RelayCommand(async () =>
            //{                
            //    try
            //    {
            //        HttpClient client = new HttpClient(new NativeMessageHandler { DisableCaching = true });
            //        var response = await client.GetAsync(_urlTest);
            //        Messenger.Default.Send(new NotificationMessage(await response.Content.ReadAsStringAsync()));
            //    }

            //    catch(Exception e)
            //    {
            //        Messenger.Default.Send(new NotificationMessage(e.Message));
            //    }
               
            //});

            
        }
      
        public void GetCredential()
        {
            _credential = DependencyService.Get<ICheckCredential>().Check();                       

            if (_credential == null || _credential.Password == "")
            {
                Messenger.Default.Send(new NotificationMessage("Configuration incomplete. Please go to setup!"));
            }            
        }

    }
}
