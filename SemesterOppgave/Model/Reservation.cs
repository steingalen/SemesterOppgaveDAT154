using System;
using System.Runtime.Serialization;

namespace Models {
    [DataContract]
    public class Reservation
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CustomerId { get; set; } // FK
        [DataMember]
        public virtual Customer Customer { get; set; }
        [DataMember]
        public int RoomId { get; set; } // FK
        [DataMember]
        public virtual Room Room { get; set; }
        
        [DataMember]
        public DateTime Start { get; set; }
        [DataMember]
        public DateTime Slutt { get; set; }

        public void FromReservationDTO(ReservationDTO dto)
        {
            Id = dto.Id;
            CustomerId = dto.CustomerId;
            Customer = dto.Customer;
            RoomId = dto.RoomId;
            Room = dto.Room;

            Start = Convert.ToDateTime(dto.Start);
            Slutt = Convert.ToDateTime(dto.Slutt);
        }

        public Reservation()
        {
            Start = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            DateTime tomorrow = DateTime.Today.AddDays(1);
            Slutt = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day);
            
        }
    }
}