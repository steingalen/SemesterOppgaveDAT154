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
using Models;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class RoomBedsController : ApiController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: api/RoomBeds
        public IQueryable<RoomBeds> GetRoomBeds()
        {
            return db.RoomBeds;
        }

        // GET: api/RoomBeds/5
        [ResponseType(typeof(RoomBeds))]
        public async Task<IHttpActionResult> GetRoomBeds(int id)
        {
            RoomBeds roomBeds = await db.RoomBeds.FindAsync(id);
            if (roomBeds == null)
            {
                return NotFound();
            }

            return Ok(roomBeds);
        }

        // PUT: api/RoomBeds/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRoomBeds(int id, RoomBeds roomBeds)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roomBeds.Id)
            {
                return BadRequest();
            }

            db.Entry(roomBeds).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomBedsExists(id))
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

        // POST: api/RoomBeds
        [ResponseType(typeof(RoomBeds))]
        public async Task<IHttpActionResult> PostRoomBeds(RoomBeds roomBeds)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RoomBeds.Add(roomBeds);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = roomBeds.Id }, roomBeds);
        }

        // DELETE: api/RoomBeds/5
        [ResponseType(typeof(RoomBeds))]
        public async Task<IHttpActionResult> DeleteRoomBeds(int id)
        {
            RoomBeds roomBeds = await db.RoomBeds.FindAsync(id);
            if (roomBeds == null)
            {
                return NotFound();
            }

            db.RoomBeds.Remove(roomBeds);
            await db.SaveChangesAsync();

            return Ok(roomBeds);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomBedsExists(int id)
        {
            return db.RoomBeds.Count(e => e.Id == id) > 0;
        }
    }
}