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
    builder.EntitySet<Fare>("Fares");
    builder.EntitySet<Transaction>("Transactions"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors("*", "*", "*")]
    public class FaresController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Fares
        [EnableQuery]
        public IQueryable<Fare> GetFares()
        {
            return db.Fares;
        }

        // GET: odata/Fares(5)
        [EnableQuery]
        public SingleResult<Fare> GetFare([FromODataUri] int key)
        {
            return SingleResult.Create(db.Fares.Where(fare => fare.Id == key));
        }

        // PUT: odata/Fares(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Fare> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Fare fare = db.Fares.Find(key);
            if (fare == null)
            {
                return NotFound();
            }

            patch.Put(fare);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FareExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(fare);
        }

        // POST: odata/Fares
        public IHttpActionResult Post(Fare fare)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Fares.Add(fare);
            db.SaveChanges();

            return Created(fare);
        }

        // PATCH: odata/Fares(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Fare> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Fare fare = db.Fares.Find(key);
            if (fare == null)
            {
                return NotFound();
            }

            patch.Patch(fare);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FareExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(fare);
        }

        // DELETE: odata/Fares(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Fare fare = db.Fares.Find(key);
            if (fare == null)
            {
                return NotFound();
            }

            db.Fares.Remove(fare);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Fares(5)/Transactions
        [EnableQuery]
        public IQueryable<Transaction> GetTransactions([FromODataUri] int key)
        {
            return db.Fares.Where(m => m.Id == key).SelectMany(m => m.Transactions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FareExists(int key)
        {
            return db.Fares.Count(e => e.Id == key) > 0;
        }
    }
}
