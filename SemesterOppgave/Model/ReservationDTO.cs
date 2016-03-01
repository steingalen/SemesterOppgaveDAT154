using System;
using Models;
namespace Models {
    public class ReservationDTO {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int RoomId { get; set; }

        public Room Room { get; set; }
        public string Start { get; set; }
        public string Slutt { get; set; }
    }
}