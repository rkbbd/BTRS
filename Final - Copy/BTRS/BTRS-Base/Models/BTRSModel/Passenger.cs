using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTRS_Server.Models.BTRSModel
{
    public class Passenger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string ContractNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

    }
}