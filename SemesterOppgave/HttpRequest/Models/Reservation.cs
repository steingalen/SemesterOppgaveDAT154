using System;
using System.Runtime.Serialization;

namespace HttpRequest.Models
{
    public class Reservation {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CustomerId { get; set; } // FK
        public virtual Customer Customer { get; set; }
        [DataMember]
        public int RoomId { get; set; } // FK
        public virtual Room Room { get; set; }
        [DataMember]
        public DateTime Start { get; set; }
        [DataMember]
        public DateTime Slutt { get; set; }
    }
}