using System.Runtime.Serialization;

namespace HttpRequest.Models
{
    public class RoomQuality {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Quality { get; set; }
    }
}