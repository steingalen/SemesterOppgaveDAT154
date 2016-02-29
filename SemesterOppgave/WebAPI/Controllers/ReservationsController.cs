using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ReservationsController : ApiController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: api/Reservations
        public IQueryable<Reservation> GetReservations()
        {
            return db.Reservations;
        }

        // GET: api/Reservations/5
        [ResponseType(typeof(Reservation))]
        public async Task<IHttpActionResult> GetReservation(int id)
        {
            Reservation reservation = await db.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        // GET: api/Reservations/fornavn/etternavn
        [ResponseType(typeof(Reservation))]
        [Route("api/reservations/{firstname}/{lastname}")]
        public async Task<IHttpActionResult> GetReservations(string firstname, string lastname) {
            var customer = await db.Customers.FirstAsync((x => x.FirstName == firstname && x.LastName == lastname));

            if (customer == null)
            {
                return NotFound();
            }

            var reservation = db.Reservations.Where(x => x.CustomerId == customer.Id);
            if (!reservation.Any())
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        // GET: api/Reservations/email
        [ResponseType(typeof(Reservation))]
        [Route("api/reservations/{email}/")]
        public async Task<IHttpActionResult> GetReservations(string email)
        {
            var customer = await db.Customers.FirstAsync((x => x.Email == email));
            if (customer == null)
            {
                return NotFound();
            }

            var reservation = db.Reservations.Where(x => x.CustomerId == customer.Id);
            if (!reservation.Any())
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        // PUT: api/Reservations/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutReservation(int id, Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservation.Id)
            {
                return BadRequest();
            }

            db.Entry(reservation).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
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

        // POST: api/Reservations
        [ResponseType(typeof(Reservation))]
        [Route("api/makereservations", Name= "MakeReservation")]
        public async Task<IHttpActionResult> PostReservation(MakeReservation makeReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var customer = db.Customers.First(c => c.Email == makeReservation.Email);
            if (customer == null)
                return BadRequest(ModelState);

            // Converts string to datetime
            DateTime _end;
            DateTime _start;
            try
            {
                _start = Convert.ToDateTime(makeReservation.Start);
                _end = Convert.ToDateTime(makeReservation.End);

                if (_start >= _end)
                    return NotFound();
            }
            catch (FormatException)
            {
                return NotFound();
            }

            // Fetches rooms baseed on parameters
            var queryyRooms = db.Rooms;
            Expression<Func<Room, bool>> qualityExpression = x => true;
            Expression<Func<Room, bool>> sizeExpression = x => true;
            Expression<Func<Room, bool>> bedsExpression = x => true;



            if (!makeReservation.Quality.Equals("Any"))
            {
                var quality = db.RoomQualities.FirstOrDefault(x => x.Quality == makeReservation.Quality);
                if (quality == null)
                    return NotFound();
                qualityExpression = (x => x.RoomQualityId == quality.Id);
            }
            if (!makeReservation.Size.Equals("Any"))
            {
                var size = db.RoomSizes.FirstOrDefault(x => x.Size == makeReservation.Size);
                if (size == null)
                    return NotFound();
                sizeExpression = x => x.RoomSizeId == size.Id;
            }
            if (makeReservation.Beds != 0)
            {
                var beds = db.RoomBeds.FirstOrDefault(x => x.Beds == makeReservation.Beds);
                if (beds == null)
                    return NotFound();
                bedsExpression = x => x.RoomBedsId == beds.Id;
            }

            var rooms = queryyRooms.Where(qualityExpression).Where(sizeExpression).Where(bedsExpression).ToList();


            if (rooms.Count == 0)
            {
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


            if (usableRooms.Count == 0)
                return NotFound();



            var newReservation = new Reservation() {
                RoomId = usableRooms[0].Id,
                Start = makeReservation.Start,
                Slutt = makeReservation.End,
                CustomerId = customer.Id,
            };

            
            db.Reservations.Add(newReservation);
            await db.SaveChangesAsync();

            return CreatedAtRoute("MakeReservation", new { id = newReservation.Id }, newReservation);
        }

        // POST: api/Reservations
        [ResponseType(typeof(Reservation))]
        public async Task<IHttpActionResult> PostReservation(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reservations.Add(reservation);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = reservation.Id }, reservation);
        }

        // DELETE: api/Reservations/5
        [ResponseType(typeof(Reservation))]
        public async Task<IHttpActionResult> DeleteReservation(int id)
        {
            Reservation reservation = await db.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            db.Reservations.Remove(reservation);
            await db.SaveChangesAsync();

            return Ok(reservation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReservationExists(int id)
        {
            return db.Reservations.Count(e => e.Id == id) > 0;
        }
    }
}