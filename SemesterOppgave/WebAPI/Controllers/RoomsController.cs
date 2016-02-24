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
    public class RoomsController : ApiController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: api/Rooms
        public IQueryable<Room> GetRooms() {
            return db.Rooms;
        }


        // GET: api/Rooms/Great/2/Large/2016-02-18/2016-02-20
        [ResponseType(typeof(Room))]
        public async Task<IHttpActionResult> GetRoom(string quality, int beds, string size, DateTime start, DateTime slutt) {

            var Quality = db.RoomQualities.FirstOrDefault(x => x.Quality == quality);
            var Size = db.RoomSizes.FirstOrDefault(x => x.Size == size);
            var Beds = db.RoomBeds.FirstOrDefault(x => x.Beds == beds);

            if (Quality == null || Size == null || Beds == null) {
                return NotFound();
            }

            var rooms = db.Rooms.Where(x => x.RoomQualityId == Quality.Id && x.RoomSizeId == Size.Id && x.RoomBedsId == Beds.Id);
            if (!rooms.Any())
            {
                return NotFound();
            }

            //TODO dobbeltskjekke denne linjen?
            var freeRooms = rooms.Where(room => db.Reservations.Any(x => (room.Id == x.RoomId && (slutt < x.Start || start > x.Slutt)) || room.Id != x.RoomId));
            if (!freeRooms.Any())
            {
                return NotFound();
            }

            return Ok(freeRooms);
        }

        // GET: api/Rooms/5
        [ResponseType(typeof(Room))]
        public async Task<IHttpActionResult> GetRoom(int id)
        {
            Room room = await db.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        // PUT: api/Rooms/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRoom(int id, Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.Id)
            {
                return BadRequest();
            }

            db.Entry(room).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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

        // POST: api/Rooms
        [ResponseType(typeof(Room))]
        public async Task<IHttpActionResult> PostRoom(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rooms.Add(room);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = room.Id }, room);
        }

        // DELETE: api/Rooms/5
        [ResponseType(typeof(Room))]
        public async Task<IHttpActionResult> DeleteRoom(int id)
        {
            Room room = await db.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            db.Rooms.Remove(room);
            await db.SaveChangesAsync();

            return Ok(room);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomExists(int id)
        {
            return db.Rooms.Count(e => e.Id == id) > 0;
        }
    }
}