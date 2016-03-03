using System.Runtime.Serialization;

namespace Models {
    [DataContract]
    public class RoomTask {

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int RoomId { get; set; } // FK
        [DataMember]
        public virtual Room Room { get; set; }
        [DataMember]
        public int TaskTypeId { get; set; }  //FK
        [DataMember]
        public virtual TaskType Type { get; set; }

        [DataMember]
        public string Status { get; set; } = "";

        [DataMember]
        public string Comments { get; set; } = "";
    }
}