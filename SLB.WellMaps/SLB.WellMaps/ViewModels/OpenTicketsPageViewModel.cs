using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using SLB.WellMaps.Models;
using SLB.WellMaps.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLB.WellMaps.ViewModels
{
    public class OpenTicketsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public string Title { get; private set; }

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

        private List<TicketItem> _tickets;
        public List<TicketItem> Tickets
        {
            get { return _tickets; }
            private set
            {
                _tickets = value;
                RaisePropertyChanged(() => Tickets);
            }
        }

        private WellMapsAPI api;
        private Credential _credential;
        public Credential Credential 
        {
            get
            {
                return _credential;
            }
            set
            {
                _credential = value;
                api = new WellMapsAPI(value);
            }
        }

        public Action<TicketItem> ItemSelected { get; set; }

        private TicketItem _selectedTicket;
        public TicketItem SelectedTicket 
        { 
            get
            {
                return _selectedTicket;
            }
            set
            {
                _selectedTicket = value;
                RaisePropertyChanged(() => SelectedTicket);

                if (_selectedTicket == null)
                    return;

                if (ItemSelected == null)
                {
                    _navigationService.NavigateTo(Locator.TicketDetailPage, new TicketPageParam(api, _selectedTicket));
                    SelectedTicket = null;
                }

                else
                {
                    ItemSelected.Invoke(_selectedTicket);
                }
                    
            }
        }

        public OpenTicketsPageViewModel(INavigationService navigationService)
        {
            Title = "Open Ticket(s)";

            if (navigationService == null)
                throw new ArgumentNullException("navigationService");

            _navigationService = navigationService;
        }

        public async void LoadTickets()
        {

            IsBusy = true;
            Tickets = await api.GetOpenTicket();
            IsBusy = false;
        }
    }
}
