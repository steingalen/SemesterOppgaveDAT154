using System.Runtime.Serialization;

namespace HttpRequest.Models
{
    public class RoomSize {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Size { get; set; }
    }
}