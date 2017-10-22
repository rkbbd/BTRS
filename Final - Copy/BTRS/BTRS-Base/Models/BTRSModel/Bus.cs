using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTRS_Server.Models.BTRSModel
{
    public class Bus
    {
        public int Id { get; set; }
        public string BusType { get; set; }
        public Nullable<int> BusScheduleId { get; set; }
        public int NoOfSeat { get; set; }

        public virtual BusSchedule BusSchedule { get; set; }
    }
}