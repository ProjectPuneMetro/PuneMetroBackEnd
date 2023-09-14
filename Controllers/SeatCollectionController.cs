using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using System.Web.Http;
using System.Web.Mvc;
using V1.Models;
using System.Web.Http.Cors;

namespace V1.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SeatCollectionController : ApiController
    {
        public SeatCollectionController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        private PuneMetroV1Entities2 db = new PuneMetroV1Entities2();

        [System.Web.Http.HttpGet]
        [ResponseType(typeof(IEnumerable<object>))]
        public IHttpActionResult SeatCollections(int trainId)
        {
            var dataCollection = from s in db.Seats
                                 join c in db.Coaches on s.coach_id equals c.coach_Id
                                 join t in db.Trains on c.train_id equals t.train_id
                                 where t.train_id == trainId
                                 select new
                                 {
                                     c.train_id,
                                     s.seat_id,
                                     s.coach_id,
                                     t.train_name,
                                     s.seat_number,
                                     s.availability,
                                     c.coach_name,
                                     t.price,
                                     t.arrival_time,
                                     t.departure_time

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