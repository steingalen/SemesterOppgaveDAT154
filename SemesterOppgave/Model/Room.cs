using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    class Room
    {
        [DataMember]
        public int RoomNumber { get; set; }

        [DataMember]
        public RoomSize Size { get; set; }

        [DataMember]
        public RoomBeds Beds { get; set; }

        [DataMember]
        public RoomQuality Quality { get; set; }
    }
}
