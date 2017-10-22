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
using BTRS_Base.Models.ContractModel;
using System.Web.Http.Cors;

namespace BTRS_Base.Controllers.ContractCtrl
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using BTRS_Base.Models.ContractModel;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Contract>("Contracts");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors("*", "*", "*")]
    public class ContractsController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Contracts
        [EnableQuery]
        public IQueryable<Contract> GetContracts()
        {
            return db.Contracts;
        }

        // GET: odata/Contracts(5)
        [EnableQuery]
        public SingleResult<Contract> GetContract([FromODataUri] int key)
        {
            return SingleResult.Create(db.Contracts.Where(contract => contract.Id == key));
        }

        // PUT: odata/Contracts(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Contract> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Contract contract = db.Contracts.Find(key);
            if (contract == null)
            {
                return NotFound();
            }

            patch.Put(contract);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(contract);
        }

        // POST: odata/Contracts
        public IHttpActionResult Post(Contract contract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contracts.Add(contract);
            db.SaveChanges();

            return Created(contract);
        }

        // PATCH: odata/Contracts(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Contract> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Contract contract = db.Contracts.Find(key);
            if (contract == null)
            {
                return NotFound();
            }

            patch.Patch(contract);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(contract);
        }

        // DELETE: odata/Contracts(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Contract contract = db.Contracts.Find(key);
            if (contract == null)
            {
                return NotFound();
            }

            db.Contracts.Remove(contract);
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

        private bool ContractExists(int key)
        {
            return db.Contracts.Count(e => e.Id == key) > 0;
        }
    }
}
