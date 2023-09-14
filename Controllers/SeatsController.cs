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
    public class SeatsController : ApiController
    {
        public SeatsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        private PuneMetroV1Entities2 db = new PuneMetroV1Entities2();

        // GET: api/Seats
        public IQueryable<Seat> GetSeats()
        {
            return db.Seats;
        }

        // GET: api/Seats/5
        [ResponseType(typeof(Seat))]
        public async Task<IHttpActionResult> GetSeat(int id)
        {
            Seat seat = await db.Seats.FindAsync(id);
            if (seat == null)
            {
                return NotFound();
            }

            return Ok(seat);
        }

        // PUT: api/Seats/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSeat(int id, Seat seat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != seat.seat_id)
            {
                return BadRequest();
            }

            db.Entry(seat).State = System.Data.Entity.EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeatExists(id))
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

        // POST: api/Seats
        [ResponseType(typeof(Seat))]
        public async Task<IHttpActionResult> PostSeat(Seat seat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Seats.Add(seat);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = seat.seat_id }, seat);
        }

        // DELETE: api/Seats/5
        [ResponseType(typeof(Seat))]
        public async Task<IHttpActionResult> DeleteSeat(int id)
        {
            Seat seat = await db.Seats.FindAsync(id);
            if (seat == null)
            {
                return NotFound();
            }

            db.Seats.Remove(seat);
            await db.SaveChangesAsync();

            return Ok(seat);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SeatExists(int id)
        {
            return db.Seats.Count(e => e.seat_id == id) > 0;
        }
    }
}