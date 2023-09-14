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
    public class CoachesController : ApiController
    {
        public CoachesController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        private PuneMetroV1Entities2 db = new PuneMetroV1Entities2();

        // GET: api/Coaches
        public IQueryable<Coach> GetCoaches()
        {
            return db.Coaches;
        }

        // GET: api/Coaches/5
        [ResponseType(typeof(Coach))]
        public async Task<IHttpActionResult> GetCoach(int id)
        {
            Coach coach = await db.Coaches.FindAsync(id);
            if (coach == null)
            {
                return NotFound();
            }

            return Ok(coach);
        }

        // PUT: api/Coaches/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCoach(int id, Coach coach)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coach.coach_Id)
            {
                return BadRequest();
            }

            db.Entry(coach).State = System.Data.Entity.EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoachExists(id))
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

        // POST: api/Coaches
        [ResponseType(typeof(Coach))]
        public async Task<IHttpActionResult> PostCoach(Coach coach)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Coaches.Add(coach);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = coach.coach_Id }, coach);
        }

        // DELETE: api/Coaches/5
        [ResponseType(typeof(Coach))]
        public async Task<IHttpActionResult> DeleteCoach(int id)
        {
            Coach coach = await db.Coaches.FindAsync(id);
            if (coach == null)
            {
                return NotFound();
            }

            db.Coaches.Remove(coach);
            await db.SaveChangesAsync();

            return Ok(coach);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoachExists(int id)
        {
            return db.Coaches.Count(e => e.coach_Id == id) > 0;
        }
    }
}