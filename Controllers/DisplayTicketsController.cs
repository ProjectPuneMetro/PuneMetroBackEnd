using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using V1.Models;

namespace V1.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DisplayTicketsController : ApiController
    {
        public DisplayTicketsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        private PuneMetroV1Entities2 db = new PuneMetroV1Entities2();

        [HttpGet]
        [ResponseType(typeof(object))]
        public IHttpActionResult DisplayTicket(int bookingId)
        {
            var data = (from bs in db.BookedSeats
                        join t in db.Trains on bs.train_id equals t.train_id
                        join u in db.Users on bs.user_id equals u.user_id
                        join c in db.Coaches on bs.coach_id equals c.coach_Id
                        join s in db.Seats on bs.seat_id equals s.seat_id
                        where bs.booking_id == bookingId
                        select new
                        {
                            bs.booking_id,
                            t.source_station,
                            t.destination_station,
                            u.user_name,
                            t.train_name,
                            c.coach_name,
                            s.seat_number
                        }).SingleOrDefault();  // Retrieve a single object

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }
    }
}
