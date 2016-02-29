using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;
using Models;

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
        [Route("api/rooms/{_quality}/{_beds}/{_size}/{start}/{end}")]
        [ResponseType(typeof(List<Room>))]
        public async Task<IHttpActionResult> GetRooms(int _quality, int _beds, int _size, string start, string end) {

            // Converts string to datetime
            DateTime _end;
            DateTime _start;
            try {
                _start = Convert.ToDateTime(start);
                 _end = Convert.ToDateTime(end);

                if (_start >= _end)
                    return NotFound();
            } catch (FormatException) {
                return NotFound();
            }

            // Fetches rooms baseed on parameters
            var queryyRooms = db.Rooms;
            Expression<Func<Room, bool>> qualityExpression = x => true;
            Expression<Func<Room, bool>> sizeExpression = x => true;
            Expression<Func<Room, bool>> bedsExpression = x => true;



            if (_quality != 0)
          {
              var quality = db.RoomQualities.FirstOrDefault(x => x.Id == _quality);
              if (quality == null)
                  return NotFound();
                qualityExpression = (x => x.RoomQualityId == quality.Id);
          }
          if (_size != 0)
          {
              var size = db.RoomSizes.FirstOrDefault(x => x.Id == _size);
              if (size == null)
                  return NotFound();
                sizeExpression = x => x.RoomSizeId == size.Id;
          }
          if (_beds != 0)
          {
              var beds = db.RoomBeds.FirstOrDefault(x => x.Id == _beds);
              if (beds == null)
                  return NotFound();
                bedsExpression = x => x.RoomBedsId == beds.Id;
          }

            var rooms = queryyRooms.Where(qualityExpression).Where(sizeExpression).Where(bedsExpression).ToList();

            if (rooms.Count == 0) {
                return NotFound();
            }

            var reservatedRooms =
                await db.Reservations.Where(
                    reservation =>
                        !(_end.Date < DbFunctions.TruncateTime(reservation.Start) ||
                        _start.Date > DbFunctions.TruncateTime(reservation.Slutt)))
                        .Select(reservation => reservation.Room)
                        .ToListAsync();

            var usableRooms = rooms.Where(room => !reservatedRooms.Contains(room)).ToList();


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