using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Command;
using SLB.WellMaps.Models;
using SLB.WellMaps.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms.Maps;
using Plugin.ExternalMaps;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace SLB.WellMaps.ViewModels
{
    public class TicketDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private string _title;
        public string Title
        {
            get { return _title; }
            private set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

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

        private string _ticketID;
        public string TicketID
        {
            get { return _ticketID; }
            private set
            {
                _ticketID = value;
                RaisePropertyChanged(() => TicketID);
            }
        }

        private string _ticketLocation;
        public string TicketLocation
        {
            get { return _ticketLocation; }
            set
            {
                _ticketLocation = value;
                RaisePropertyChanged(() => TicketLocation);
            }
        }

        private string _ticketJobName;
        public string TicketJobName
        {
            get { return _ticketJobName; }
            set
            {
                _ticketJobName = value;
                RaisePropertyChanged(() => TicketJobName);
            }
        }

        private string _ticketSpvInCharge;
        public string TicketSpvInCharge
        {
            get { return _ticketSpvInCharge; }
            set
            {
                _ticketSpvInCharge = value;
                RaisePropertyChanged(() => TicketSpvInCharge);
            }
        }

        private string _ticketIssueDate;
        public string TicketIssueDate
        {
            get { return _ticketIssueDate; }
            set
            {
                _ticketIssueDate = value;
                RaisePropertyChanged(() => TicketIssueDate);
            }
        }

        private string _ticketRemarkHistory;
        public string TicketRemarkHistory
        {
            get { return _ticketRemarkHistory; }
            set
            {
                _ticketRemarkHistory = value;
                RaisePropertyChanged(() => TicketRemarkHistory);
            }
        }

        private List<PeopleType> _ticketCrewInCharge;
        public List<PeopleType> TicketCrewInCharge
        {
            get { return _ticketCrewInCharge; }
            set
            {
                _ticketCrewInCharge = value;
                RaisePropertyChanged(() => TicketCrewInCharge);
            }
        }

        private DateTime _closeDate;
        public DateTime CloseDate
        {
            get { return _closeDate; }
            set
            {
                _closeDate = value;
                RaisePropertyChanged(() => CloseDate);
            }
        }

        private TimeSpan _closeTime;
        public TimeSpan CloseTime
        {
            get { return _closeTime; }
            set
            {
                _closeTime = value;
                RaisePropertyChanged(() => CloseTime);
            }
        }

        public string NewRemark { get; set; }

        private TicketPageParam _param;
        public TicketPageParam Param
        {
            set
            {
                _param = value;

                Title = "Ticket ID: " + _param.Ticket.ID;
                TicketID = _param.Ticket.ID;
                TicketLocation = _param.Ticket.Location;
                TicketJobName = _param.Ticket.JobName;

                if (_param.Ticket.SpvInCharge != null)
                    TicketSpvInCharge = _param.Ticket.SpvInCharge.Name;
                else
                    TicketSpvInCharge = null;

                if (_param.Ticket.IssueDate != null)
                    TicketIssueDate = _param.Ticket.IssueDate.ToString();
                else
                    TicketIssueDate = null;

                if (_param.Ticket.Remarks != null)
                    TicketRemarkHistory = _param.Ticket.Remarks;
                else
                    TicketRemarkHistory = null;

                TicketCrewInCharge = _param.Ticket.CrewInCharge;

            }
        }

        private WellItem _well;
        public ICommand NavigateCommand { get; set; }
        public ICommand SaveAndCloseCommand { get; set; }

        public TicketDetailPageViewModel(INavigationService navigationService)
        {
            if (navigationService == null)
                throw new ArgumentNullException("navigationService");

            _navigationService = navigationService;

            CloseDate = DateTime.Now.Date;
            CloseTime = DateTime.Now.TimeOfDay;

            NavigateCommand = new RelayCommand(() => 
            {
                CrossExternalMaps.Current.NavigateTo(_well.WellName, _well.Latitude, _well.Longitude);
            });

            SaveAndCloseCommand = new RelayCommand(async () =>
            {
                JObject body = new JObject(
                    new JProperty("StatusValue", "Resolved"),
                    new JProperty("CloseDate", (_closeDate + _closeTime).ToString("s")),
                    new JProperty("Remarks", NewRemark));

                IsBusy = true;
                await _param.Api.UpdateTicket(_ticketID, body);
                IsBusy = false;
                _navigationService.NavigateTo("HomePage");

            });
        }

        public async void GetWellDetail()
        {
            IsBusy = true;
            _well = await _param.Api.GetWellByName(_param.Ticket.Location);
            IsBusy = false;

            var wellPosition = new Position(_well.Latitude, _well.Longitude);
            
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = wellPosition,
                Label = _well.WellName ?? string.Empty,
                Address = _well.Description
            };

            Messenger.Default.Send(pin);
        }

    }
}
