using System;
using System.Runtime.Serialization;
using System.Web.Security;

namespace WebAPI.Models {
    public class Reservation {
        public int Id { get; set; }
        public int CustomerId { get; set; } // FK
        public virtual Customer Customer { get; set; }
        public int RoomId { get; set; } // FK
        public virtual Room Room { get; set; }
        public DateTime Start { get; set; }
        public DateTime Slutt { get; set; }
    }
}