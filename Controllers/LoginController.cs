using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using V1.Models;

namespace V1.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        private PuneMetroV1Entities2 db = new PuneMetroV1Entities2();

        public IHttpActionResult SignIn(MyClass User)
        {
            var trueUser = (from u in db.Users
                            where u.user_email == User.email && u.password == User.password
                            select u).FirstOrDefault();

            if (trueUser == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(trueUser);
            }
        }
    }
}
