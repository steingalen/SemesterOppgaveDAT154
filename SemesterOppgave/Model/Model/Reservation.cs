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
    public class Reservation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [DataMember]
        int id;
        public int Id
        {
            get { return this.id; }
            set
            {
                if (value != Id)
                {
                    this.id = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        Room room;
        public Room Room
        {
            get { return this.room; }
            set
            {
                if(value != Room)
                {
                    this.room = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        Customer customer;
        public Customer Customer
        {
            get { return this.customer; }
            set
            {
                if(value != Customer)
                {
                    this.customer = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        DateTime start;
        public DateTime Start
        {
            get { return this.start; }
            set
            {
                if(value != Start)
                {
                    this.start = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        DateTime slutt;
        public DateTime Slutt
        {
            get { return this.slutt; }
            set
            {
                if(value != Slutt)
                {
                    this.slutt = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
