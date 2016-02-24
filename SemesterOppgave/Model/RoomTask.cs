using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    class RoomTask
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Room Room { get; set; }

        [DataMember]
        public TaskType Type { get; set; }

        [DataMember]
        public string Comments { get; set; }

        [DataMember]
        public int Status { get; set; }
    }
}
