using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTRS_Server.Models.BTRSModel
{
    public class Fare
    {
        public int Id { get; set; }
        public int BusId { get; set; }
        public int DepartureLocationId { get; set; }
        public int DestinationLocationId { get; set; }
        public decimal Amount { get; set; }
        public Nullable<decimal> AdditionalAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}