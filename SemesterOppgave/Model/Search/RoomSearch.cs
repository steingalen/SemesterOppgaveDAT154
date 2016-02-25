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
        RoomQuality activeRoomQuality;
        public RoomQuality ActiveRoomQuality
        {
            get { return activeRoomQuality; }
            set
            {
                if(value != ActiveRoomQuality)
                {
                    this.activeRoomQuality = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        RoomBeds activeRoomBeds;
        public RoomBeds ActiveRoomBeds
        {
            get { return this.activeRoomBeds; }
            set
            {
                if(value != ActiveRoomBeds)
                {
                    this.activeRoomBeds = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        RoomSize activeRoomSize;
        public RoomSize ActiveRoomSize
        {
            get { return this.activeRoomSize; }
            set
            {
                if(value != ActiveRoomSize)
                {
                    this.activeRoomSize = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        DateTime activeDateStart;
        public DateTime ActiveDateStart
        {
            get { return this.activeDateStart; }
            set
            {
                if(value != ActiveDateStart)
                {
                    this.activeDateStart = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        DateTime activeDateEnd;
        public DateTime ActiveDateEnd
        {
            get { return this.activeDateEnd; }
            set
            {
                if(value != ActiveDateEnd)
                {
                    this.activeDateEnd = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
