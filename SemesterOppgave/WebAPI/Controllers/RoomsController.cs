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
        [Route("api/rooms/{quality}/{beds}/{size}/{start}/{end}")]
        [ResponseType(typeof(List<Room>))]
        public async Task<IHttpActionResult> GetRooms(int quality, int beds, int size, string start, string end) {

            // Converts string to datetime
            DateTime _end;
            DateTime _start;
            try {
                _start = Convert.ToDateTime(start);
                 _end = Convert.ToDateTime(end);

                if (_start > _end)
                    return NotFound();
            } catch (FormatException) {
                return NotFound();
            }
            
            // Fetches rooms baseed on parameters
            var rooms = db.Rooms;

            if (quality != 0) {
                var _quality = db.RoomQualities.FirstOrDefault(x => x.Id == quality);
                if (_quality == null)
                    return NotFound();
                rooms.Where(x => x.RoomQualityId == _quality.Id);
            }
            if (size != 0) {
                var _size = db.RoomSizes.FirstOrDefault(x => x.Id == size);
                if (_size == null)
                    return NotFound();
                rooms.Where(x => x.RoomSizeId == _size.Id);
            }
            if (beds != 0) {
                var _beds = db.RoomBeds.FirstOrDefault(x => x.Beds == beds);
                if (_beds == null)
                    return NotFound();
                rooms.Where(x => x.RoomBedsId == _beds.Id);
            }


            if (!rooms.Any()) {
                return NotFound();
            }

            var reservatedRooms =
                await db.Reservations.Where(
                    reservation =>
                        !(_end.Date < DbFunctions.TruncateTime(reservation.Start) ||
                        _start.Date > DbFunctions.TruncateTime(reservation.Slutt)))
                        .Select(reservation => reservation.Room)
                        .ToListAsync();

            var usableRooms = new List<Room>();


            foreach (var room in rooms) {
                if (!reservatedRooms.Contains(room))
                    usableRooms.Add(room);
            }
            
            return Ok(usableRooms);
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