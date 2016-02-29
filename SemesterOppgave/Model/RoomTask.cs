namespace Models {
    public class RoomTask {

        public int Id { get; set; }
        public int RoomId { get; set; } // FK
        public virtual Room Room { get; set; }
        public int TaskTypeId { get; set; }  //FK
        public virtual TaskType Type { get; set; }
        public int Status { get; set; }
        public string Comments { get; set; }
    }
}