using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Locusnine_CURD.Models;
using System.Web.Http.Cors;

namespace Locusnine_CURD.Controllers
{

    //[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class UserDatasController : ApiController
    {
        private LocusnineEntities db = new LocusnineEntities();

        // GET: api/UserDatas
        public IQueryable<UserData> GetUserDatas()
        {
            return db.UserDatas;
        }

        // GET: api/UserDatas/5
        [ResponseType(typeof(UserData))]
        public IHttpActionResult GetUserData(long id)
        {
            UserData userData = db.UserDatas.Find(id);
            if (userData == null)
            {
                return NotFound();
            }

            return Ok(userData);
        }

        // PUT: api/UserDatas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserData(long id, UserData userData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userData.UserID)
            {
                return BadRequest();
            }

            db.Entry(userData).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDataExists(id))
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

        // POST: api/UserDatas
        [ResponseType(typeof(UserData))]
        public IHttpActionResult PostUserData(UserData userData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserDatas.Add(userData);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userData.UserID }, userData);
        }

        // DELETE: api/UserDatas/5
        [ResponseType(typeof(UserData))]
        public IHttpActionResult DeleteUserData(long id)
        {
            UserData userData = db.UserDatas.Find(id);
            if (userData == null)
            {
                return NotFound();
            }

            db.UserDatas.Remove(userData);
            db.SaveChanges();

            return Ok(userData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserDataExists(long id)
        {
            return db.UserDatas.Count(e => e.UserID == id) > 0;
        }
    }
}