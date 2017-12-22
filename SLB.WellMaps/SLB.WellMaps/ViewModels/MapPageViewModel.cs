using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using SLB.WellMaps.Models;
using SLB.WellMaps.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms.Maps;

namespace SLB.WellMaps.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public string Title { get; private set; }
        public Credential _credential { get; set; }
        public string WellName { get; set; }

        public ICommand SearchCommand { get; set; }
        public ICommand StreetCommand { get; set; }
        public ICommand HybridCommand { get; set; }
        public ICommand SatelliteCommand { get; set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            private set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
                RaisePropertyChanged(() => IsNotBusy);
            }
        }

        public bool IsNotBusy
        {
            get { return !_isBusy; }
        }

        public MapPageViewModel(INavigationService navigationService)
        {
            Title = "Map";

            if (navigationService == null)
                throw new ArgumentNullException("navigationService");

            _navigationService = navigationService;

            SearchCommand = new RelayCommand(() =>
            {
                if(WellName != null)
                {
                    GetWellDetail(WellName);
                }
            });

            StreetCommand = new RelayCommand(() =>
            {
                Messenger.Default.Send(MapType.Street);
            });

            HybridCommand = new RelayCommand(() =>
            {
                Messenger.Default.Send(MapType.Hybrid);
            });

            SatelliteCommand = new RelayCommand(() =>
            {
                Messenger.Default.Send(MapType.Satellite);
            });
        }

        public async void GetWellDetail(string wellname)
        {
            WellMapsAPI api = new WellMapsAPI(_credential);

            IsBusy = true;
            WellItem well = await api.GetWellByName(wellname);
            IsBusy = false;

            if(well != null)
            {
                var wellPosition = new Position(well.Latitude, well.Longitude);

                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = wellPosition,
                    Label = well.WellName ?? string.Empty,
                    Address = well.Description
                };

                Messenger.Default.Send(pin);
            }
            
        }
    }
}
