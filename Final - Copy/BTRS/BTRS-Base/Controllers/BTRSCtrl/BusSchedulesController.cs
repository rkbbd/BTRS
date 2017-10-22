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
    builder.EntitySet<BusSchedule>("BusSchedules");
    builder.EntitySet<Bus>("Buses"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors("*", "*", "*")]
    public class BusSchedulesController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/BusSchedules
        [EnableQuery]
        public IQueryable<BusSchedule> GetBusSchedules()
        {
            return db.BusSchedules;
        }

        // GET: odata/BusSchedules(5)
        [EnableQuery]
        public SingleResult<BusSchedule> GetBusSchedule([FromODataUri] int key)
        {
            return SingleResult.Create(db.BusSchedules.Where(busSchedule => busSchedule.Id == key));
        }

        // PUT: odata/BusSchedules(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<BusSchedule> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BusSchedule busSchedule = db.BusSchedules.Find(key);
            if (busSchedule == null)
            {
                return NotFound();
            }

            patch.Put(busSchedule);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusScheduleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(busSchedule);
        }

        // POST: odata/BusSchedules
        public IHttpActionResult Post(BusSchedule busSchedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BusSchedules.Add(busSchedule);
            db.SaveChanges();

            return Created(busSchedule);
        }

        // PATCH: odata/BusSchedules(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<BusSchedule> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BusSchedule busSchedule = db.BusSchedules.Find(key);
            if (busSchedule == null)
            {
                return NotFound();
            }

            patch.Patch(busSchedule);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusScheduleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(busSchedule);
        }

        // DELETE: odata/BusSchedules(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            BusSchedule busSchedule = db.BusSchedules.Find(key);
            if (busSchedule == null)
            {
                return NotFound();
            }

            db.BusSchedules.Remove(busSchedule);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/BusSchedules(5)/Buses
        [EnableQuery]
        public IQueryable<Bus> GetBuses([FromODataUri] int key)
        {
            return db.BusSchedules.Where(m => m.Id == key).SelectMany(m => m.Buses);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BusScheduleExists(int key)
        {
            return db.BusSchedules.Count(e => e.Id == key) > 0;
        }
    }
}
