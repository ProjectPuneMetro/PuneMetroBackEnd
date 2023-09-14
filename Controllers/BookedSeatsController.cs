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
    public class BookedSeatsController : ApiController
    {
        public BookedSeatsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        private PuneMetroV1Entities2 db = new PuneMetroV1Entities2();

        // GET: api/BookedSeats
        public IQueryable<BookedSeat> GetBookedSeats()
        {
            return db.BookedSeats;
        }

        // GET: api/BookedSeats/5
        [ResponseType(typeof(BookedSeat))]
        public async Task<IHttpActionResult> GetBookedSeat(int id)
        {
            BookedSeat bookedSeat = await db.BookedSeats.FindAsync(id);
            if (bookedSeat == null)
            {
                return NotFound();
            }

            return Ok(bookedSeat);
        }

        // PUT: api/BookedSeats/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBookedSeat(int id, BookedSeat bookedSeat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bookedSeat.booking_id)
            {
                return BadRequest();
            }

            db.Entry(bookedSeat).State = System.Data.Entity.EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookedSeatExists(id))
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

        // POST: api/BookedSeats
        [ResponseType(typeof(BookedSeat))]
        public async Task<IHttpActionResult> PostBookedSeat(BookedSeat bookedSeat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BookedSeats.Add(bookedSeat);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookedSeatExists(bookedSeat.booking_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bookedSeat.booking_id }, bookedSeat);
        }

        // DELETE: api/BookedSeats/5
        [ResponseType(typeof(BookedSeat))]
        public async Task<IHttpActionResult> DeleteBookedSeat(int id)
        {
            BookedSeat bookedSeat = await db.BookedSeats.FindAsync(id);
            if (bookedSeat == null)
            {
                return NotFound();
            }

            db.BookedSeats.Remove(bookedSeat);
            await db.SaveChangesAsync();

            return Ok(bookedSeat);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookedSeatExists(int id)
        {
            return db.BookedSeats.Count(e => e.booking_id == id) > 0;
        }
    }
}