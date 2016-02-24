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
    public class RoomSearch : INotifyPropertyChanged
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
        public RoomQuality activeRoomQuality
        {
            get { return activeRoomQuality; }
            set
            {
                if(value != activeRoomQuality)
                {
                    this.activeRoomQuality = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        public RoomBeds activeRoomBeds
        {
            get { return this.activeRoomBeds; }
            set
            {
                if(value != activeRoomBeds)
                {
                    this.activeRoomBeds = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        public RoomSize activeRoomSize
        {
            get { return this.activeRoomSize; }
            set
            {
                if(value != activeRoomSize)
                {
                    this.activeRoomSize = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        public DateTime activeDateStart
        {
            get { return this.activeDateStart; }
            set
            {
                if(value != activeDateStart)
                {
                    this.activeDateStart = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        public DateTime activeDateEnd
        {
            get { return this.activeDateEnd; }
            set
            {
                if(value != activeDateEnd)
                {
                    this.activeDateEnd = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
