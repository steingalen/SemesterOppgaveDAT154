using System.Runtime.Serialization;

namespace Models {
    [DataContract]
    public class RoomQuality
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Quality { get; set; }
    }
}