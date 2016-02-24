using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    class Reservation
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Room Room { get; set; }

        [DataMember]
        public Customer Customer { get; set; }

        [DataMember]
        public DateTime Start { get; set; }

        [DataMember]
        public DateTime Slutt { get; set; }
    }
}
