namespace Models {
    public class Room {
        public int Id { get; set; }
        public virtual  RoomSize Size { get; set; }
        public int RoomSizeId { get; set; } // FK
        public virtual RoomBeds Beds { get; set; }
        public int RoomBedsId { get; set; } // FK
        public virtual RoomQuality Quality { get; set; }
        public int RoomQualityId { get; set; } // FK
    }
}