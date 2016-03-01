using System.Runtime.Serialization;

namespace Models {
    [DataContract]
    public class TaskType
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Type { get; set; }
    }
}