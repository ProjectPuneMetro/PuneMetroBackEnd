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
    public class CancelledTicketsController : ApiController
    {
        public CancelledTicketsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        private PuneMetroV1Entities2 db = new PuneMetroV1Entities2();

        // GET: api/CancelledTickets
        public IQueryable<CancelledTicket> GetCancelledTickets()
        {
            return db.CancelledTickets;
        }

        // GET: api/CancelledTickets/5
        [ResponseType(typeof(CancelledTicket))]
        public async Task<IHttpActionResult> GetCancelledTicket(int id)
        {
            CancelledTicket cancelledTicket = await db.CancelledTickets.FindAsync(id);
            if (cancelledTicket == null)
            {
                return NotFound();
            }

            return Ok(cancelledTicket);
        }

        // PUT: api/CancelledTickets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCancelledTicket(int id, CancelledTicket cancelledTicket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cancelledTicket.cancellation_id)
            {
                return BadRequest();
            }

            db.Entry(cancelledTicket).State = System.Data.Entity.EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CancelledTicketExists(id))
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

        // POST: api/CancelledTickets
        [ResponseType(typeof(CancelledTicket))]
        public async Task<IHttpActionResult> PostCancelledTicket(CancelledTicket cancelledTicket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CancelledTickets.Add(cancelledTicket);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CancelledTicketExists(cancelledTicket.cancellation_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cancelledTicket.cancellation_id }, cancelledTicket);
        }

        // DELETE: api/CancelledTickets/5
        [ResponseType(typeof(CancelledTicket))]
        public async Task<IHttpActionResult> DeleteCancelledTicket(int id)
        {
            CancelledTicket cancelledTicket = await db.CancelledTickets.FindAsync(id);
            if (cancelledTicket == null)
            {
                return NotFound();
            }

            db.CancelledTickets.Remove(cancelledTicket);
            await db.SaveChangesAsync();

            return Ok(cancelledTicket);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CancelledTicketExists(int id)
        {
            return db.CancelledTickets.Count(e => e.cancellation_id == id) > 0;
        }
    }
}