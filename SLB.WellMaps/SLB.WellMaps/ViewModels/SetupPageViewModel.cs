using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using SLB.WellMaps.Models;
using SLB.WellMaps.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SLB.WellMaps.ViewModels
{
    public class SetupPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private string _userName, _password, _url;

        public string Title { get; private set; }
        public ICommand SaveCommand { get; set; }

        public string UserName
        {
            get 
            { 
                return _userName;
            }
            set 
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        public string Password 
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }        
        }

        public string URL
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
                RaisePropertyChanged(() => URL);
            }
        }

        public SetupPageViewModel(INavigationService navigationService)
        {
            Title = "Setup";

            if (navigationService == null)
                throw new ArgumentNullException("navigationService");

            _navigationService = navigationService;

            SaveCommand = new RelayCommand(() => Save());
        }

        private void Save()
        {
            bool saveResult = DependencyService.Get<ISaveCredential>().Save(_url, _userName, _password);

            if (saveResult)
            {
                _navigationService.GoBack();
            }
            else
            {
                Messenger.Default.Send(new NotificationMessage("Could not save configuration!"));
            }
        }


    }
}
