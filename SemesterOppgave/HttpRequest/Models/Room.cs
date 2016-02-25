using System.Runtime.Serialization;

namespace HttpRequest.Models
{
    public class Room {
        [DataMember]
        public int Id { get; set; }
        public virtual  RoomSize Size { get; set; }
        [DataMember]
        public int RoomSizeId { get; set; } // FK
        public virtual RoomBeds Beds { get; set; }
        [DataMember]
        public int RoomBedsId { get; set; } // FK
        public virtual RoomQuality Quality { get; set; }
        [DataMember]
        public int RoomQualityId { get; set; } // FK
    }
}