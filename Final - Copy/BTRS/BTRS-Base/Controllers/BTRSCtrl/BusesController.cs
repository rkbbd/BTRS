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
    builder.EntitySet<Bus>("Buses");
    builder.EntitySet<BusSchedule>("BusSchedules"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors("*", "*", "*")]
    public class BusesController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: odata/Buses
        [EnableQuery]
        public IQueryable<Bus> GetBuses()
        {
            return db.Buses;
        }

        // GET: odata/Buses(5)
        [EnableQuery]
        public SingleResult<Bus> GetBus([FromODataUri] int key)
        {
            return SingleResult.Create(db.Buses.Where(bus => bus.Id == key));
        }

        // PUT: odata/Buses(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Bus> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Bus bus = db.Buses.Find(key);
            if (bus == null)
            {
                return NotFound();
            }

            patch.Put(bus);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(bus);
        }

        // POST: odata/Buses
        public IHttpActionResult Post(Bus bus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Buses.Add(bus);
            db.SaveChanges();

            return Created(bus);
        }

        // PATCH: odata/Buses(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Bus> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Bus bus = db.Buses.Find(key);
            if (bus == null)
            {
                return NotFound();
            }

            patch.Patch(bus);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(bus);
        }

        // DELETE: odata/Buses(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Bus bus = db.Buses.Find(key);
            if (bus == null)
            {
                return NotFound();
            }

            db.Buses.Remove(bus);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Buses(5)/BusSchedule
        [EnableQuery]
        public SingleResult<BusSchedule> GetBusSchedule([FromODataUri] int key)
        {
            return SingleResult.Create(db.Buses.Where(m => m.Id == key).Select(m => m.BusSchedule));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BusExists(int key)
        {
            return db.Buses.Count(e => e.Id == key) > 0;
        }
    }
}
