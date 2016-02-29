using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;
using Models;

namespace WebAPI.Controllers
{
    public class RoomSizesController : ApiController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: api/RoomSizes
        public IQueryable<RoomSize> GetRoomSizes()
        {
            return db.RoomSizes;
        }

        // GET: api/RoomSizes/5
        [ResponseType(typeof(RoomSize))]
        public async Task<IHttpActionResult> GetRoomSize(int id)
        {
            RoomSize roomSize = await db.RoomSizes.FindAsync(id);
            if (roomSize == null)
            {
                return NotFound();
            }

            return Ok(roomSize);
        }

        // PUT: api/RoomSizes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRoomSize(int id, RoomSize roomSize)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roomSize.Id)
            {
                return BadRequest();
            }

            db.Entry(roomSize).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomSizeExists(id))
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

        // POST: api/RoomSizes
        [ResponseType(typeof(RoomSize))]
        public async Task<IHttpActionResult> PostRoomSize(RoomSize roomSize)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RoomSizes.Add(roomSize);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = roomSize.Id }, roomSize);
        }

        // DELETE: api/RoomSizes/5
        [ResponseType(typeof(RoomSize))]
        public async Task<IHttpActionResult> DeleteRoomSize(int id)
        {
            RoomSize roomSize = await db.RoomSizes.FindAsync(id);
            if (roomSize == null)
            {
                return NotFound();
            }

            db.RoomSizes.Remove(roomSize);
            await db.SaveChangesAsync();

            return Ok(roomSize);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomSizeExists(int id)
        {
            return db.RoomSizes.Count(e => e.Id == id) > 0;
        }
    }
}