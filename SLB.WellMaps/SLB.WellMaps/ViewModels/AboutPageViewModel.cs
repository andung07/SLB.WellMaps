using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLB.WellMaps.ViewModels
{
    public class AboutPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public string Title { get; set; }

        public AboutPageViewModel(INavigationService navigationService)
        {
            Title = "About";

            if (navigationService == null)
                throw new ArgumentNullException("navigationService");

            _navigationService = navigationService;
        }
    }
}
