﻿using System;
using System.Collections.Generic;

namespace SLB.WellMaps.Models
{
    #region WellItemModel

    public class ResultWell
    {
        public string GRID_NAME { get; set; }
        public string Area { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime UnitDepart { get; set; }
        public DateTime UnitArrive { get; set; }
    }

    public class DWell
    {
        public List<ResultWell> results { get; set; }
    }

    public class WellResponseJSON
    {
        public DWell d { get; set; }
    }

    #endregion

    #region TicketModel

    public class PeopleType
    {
        public string Name { get; set; }
        public string WorkEmail { get; set; }
        public object MobilePhone { get; set; }
        public string Department { get; set; }
        public string Title { get; set; }
    }

    public class CrewInCharge
    {
        public List<PeopleType> results { get; set; }
    }

    public class ResultTicket
    {
        public string Issue_ID { get; set; }
        public string JobNameValue { get; set; }
        public string Location { get; set; }
        public PeopleType SpvInCharge { get; set; }
        public CrewInCharge CrewInCharge { get; set; }
        public DateTime? IssueDate { get; set; }
        public string StatusValue { get; set; }
        public string Remarks { get; set; }
        public DateTime? UnitDepart { get; set; }
        public DateTime? UnitArrive { get; set; }
    }

    public class DTicket
    {
        public List<ResultTicket> results { get; set; }
    }

    public class TicketResponseJSON
    {
        public DTicket d { get; set; }
    }

    #endregion
}

