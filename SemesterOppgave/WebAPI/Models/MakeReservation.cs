using System;
using System.Runtime.Serialization;

namespace WebAPI.Models {
    public class MakeReservation {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public DateTime Start { get; set; }
        [DataMember]
        public DateTime End { get; set; }
        [DataMember]
        public string Quality { get; set; }
        [DataMember]
        public int Beds { get; set; }
        [DataMember]
        public string Size { get; set; }
    }
}