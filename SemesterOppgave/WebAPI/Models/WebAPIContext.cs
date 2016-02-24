using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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

        public System.Data.Entity.DbSet<WebAPI.Models.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<WebAPI.Models.TaskType> TaskTypes { get; set; }

        public System.Data.Entity.DbSet<WebAPI.Models.RoomSize> RoomSizes { get; set; }

        public System.Data.Entity.DbSet<WebAPI.Models.RoomQuality> RoomQualities { get; set; }

        public System.Data.Entity.DbSet<WebAPI.Models.RoomBeds> RoomBeds { get; set; }

        public System.Data.Entity.DbSet<WebAPI.Models.Room> Rooms { get; set; }

        public System.Data.Entity.DbSet<WebAPI.Models.Reservation> Reservations { get; set; }

        public System.Data.Entity.DbSet<WebAPI.Models.RoomTask> RoomTasks { get; set; }
    }
}
