using System;
using System.Collections.Generic;

namespace SLB.WellMaps.Models
{
    public class TicketItem
    {
        public string ID { get; set; }
        public string JobName { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Detail { get; set; }
        public string Remarks { get; set; }
        public PeopleType SpvInCharge { get; set; }
        public List<PeopleType> CrewInCharge { get; set; }
        public DateTime? IssueDate { get; set; }
    }

}

