using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using SLB.WellMaps.ViewModels;

namespace SLB.WellMaps
{
    public class Locator
    {
        static Locator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<AboutPageViewModel>();
            SimpleIoc.Default.Register<HomePageViewModel>();
            SimpleIoc.Default.Register<MapPageViewModel>();
            SimpleIoc.Default.Register<OpenTicketsPageViewModel>();
            SimpleIoc.Default.Register<SetupPageViewModel>();
            SimpleIoc.Default.Register<TicketDetailPageViewModel>();
        }

        public const string AboutPage           = "AboutPage";
        public const string HomePage            = "HomePage";
        public const string MapPage             = "MapPage";
        public const string OpenTicketsPage     = "OpenTicketsPage";
        public const string SetupPage           = "SetupPage";
        public const string FeedbackPage        = "FeedbackPage";
        public const string TicketDetailPage    = "TicketDetailPage";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
         Justification = "This non-static member is needed for data binding purposes.")]
        public AboutPageViewModel About
        {
            get { return ServiceLocator.Current.GetInstance<AboutPageViewModel>(); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
         Justification = "This non-static member is needed for data binding purposes.")]
        public HomePageViewModel Home
        {
            get { return ServiceLocator.Current.GetInstance<HomePageViewModel>(); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
         Justification = "This non-static member is needed for data binding purposes.")]
        public MapPageViewModel Map
        {
            get { return ServiceLocator.Current.GetInstance<MapPageViewModel>(); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
         Justification = "This non-static member is needed for data binding purposes.")]
        public OpenTicketsPageViewModel OpenTickets
        {
            get { return ServiceLocator.Current.GetInstance<OpenTicketsPageViewModel>(); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
         Justification = "This non-static member is needed for data binding purposes.")]
        public SetupPageViewModel Setup
        {
            get { return ServiceLocator.Current.GetInstance<SetupPageViewModel>(); }
        }        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
         Justification = "This non-static member is needed for data binding purposes.")]
        public TicketDetailPageViewModel TicketDetail
        {
            get { return ServiceLocator.Current.GetInstance<TicketDetailPageViewModel>(); }
        }
    }
}
