using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using System.Web.Http;
using System.Web.Mvc;
using V1.Models;

namespace V1.Controllers
{
    public class BookingHistoryController : ApiController
    {
        public BookingHistoryController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        private PuneMetroV1Entities2 db = new PuneMetroV1Entities2();

        [System.Web.Http.HttpGet]
        [ResponseType(typeof(IEnumerable<object>))]
        public IHttpActionResult BookingHistory(int userId)
        {
            var dataCollection = from b in db.BookedSeats
                                 
                                 join t in db.Trains on b.train_id equals t.train_id
                                 join c in db.Coaches on b.coach_id equals c.coach_Id
                                 join s in db.Seats on b.seat_id equals s.seat_id
                                 where b.user_id == userId 
                                 select new
                                 {
                                      t.train_name,
                                     s.seat_number,
                                      c.coach_name,                          
                                      s.availability,
                                      t.price,
                                    b.booking_id
                                 
        };
            if (!dataCollection.Any())  // Check if the collection is empty
            {
                return NotFound();
            }
            else
            {
                return Ok(dataCollection);
            }
        }
    }
}