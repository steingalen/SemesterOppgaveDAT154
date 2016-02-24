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
    public class Room : INotifyPropertyChanged
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
        public int RoomNumber
        {
            get { return this.RoomNumber; }
            set
            {
                if (value != RoomNumber)
                {
                    this.RoomNumber = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        public RoomSize Size
        {
            get { return this.Size; }
            set
            {
                if(value != Size)
                {
                    this.Size = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        public RoomBeds Beds
        {
            get { return this.Beds; }
            set
            {
                if(value != Beds)
                {
                    this.Beds = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        public RoomQuality Quality
        {
            get { return this.Quality; }
            set
            {
                if(value != Quality)
                {
                    this.Quality = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
