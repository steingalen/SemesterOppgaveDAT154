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
        public int Id
        {
            get { return this.Id; }
            set
            {
                if (value != Id)
                {
                    this.Id = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        public Room Room
        {
            get { return this.Room; }
            set
            {
                if(value != Room)
                {
                    this.Room = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        public Customer Customer
        {
            get { return this.Customer; }
            set
            {
                if(value != Customer)
                {
                    this.Customer = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        public DateTime Start
        {
            get { return this.Start; }
            set
            {
                if(value != Start)
                {
                    this.Start = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        public DateTime Slutt
        {
            get { return this.Slutt; }
            set
            {
                if(value != Slutt)
                {
                    this.Slutt = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
