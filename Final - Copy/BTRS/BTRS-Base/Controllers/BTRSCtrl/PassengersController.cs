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
    builder.EntitySet<Passenger>("Passengers");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors("*", "*", "*")]
    public class PassengersController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Passengers
        [EnableQuery]
        public IQueryable<Passenger> GetPassengers()
        {
            return db.Passengers;
        }

        // GET: odata/Passengers(5)
        [EnableQuery]
        public SingleResult<Passenger> GetPassenger([FromODataUri] int key)
        {
            return SingleResult.Create(db.Passengers.Where(passenger => passenger.Id == key));
        }

        // PUT: odata/Passengers(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Passenger> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Passenger passenger = db.Passengers.Find(key);
            if (passenger == null)
            {
                return NotFound();
            }

            patch.Put(passenger);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PassengerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(passenger);
        }

        // POST: odata/Passengers
        public IHttpActionResult Post(Passenger passenger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Passengers.Add(passenger);
            db.SaveChanges();

            return Created(passenger);
        }

        // PATCH: odata/Passengers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Passenger> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Passenger passenger = db.Passengers.Find(key);
            if (passenger == null)
            {
                return NotFound();
            }

            patch.Patch(passenger);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PassengerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(passenger);
        }

        // DELETE: odata/Passengers(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Passenger passenger = db.Passengers.Find(key);
            if (passenger == null)
            {
                return NotFound();
            }

            db.Passengers.Remove(passenger);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PassengerExists(int key)
        {
            return db.Passengers.Count(e => e.Id == key) > 0;
        }
    }
}
