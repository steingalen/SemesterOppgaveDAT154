using System.Runtime.Serialization;

namespace HttpRequest.Models
{
    public class RoomTask {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int RoomId { get; set; } // FK
        public virtual Room Room { get; set; }
        [DataMember]
        public int TaskTypeId { get; set; }  //FK
        public virtual TaskType Type { get; set; }
        [DataMember]
        public int Status { get; set; }
        [DataMember]
        public string Comments { get; set; }
    }
}