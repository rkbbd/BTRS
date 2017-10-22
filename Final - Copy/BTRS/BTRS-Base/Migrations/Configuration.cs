namespace BTRS_Base.Migrations
{
    using BTRS_Server.Models.BTRSModel;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Models.ContractModel;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BTRS_Base.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BTRS_Base.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            //============= Super Admin============
            var userStore = new UserStore<ApplicationUser>(context);

            if (!(context.Users.Any(u => u.UserName == "rakib@admin.com")))
            {
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser { UserName = "rakib@admin.com", PhoneNumber = "01684859599" };
                userManager.Create(userToInsert, "01684859599");
            }

            //==================Initial Data =================
            List<Location> LocationList = new List<Location>() {
                  new Location(){ LocationName = "Dhaka" },
                  new Location(){ LocationName = "Chittagong" },
                  new Location(){ LocationName = "Cox's Bazar" },
                  new Location(){ LocationName = "Sylhet" },
                  new Location(){ LocationName = "Khulna" },
                  new Location(){ LocationName = "Rajshahi" },
                  new Location(){ LocationName = "Rangpur" },
             };
            foreach (Location l in LocationList)
            {
                context.Locations.Add(l);

            }

            List<BusSchedule> BusScheduleList = new List<BusSchedule>() {
                 new BusSchedule(){  BusName = "7City", DepartureTime = "9.00 AM", ArrivalTime = "2.00 PM"},
                 new BusSchedule(){  BusName = "7City", DepartureTime = "2.00 PM", ArrivalTime = "7.00 PM"},
                 new BusSchedule(){  BusName = "7City", DepartureTime = "7.00 PM", ArrivalTime = "12.00 AM"},
                 new BusSchedule(){  BusName = "7City", DepartureTime = "12.00 AM", ArrivalTime = "5.00 AM"},
             };
            foreach (BusSchedule BS in BusScheduleList)
            {
                context.BusSchedules.Add(BS);

            }

            context.SaveChanges();
            base.Seed(context);

            List<Bus> BusList = new List<Bus>() {
                 new Bus(){ BusType = "General", NoOfSeat= 32, BusScheduleId = 1 },
                 new Bus(){ BusType = "General", NoOfSeat= 32, BusScheduleId = 2 },
                 new Bus(){ BusType = "General", NoOfSeat= 32, BusScheduleId = 3 },
                 new Bus(){ BusType = "AC", NoOfSeat= 32, BusScheduleId = 2},
                 new Bus(){ BusType = "AC", NoOfSeat= 32, BusScheduleId = 3 },
                 new Bus(){ BusType = "Business Class", NoOfSeat= 32, BusScheduleId = 4 },
             };
            foreach (Bus b in BusList)
            {
                context.Buses.Add(b);

            }

            List<Fare> FareList = new List<Fare>() {
                //===Dhaka - Chittagong===
                new Fare() { BusId = 1, DepartureLocationId = 1, DestinationLocationId = 2, Amount = 0, AdditionalAmount = 0, TotalAmount = 1400 },
                new Fare() { BusId = 2, DepartureLocationId = 1, DestinationLocationId = 2, Amount = 0, AdditionalAmount = 0, TotalAmount = 1400 },
                new Fare() { BusId = 3, DepartureLocationId = 1, DestinationLocationId = 2, Amount = 0, AdditionalAmount = 0, TotalAmount = 1400 },
                new Fare() { BusId = 4, DepartureLocationId = 1, DestinationLocationId = 2, Amount = 0, AdditionalAmount = 0, TotalAmount = 1800 },
                new Fare() { BusId = 5, DepartureLocationId = 1, DestinationLocationId = 2, Amount = 0, AdditionalAmount = 0, TotalAmount = 1800 },
                new Fare() { BusId = 6, DepartureLocationId = 1, DestinationLocationId = 2, Amount = 0, AdditionalAmount = 0, TotalAmount = 2000 },
                //===Chittagong - Dhaka===
                new Fare() { BusId = 1, DepartureLocationId = 2, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 1400 },
                new Fare() { BusId = 2, DepartureLocationId = 2, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 1400 },
                new Fare() { BusId = 3, DepartureLocationId = 2, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 1400 },
                new Fare() { BusId = 4, DepartureLocationId = 2, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 1800 },
                new Fare() { BusId = 5, DepartureLocationId = 2, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 1800 },
                new Fare() { BusId = 6, DepartureLocationId = 2, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 2000 },
                
                //===Dhaka - Cox's Bazar===
                new Fare() { BusId = 1, DepartureLocationId = 1, DestinationLocationId = 3, Amount = 0, AdditionalAmount = 0, TotalAmount = 2100 },
                new Fare() { BusId = 2, DepartureLocationId = 1, DestinationLocationId = 3, Amount = 0, AdditionalAmount = 0, TotalAmount = 2100 },
                new Fare() { BusId = 3, DepartureLocationId = 1, DestinationLocationId = 3, Amount = 0, AdditionalAmount = 0, TotalAmount = 2100 },
                new Fare() { BusId = 4, DepartureLocationId = 1, DestinationLocationId = 3, Amount = 0, AdditionalAmount = 0, TotalAmount = 2700 },
                new Fare() { BusId = 5, DepartureLocationId = 1, DestinationLocationId = 3, Amount = 0, AdditionalAmount = 0, TotalAmount = 2700 },
                new Fare() { BusId = 6, DepartureLocationId = 1, DestinationLocationId = 3, Amount = 0, AdditionalAmount = 0, TotalAmount = 3000 },
                //===Cox's Bazar - Dhaka===
                new Fare() { BusId = 1, DepartureLocationId = 3, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 2100 },
                new Fare() { BusId = 2, DepartureLocationId = 3, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 2100 },
                new Fare() { BusId = 3, DepartureLocationId = 3, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 2100 },
                new Fare() { BusId = 4, DepartureLocationId = 3, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 2700 },
                new Fare() { BusId = 5, DepartureLocationId = 3, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 2700 },
                new Fare() { BusId = 6, DepartureLocationId = 3, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 3000 },

                 //===Chittagong - Cox's Bazar===
                new Fare() { BusId = 1, DepartureLocationId = 2, DestinationLocationId = 3, Amount = 0, AdditionalAmount = 0, TotalAmount = 1400 },
                new Fare() { BusId = 2, DepartureLocationId = 2, DestinationLocationId = 3, Amount = 0, AdditionalAmount = 0, TotalAmount = 1400 },
                new Fare() { BusId = 3, DepartureLocationId = 2, DestinationLocationId = 3, Amount = 0, AdditionalAmount = 0, TotalAmount = 1400 },
                new Fare() { BusId = 4, DepartureLocationId = 2, DestinationLocationId = 3, Amount = 0, AdditionalAmount = 0, TotalAmount = 1800 },
                new Fare() { BusId = 5, DepartureLocationId = 2, DestinationLocationId = 3, Amount = 0, AdditionalAmount = 0, TotalAmount = 1800 },
                new Fare() { BusId = 6, DepartureLocationId = 2, DestinationLocationId = 3, Amount = 0, AdditionalAmount = 0, TotalAmount = 2000 },
                //===Cox's Bazar - Chittagong===
                new Fare() { BusId = 1, DepartureLocationId = 3, DestinationLocationId = 2, Amount = 0, AdditionalAmount = 0, TotalAmount = 1400 },
                new Fare() { BusId = 2, DepartureLocationId = 3, DestinationLocationId = 2, Amount = 0, AdditionalAmount = 0, TotalAmount = 1400 },
                new Fare() { BusId = 3, DepartureLocationId = 3, DestinationLocationId = 2, Amount = 0, AdditionalAmount = 0, TotalAmount = 1400 },
                new Fare() { BusId = 4, DepartureLocationId = 3, DestinationLocationId = 2, Amount = 0, AdditionalAmount = 0, TotalAmount = 1800 },
                new Fare() { BusId = 5, DepartureLocationId = 3, DestinationLocationId = 2, Amount = 0, AdditionalAmount = 0, TotalAmount = 1800 },
                new Fare() { BusId = 6, DepartureLocationId = 3, DestinationLocationId = 2, Amount = 0, AdditionalAmount = 0, TotalAmount = 2000 },


                new Fare() { BusId = 1, DepartureLocationId = 1, DestinationLocationId = 4, Amount = 0, AdditionalAmount = 0, TotalAmount = 2150 },
                new Fare() { BusId = 2, DepartureLocationId = 1, DestinationLocationId = 4, Amount = 0, AdditionalAmount = 0, TotalAmount = 2150 },
                new Fare() { BusId = 3, DepartureLocationId = 1, DestinationLocationId = 4, Amount = 0, AdditionalAmount = 0, TotalAmount = 2150 },
                new Fare() { BusId = 4, DepartureLocationId = 1, DestinationLocationId = 4, Amount = 0, AdditionalAmount = 0, TotalAmount = 2750 },
                new Fare() { BusId = 5, DepartureLocationId = 1, DestinationLocationId = 4, Amount = 0, AdditionalAmount = 0, TotalAmount = 2750 },
                new Fare() { BusId = 6, DepartureLocationId = 1, DestinationLocationId = 4, Amount = 0, AdditionalAmount = 0, TotalAmount = 3000 },

                new Fare() { BusId = 1, DepartureLocationId = 4, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 2150 },
                new Fare() { BusId = 2, DepartureLocationId = 4, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 2150 },
                new Fare() { BusId = 3, DepartureLocationId = 4, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 2150 },
                new Fare() { BusId = 4, DepartureLocationId = 4, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 2750 },
                new Fare() { BusId = 5, DepartureLocationId = 4, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 2750 },
                new Fare() { BusId = 6, DepartureLocationId = 4, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 3000 },

                new Fare() { BusId = 1, DepartureLocationId = 1, DestinationLocationId = 5, Amount = 0, AdditionalAmount = 0, TotalAmount = 1750 },
                new Fare() { BusId = 2, DepartureLocationId = 1, DestinationLocationId = 5, Amount = 0, AdditionalAmount = 0, TotalAmount = 1750 },
                new Fare() { BusId = 3, DepartureLocationId = 1, DestinationLocationId = 5, Amount = 0, AdditionalAmount = 0, TotalAmount = 1750 },
                new Fare() { BusId = 4, DepartureLocationId = 1, DestinationLocationId = 5, Amount = 0, AdditionalAmount = 0, TotalAmount = 2250 },
                new Fare() { BusId = 5, DepartureLocationId = 1, DestinationLocationId = 5, Amount = 0, AdditionalAmount = 0, TotalAmount = 2250 },
                new Fare() { BusId = 6, DepartureLocationId = 1, DestinationLocationId = 5, Amount = 0, AdditionalAmount = 0, TotalAmount = 2500 },

                new Fare() { BusId = 1, DepartureLocationId = 5, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 1750 },
                new Fare() { BusId = 2, DepartureLocationId = 5, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 1750 },
                new Fare() { BusId = 3, DepartureLocationId = 5, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 1750 },
                new Fare() { BusId = 4, DepartureLocationId = 5, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 2250 },
                new Fare() { BusId = 5, DepartureLocationId = 5, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 2250 },
                new Fare() { BusId = 6, DepartureLocationId = 5, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 2500 },

                new Fare() { BusId = 1, DepartureLocationId = 1, DestinationLocationId = 6, Amount = 0, AdditionalAmount = 0, TotalAmount = 1400 },
                new Fare() { BusId = 2, DepartureLocationId = 1, DestinationLocationId = 6, Amount = 0, AdditionalAmount = 0, TotalAmount = 1400 },
                new Fare() { BusId = 3, DepartureLocationId = 1, DestinationLocationId = 6, Amount = 0, AdditionalAmount = 0, TotalAmount = 1400 },
                new Fare() { BusId = 4, DepartureLocationId = 1, DestinationLocationId = 6, Amount = 0, AdditionalAmount = 0, TotalAmount = 1800 },
                new Fare() { BusId = 5, DepartureLocationId = 1, DestinationLocationId = 6, Amount = 0, AdditionalAmount = 0, TotalAmount = 1800 },
                new Fare() { BusId = 6, DepartureLocationId = 1, DestinationLocationId = 6, Amount = 0, AdditionalAmount = 0, TotalAmount = 2000 },

                new Fare() { BusId = 1, DepartureLocationId = 6, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 1400 },
                new Fare() { BusId = 2, DepartureLocationId = 6, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 1400 },
                new Fare() { BusId = 3, DepartureLocationId = 6, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 1400 },
                new Fare() { BusId = 4, DepartureLocationId = 6, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 1800 },
                new Fare() { BusId = 5, DepartureLocationId = 6, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 1800 },
                new Fare() { BusId = 6, DepartureLocationId = 6, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 2000 },

                new Fare() { BusId = 1, DepartureLocationId = 1, DestinationLocationId = 7, Amount = 0, AdditionalAmount = 0, TotalAmount = 1050 },
                new Fare() { BusId = 2, DepartureLocationId = 1, DestinationLocationId = 7, Amount = 0, AdditionalAmount = 0, TotalAmount = 1050 },
                new Fare() { BusId = 3, DepartureLocationId = 1, DestinationLocationId = 7, Amount = 0, AdditionalAmount = 0, TotalAmount = 1050 },
                new Fare() { BusId = 4, DepartureLocationId = 1, DestinationLocationId = 7, Amount = 0, AdditionalAmount = 0, TotalAmount = 1350 },
                new Fare() { BusId = 5, DepartureLocationId = 1, DestinationLocationId = 7, Amount = 0, AdditionalAmount = 0, TotalAmount = 1350 },
                new Fare() { BusId = 6, DepartureLocationId = 1, DestinationLocationId = 7, Amount = 0, AdditionalAmount = 0, TotalAmount = 1500 },

                new Fare() { BusId = 1, DepartureLocationId = 7, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 1050 },
                new Fare() { BusId = 2, DepartureLocationId = 7, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 1050 },
                new Fare() { BusId = 3, DepartureLocationId = 7, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 1050 },
                new Fare() { BusId = 4, DepartureLocationId = 7, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 1350 },
                new Fare() { BusId = 5, DepartureLocationId = 7, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 1350 },
                new Fare() { BusId = 6, DepartureLocationId = 7, DestinationLocationId = 1, Amount = 0, AdditionalAmount = 0, TotalAmount = 1500 },
             };
            foreach (Fare f in FareList)
            {
                context.Fares.Add(f);

            }

            List<Payment> PaymentList = new List<Payment>()
            {
                new Payment() { Email ="rakib434@gmail.com", Code = "rakib-01684859599", PaymentDate = DateTime.Now, IsVerified = true },
            };
            foreach (Payment p in PaymentList)
            {
                context.Payments.Add(p);

            }

            context.SaveChanges();
            base.Seed(context);

            List<Transaction> TransList = new List<Transaction>() {
                 new Transaction(){ TicketNo = "01684859599", BusId= 6, DepartureLocationId = 1, DestinationLocationId = 3, BookingDate= DateTime.Now, TravelDate =  DateTime.Now.Date.AddDays(1).AddMinutes(1400), Time = "12.00 AM", ReservedSeat = "D1D2", AmountTaka= 2000, FareId = 18, PaymentId= 1, UserName= "Rakib" },

             };
            foreach (Transaction t in TransList)
            {
                context.Transactions.Add(t);

            }

            List<Contract> ContractList = new List<Contract>() {
                 new Contract(){ Name = "Rakib", Email = "Rakib434@gmail.com", Message = "This App is created by me" },

             };
            foreach (Contract c in ContractList)
            {
                context.Contracts.Add(c);

            }


            context.SaveChanges();
            base.Seed(context);

        }
    }
}
