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
    builder.EntitySet<Location>("Locations");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors("*", "*", "*")]
    public class LocationsController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Locations
        [EnableQuery]
        public IQueryable<Location> GetLocations()
        {
            return db.Locations;
        }

        // GET: odata/Locations(5)
        [EnableQuery]
        public SingleResult<Location> GetLocation([FromODataUri] int key)
        {
            return SingleResult.Create(db.Locations.Where(location => location.Id == key));
        }

        // PUT: odata/Locations(5)
        
        public IHttpActionResult Put([FromODataUri] int key, Delta<Location> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Location location = db.Locations.Find(key);
            if (location == null)
            {
                return NotFound();
            }

            patch.Put(location);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(location);
        }

        // POST: odata/Locations
       
        public IHttpActionResult Post(Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Locations.Add(location);
            db.SaveChanges();

            return Created(location);
        }

        // PATCH: odata/Locations(5)
       
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Location> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Location location = db.Locations.Find(key);
            if (location == null)
            {
                return NotFound();
            }

            patch.Patch(location);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(location);
        }

        // DELETE: odata/Locations(5)
       
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Location location = db.Locations.Find(key);
            if (location == null)
            {
                return NotFound();
            }

            db.Locations.Remove(location);
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

        private bool LocationExists(int key)
        {
            return db.Locations.Count(e => e.Id == key) > 0;
        }
    }
}
