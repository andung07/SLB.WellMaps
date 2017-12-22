using SLB.WellMaps.Services;

namespace SLB.WellMaps.Models
{
    public class TicketPageParam
    {
        private WellMapsAPI _api;
        public WellMapsAPI Api
        {
            get { return _api; }
        }

        private TicketItem _ticket;
        public TicketItem Ticket
        {
            get { return _ticket; }
        }

        public TicketPageParam(WellMapsAPI api , TicketItem ticket)
        {
            _api = api;
            _ticket = ticket;
        }
    }
}
