using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTRS_Server.Models.BTRSModel
{
    public class Transaction
    {
        public int Id { get; set; }
        public string TicketNo { get; set; }
        public int BusId { get; set; }
        public int DepartureLocationId { get; set; }
        public int DestinationLocationId { get; set; }
        public System.DateTime TravelDate { get; set; }
        public System.DateTime BookingDate { get; set; }
        public string Time { get; set; }
        public string ReservedSeat { get; set; }
        public decimal AmountTaka { get; set; }
        public int FareId { get; set; }
        public Nullable<int> PaymentId { get; set; }
        public string UserName { get; set; }

        public virtual Fare Fare { get; set; }
        public virtual Payment Payment { get; set; }
    }
}