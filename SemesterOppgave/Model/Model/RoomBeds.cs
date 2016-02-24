using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    public class RoomBeds
    {
        [DataMember]
        public int Id { get; internal set; }

        [DataMember]
        public int Beds { get; internal set; }
    }
}
