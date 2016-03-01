using System.Runtime.Serialization;

namespace Models {
    [DataContract]
    public class Room
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public virtual  RoomSize Size { get; set; }
        [DataMember]
        public int RoomSizeId { get; set; } // FK
        [DataMember]
        public virtual RoomBeds Beds { get; set; }
        [DataMember]
        public int RoomBedsId { get; set; } // FK
        [DataMember]
        public virtual RoomQuality Quality { get; set; }
        [DataMember]
        public int RoomQualityId { get; set; } // FK
    }
}