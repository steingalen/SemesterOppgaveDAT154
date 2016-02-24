using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model.Search
{
    [DataContract]
    class ReservationSearch : INotifyPropertyChanged
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
        public string CustomerName
        {
            get { return this.CustomerName; }
            set
            {
                if(value != CustomerName)
                {
                    this.CustomerName = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
