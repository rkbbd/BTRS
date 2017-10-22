using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTRS_Server.Models.BTRSModel
{
    public class BusSchedule
    {
        public int Id { get; set; }
        public string BusName { get; set; }
        //public int DepartureLocationId { get; set; }
        //public int DestinationLocationId { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public virtual ICollection<Bus> Buses { get; set; }
    }
}