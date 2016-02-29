using System.Data.Entity;
using Models;

namespace WebAPI.Models
{
    public class WebAPIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public WebAPIContext() : base("name=WebAPIContext")
        {
        }

        public System.Data.Entity.DbSet<Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<TaskType> TaskTypes { get; set; }

        public System.Data.Entity.DbSet<RoomSize> RoomSizes { get; set; }

        public System.Data.Entity.DbSet<RoomQuality> RoomQualities { get; set; }

        public System.Data.Entity.DbSet<RoomBeds> RoomBeds { get; set; }

        public System.Data.Entity.DbSet<Room> Rooms { get; set; }

        public System.Data.Entity.DbSet<Reservation> Reservations { get; set; }

        public System.Data.Entity.DbSet<RoomTask> RoomTasks { get; set; }
    }
}
