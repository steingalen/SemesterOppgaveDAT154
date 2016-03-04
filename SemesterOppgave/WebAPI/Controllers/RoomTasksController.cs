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
    public class RoomTasksController : ApiController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: api/RoomTasks
        public IQueryable<RoomTask> GetRoomTasks()
        {
            return db.RoomTasks;
        }
        
        [ResponseType(typeof(RoomTask))]
        [Route("api/roomtasks/room/{id}")]
        public async Task<IHttpActionResult> GetRoomTasks(int id) {
            var room = await db.Rooms.FirstAsync(r => r.Id == id);

            if (room == null)
            {
                return NotFound();
            }

            var roomTasks = await db.RoomTasks.Where(task => task.RoomId == id).ToListAsync();
            if (roomTasks == null)
            {
                return NotFound();
            }

            return Ok(roomTasks);
        }

        [ResponseType(typeof(RoomTask))]
        [Route("api/roomtasks/tasktype/{id}")]
        public async Task<IHttpActionResult> GetRoomTasksByTaskType(int id)
        {
            var taskType = await db.TaskTypes.FirstAsync(r => r.Id == id);

            if (taskType == null)
            {
                return NotFound();
            }

            var roomTasks = await db.RoomTasks.Where(task => task.TaskTypeId == id && !task.Status.Contains("Finished")).OrderByDescending(t => t.Status).ToListAsync();
            if (roomTasks == null)
            {
                return NotFound();
            }

            return Ok(roomTasks);
        }
        

        /*
        // GET: api/RoomTasks/5
        [ResponseType(typeof(RoomTask))]
        public async Task<IHttpActionResult> GetRoomTask(int id)
        {
            RoomTask roomTask = await db.RoomTasks.FindAsync(id);
            if (roomTask == null)
            {
                return NotFound();
            }

            return Ok(roomTask);
        }*/

        // PUT: api/RoomTasks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRoomTask(int id, RoomTask roomTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roomTask.Id)
            {
                return BadRequest();
            }

            db.Entry(roomTask).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomTaskExists(id))
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

        // POST: api/RoomTasks
        [ResponseType(typeof(RoomTask))]
        public async Task<IHttpActionResult> PostRoomTask(RoomTask roomTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RoomTasks.Add(roomTask);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = roomTask.Id }, roomTask);
        }

        // DELETE: api/RoomTasks/5
        [ResponseType(typeof(RoomTask))]
        public async Task<IHttpActionResult> DeleteRoomTask(int id)
        {
            RoomTask roomTask = await db.RoomTasks.FindAsync(id);
            if (roomTask == null)
            {
                return NotFound();
            }

            db.RoomTasks.Remove(roomTask);
            await db.SaveChangesAsync();

            return Ok(roomTask);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomTaskExists(int id)
        {
            return db.RoomTasks.Count(e => e.Id == id) > 0;
        }
    }
}