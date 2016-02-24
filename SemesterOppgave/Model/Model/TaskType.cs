using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    public class TaskType
    {
        [DataMember]
        public int Id { get; internal set; }

        [DataMember]
        public string Type { get; internal set; }
    }
}
