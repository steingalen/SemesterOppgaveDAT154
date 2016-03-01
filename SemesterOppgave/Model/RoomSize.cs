using System.Runtime.Serialization;

namespace Models {
    [DataContract]
    public class RoomSize
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Size { get; set; }
    }
}