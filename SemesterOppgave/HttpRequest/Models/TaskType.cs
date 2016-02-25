using System.Runtime.Serialization;

namespace HttpRequest.Models
{
    public class TaskType {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Type { get; set; }
    }
}