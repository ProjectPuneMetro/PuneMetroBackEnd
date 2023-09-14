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
using System.Web.Http.Cors;
using V1.Models;

namespace V1.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TrainsController : ApiController
    {
        private PuneMetroV1Entities2 db = new PuneMetroV1Entities2();
        public TrainsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Trains
        public IQueryable<Train> GetTrains()
        {
            return db.Trains;
        }

        // GET: api/Trains/5
        [ResponseType(typeof(Train))]
        public async Task<IHttpActionResult> GetTrain(int id)
        {
            Train train = await db.Trains.FindAsync(id);
            if (train == null)
            {
                return NotFound();
            }

            return Ok(train);
        }

        // PUT: api/Trains/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTrain(int id, Train train)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != train.train_id)
            {
                return BadRequest();
            }

            db.Entry(train).State = System.Data.Entity.EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainExists(id))
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

        // POST: api/Trains
        [ResponseType(typeof(Train))]
        public async Task<IHttpActionResult> PostTrain(Train train)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Trains.Add(train);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TrainExists(train.train_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = train.train_id }, train);
        }

        // DELETE: api/Trains/5
        [ResponseType(typeof(Train))]
        public async Task<IHttpActionResult> DeleteTrain(int id)
        {
            Train train = await db.Trains.FindAsync(id);
            if (train == null)
            {
                return NotFound();
            }

            db.Trains.Remove(train);
            await db.SaveChangesAsync();

            return Ok(train);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TrainExists(int id)
        {
            return db.Trains.Count(e => e.train_id == id) > 0;
        }
    }
}