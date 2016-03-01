using System.Runtime.Serialization;

namespace Models {
    [DataContract]
    public class RoomBeds
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Beds { get; set; }
    }
}