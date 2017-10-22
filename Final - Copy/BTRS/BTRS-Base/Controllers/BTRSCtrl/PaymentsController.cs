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
    builder.EntitySet<Payment>("Payments");
    builder.EntitySet<Transaction>("Transactions"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors("*", "*", "*")]
    public class PaymentsController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Payments
        [EnableQuery]
        public IQueryable<Payment> GetPayments()
        {
            return db.Payments;
        }

        // GET: odata/Payments(5)
        [EnableQuery]
        public SingleResult<Payment> GetPayment([FromODataUri] int key)
        {
            return SingleResult.Create(db.Payments.Where(payment => payment.Id == key));
        }

        // PUT: odata/Payments(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Payment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Payment payment = db.Payments.Find(key);
            if (payment == null)
            {
                return NotFound();
            }

            patch.Put(payment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(payment);
        }

        // POST: odata/Payments
        public IHttpActionResult Post(Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            payment.PaymentDate = DateTime.Now;
            payment.IsVerified = false;                 //Always insert false admin update (true) after check payment

            db.Payments.Add(payment);
            db.SaveChanges();

            return Created(payment);
        }

        // PATCH: odata/Payments(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Payment> patch)   //authorize
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Payment payment = db.Payments.Find(key);
            if (payment == null)
            {
                return NotFound();
            }

            patch.Patch(payment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(payment);
        }

        // DELETE: odata/Payments(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Payment payment = db.Payments.Find(key);
            if (payment == null)
            {
                return NotFound();
            }

            db.Payments.Remove(payment);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Payments(5)/Transactions
        [EnableQuery]
        public IQueryable<Transaction> GetTransactions([FromODataUri] int key)
        {
            return db.Payments.Where(m => m.Id == key).SelectMany(m => m.Transactions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaymentExists(int key)
        {
            return db.Payments.Count(e => e.Id == key) > 0;
        }
    }
}
