using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using BTRS_Base.Models;
using BTRS_Server.Models.BTRSModel;
using System.Web.Http.Cors;

namespace BTRS_Base.Controllers.BTRSCtrl
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using BTRS_Server.Models.BTRSModel;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Transaction>("Transactions");
    builder.EntitySet<Fare>("Fares"); 
    builder.EntitySet<Payment>("Payments"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors("*", "*", "*")]
    public class TransactionsController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Transactions
        [EnableQuery]
        public IQueryable<Transaction> GetTransactions()
        {

            var xlist = db.Payments.Where(x => x.IsVerified == true);
           
            //Query item which payment isVarified == true
            var item = from firstitem in db.Transactions
                       join seconditem in xlist
                       on firstitem.PaymentId equals seconditem.Id
                       select firstitem;

            return item;
        }

        // GET: odata/Transactions(5)
        [EnableQuery]
        public SingleResult<Transaction> GetTransaction([FromODataUri] int key)
        {
            return SingleResult.Create(db.Transactions.Where(transaction => transaction.Id == key));
        }

        // PUT: odata/Transactions(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Transaction> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Transaction transaction = db.Transactions.Find(key);
            if (transaction == null)
            {
                return NotFound();
            }

            patch.Put(transaction);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(transaction);
        }

        // POST: odata/Transactions
        public IHttpActionResult Post(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticks = new DateTime(2016, 1, 1).Ticks;
            var ans = DateTime.Now.Ticks - ticks;
            var uniqueId = ans.ToString("x");


            transaction.BookingDate = DateTime.Now;
            transaction.TicketNo = uniqueId;

            var fare = db.Fares.FirstOrDefault(x => x.Id == transaction.FareId);

            var seat = transaction.ReservedSeat.Count();
            var seatTotal = seat / 2;
            transaction.AmountTaka = fare.TotalAmount * seatTotal;

            //var g = Guid.NewGuid();
            //var ticket = g.ToString();
            //string base64Guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            //t.BookingDate = DateTime.Now;
            //t.TicketNo = ticket;
            //t.AmountTaka = transaction.AmountTaka;
            //t.BusId = transaction.BusId;
            //t.DepartureLocationId = transaction.DepartureLocationId;
            //t.DestinationLocationId = transaction.DestinationLocationId;
            //t.Fare = transaction.Fare;
            //t.FareId = transaction.FareId;
            //t.Payment = transaction.Payment;
            //t.PaymentId = transaction.PaymentId;
            //t.ReservedSeat = transaction.ReservedSeat;
            //t.Time = transaction.Time;
            //t.TravelDate = transaction.TravelDate;
            //t.UserId = transaction.UserId;

            db.Transactions.Add(transaction);
            db.SaveChanges();

            return Created(transaction);
        }

        // PATCH: odata/Transactions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Transaction> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Transaction transaction = db.Transactions.Find(key);
            if (transaction == null)
            {
                return NotFound();
            }

            patch.Patch(transaction);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(transaction);
        }

        // DELETE: odata/Transactions(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Transaction transaction = db.Transactions.Find(key);
            if (transaction == null)
            {
                return NotFound();
            }

            db.Transactions.Remove(transaction);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Transactions(5)/Fare
        [EnableQuery]
        public SingleResult<Fare> GetFare([FromODataUri] int key)
        {
            return SingleResult.Create(db.Transactions.Where(m => m.Id == key).Select(m => m.Fare));
        }

        // GET: odata/Transactions(5)/Payment
        [EnableQuery]
        public SingleResult<Payment> GetPayment([FromODataUri] int key)
        {
            return SingleResult.Create(db.Transactions.Where(m => m.Id == key).Select(m => m.Payment));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransactionExists(int key)
        {
            return db.Transactions.Count(e => e.Id == key) > 0;
        }
    }
}
