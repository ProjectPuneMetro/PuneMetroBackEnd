using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using V1.Models;

namespace V1.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CancellationReqsController : ApiController
    {
        public CancellationReqsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        private PuneMetroV1Entities2 db = new PuneMetroV1Entities2();

        // GET: api/CancellationReqs
        public IQueryable<CancellationReq> GetCancellationReqs()
        {
            return db.CancellationReqs;
        }

        // GET: api/CancellationReqs/5
        [ResponseType(typeof(CancellationReq))]
        public async Task<IHttpActionResult> GetCancellationReq(int id)
        {
            CancellationReq cancellationReq = await db.CancellationReqs.FindAsync(id);
            if (cancellationReq == null)
            {
                return NotFound();
            }

            return Ok(cancellationReq);
        }

        // PUT: api/CancellationReqs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCancellationReq(int id, CancellationReq cancellationReq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cancellationReq.cancellationReq_id)
            {
                return BadRequest();
            }

            db.Entry(cancellationReq).State = System.Data.Entity.EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CancellationReqExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CancellationReqs
        [ResponseType(typeof(CancellationReq))]
        public async Task<IHttpActionResult> PostCancellationReq(CancellationReq cancellationReq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CancellationReqs.Add(cancellationReq);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CancellationReqExists(cancellationReq.cancellationReq_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cancellationReq.cancellationReq_id }, cancellationReq);
        }

        // DELETE: api/CancellationReqs/5
        [ResponseType(typeof(CancellationReq))]
        public async Task<IHttpActionResult> DeleteCancellationReq(int id)
        {
            CancellationReq cancellationReq = await db.CancellationReqs.FindAsync(id);
            if (cancellationReq == null)
            {
                return NotFound();
            }

            db.CancellationReqs.Remove(cancellationReq);
            await db.SaveChangesAsync();

            return Ok(cancellationReq);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CancellationReqExists(int id)
        {
            return db.CancellationReqs.Count(e => e.cancellationReq_id == id) > 0;
        }
    }
}