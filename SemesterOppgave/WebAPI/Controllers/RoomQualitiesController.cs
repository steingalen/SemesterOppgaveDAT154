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
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class RoomQualitiesController : ApiController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: api/RoomQualities
        public IQueryable<RoomQuality> GetRoomQualities()
        {
            return db.RoomQualities;
        }

        // GET: api/RoomQualities/5
        [ResponseType(typeof(RoomQuality))]
        public async Task<IHttpActionResult> GetRoomQuality(int id)
        {
            RoomQuality roomQuality = await db.RoomQualities.FindAsync(id);
            if (roomQuality == null)
            {
                return NotFound();
            }

            return Ok(roomQuality);
        }

        // PUT: api/RoomQualities/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRoomQuality(int id, RoomQuality roomQuality)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roomQuality.Id)
            {
                return BadRequest();
            }

            db.Entry(roomQuality).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomQualityExists(id))
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

        // POST: api/RoomQualities
        [ResponseType(typeof(RoomQuality))]
        public async Task<IHttpActionResult> PostRoomQuality(RoomQuality roomQuality)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RoomQualities.Add(roomQuality);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = roomQuality.Id }, roomQuality);
        }

        // DELETE: api/RoomQualities/5
        [ResponseType(typeof(RoomQuality))]
        public async Task<IHttpActionResult> DeleteRoomQuality(int id)
        {
            RoomQuality roomQuality = await db.RoomQualities.FindAsync(id);
            if (roomQuality == null)
            {
                return NotFound();
            }

            db.RoomQualities.Remove(roomQuality);
            await db.SaveChangesAsync();

            return Ok(roomQuality);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomQualityExists(int id)
        {
            return db.RoomQualities.Count(e => e.Id == id) > 0;
        }
    }
}