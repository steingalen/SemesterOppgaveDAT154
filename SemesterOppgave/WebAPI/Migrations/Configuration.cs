using Models;

namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<WebAPI.Models.WebAPIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WebAPI.Models.WebAPIContext";
        }

        protected override void Seed(WebAPI.Models.WebAPIContext context)
        {
            

            
            context.Customers.AddOrUpdate(x => x.Id,
                new Customer() { Id = 0, FirstName = "Some", LastName = "Thing", Email = "something@gmail.com"},
                new Customer() { Id = 1, FirstName = "Max", LastName = "Mekker", Email = "max_mekker@gmail.com"},
                new Customer() { Id = 2, FirstName = "Kaptein", LastName = "Sabeltann", Email = "sorte_gull@gmail.com"}
            );

            context.RoomBeds.AddOrUpdate(x => x.Id,
                new RoomBeds() { Id = 0, Beds = 1},
                new RoomBeds() { Id = 1, Beds = 2 },
                new RoomBeds() { Id = 2, Beds = 3 }
            );

            context.RoomSizes.AddOrUpdate(x => x.Id,
                new RoomSize() { Id = 0, Size = "Small" },
                new RoomSize() { Id = 1, Size = "Medium" },
                new RoomSize() { Id = 2, Size = "Large" }
            );

            context.RoomQualities.AddOrUpdate(x => x.Id,
                new RoomQuality() { Id = 0, Quality = "Good" },
                new RoomQuality() { Id = 1, Quality = "Great" },
                new RoomQuality() { Id = 2, Quality = "Exceptional"}
            );

            context.Rooms.AddOrUpdate(x => x.Id,
                new Room() { Id = 0, RoomQualityId = 0, RoomSizeId = 0, RoomBedsId = 0},
                new Room() { Id = 1, RoomQualityId = 1, RoomSizeId = 2, RoomBedsId = 0 },
                new Room() { Id = 2, RoomQualityId = 2, RoomSizeId = 1, RoomBedsId = 2 },
                new Room() { Id = 3, RoomQualityId = 2, RoomSizeId = 0, RoomBedsId = 2 },
                new Room() { Id = 4, RoomQualityId = 1, RoomSizeId = 1, RoomBedsId = 0 },
                new Room() { Id = 5, RoomQualityId = 1, RoomSizeId = 0, RoomBedsId = 2 },
                new Room() { Id = 6, RoomQualityId = 0, RoomSizeId = 2, RoomBedsId = 0 },
                new Room() { Id = 7, RoomQualityId = 2, RoomSizeId = 0, RoomBedsId = 1 },
                new Room() { Id = 8, RoomQualityId = 1, RoomSizeId = 2, RoomBedsId = 0 }
            );

            context.TaskTypes.AddOrUpdate(x => x.Id,
                new TaskType() {Id = 0, Type = "RoomService"},
                new TaskType() {Id = 1, Type = "Maintenance" },
                new TaskType() {Id = 2, Type = "Cleaning"}
            );
       
            context.RoomTasks.AddOrUpdate(x => x.Id,
                new RoomTask() { Id = 0, RoomId = 0, TaskTypeId = 2, Comments = "", Status = 0},
                new RoomTask() { Id = 1, RoomId = 2, TaskTypeId = 2, Comments = "Filty filty room......", Status = 1},
                new RoomTask() { Id = 2, RoomId = 1, TaskTypeId = 0, Comments = "Wants a burger", Status = 0},
                new RoomTask() { Id = 3, RoomId = 1, TaskTypeId = 1, Comments = "Needs a new lightbulb.", Status = 0}
            );

            context.Reservations.AddOrUpdate(x => x.Id,
                new Reservation()  {Id = 0, RoomId = 0, CustomerId = 0, Start = new DateTime(2016, 02, 10, 14, 00 ,0), Slutt = new DateTime(2016, 02, 20, 12, 00, 0) },
                new Reservation() { Id = 1, RoomId = 1, CustomerId = 1, Start = new DateTime(2016, 02, 18, 14, 00, 0), Slutt = new DateTime(2016, 02, 18, 12, 00, 0) },
                new Reservation() { Id = 2, RoomId = 1, CustomerId = 1, Start = new DateTime(2016, 02, 16, 14, 00, 0), Slutt = new DateTime(2016, 02, 18, 12, 00, 0) },
                new Reservation() { Id = 3, RoomId = 1, CustomerId = 2, Start = new DateTime(2016, 02, 18, 14, 00, 0), Slutt = new DateTime(2016, 02, 28, 12, 00, 0) }
                );
            
        }
    }
}
