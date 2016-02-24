using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    class TaskType
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Type { get; set; }
    }
}
