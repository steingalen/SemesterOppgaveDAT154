using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    public class RoomQuality
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Quality { get; set; }
    }
}
