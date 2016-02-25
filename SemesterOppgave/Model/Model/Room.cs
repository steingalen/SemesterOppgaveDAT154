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
        int roomNumber;
        public int RoomNumber
        {
            get { return this.roomNumber; }
            set
            {
                if (value != RoomNumber)
                {
                    this.roomNumber = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        RoomSize size;
        public RoomSize Size
        {
            get { return this.size; }
            set
            {
                if(value != Size)
                {
                    this.size = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        RoomBeds beds;
        public RoomBeds Beds
        {
            get { return this.beds; }
            set
            {
                if(value != Beds)
                {
                    this.beds = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        RoomQuality quality;
        public RoomQuality Quality
        {
            get { return this.quality; }
            set
            {
                if(value != Quality)
                {
                    this.quality = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
