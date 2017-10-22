using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTRS_Server.Models.BTRSModel
{
    public class Payment
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        //public decimal Amount { get; set; }
        public System.DateTime PaymentDate { get; set; }
        public bool IsVerified { get; set; }

        //public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}