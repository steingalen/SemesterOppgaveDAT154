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
    public class RoomTask : INotifyPropertyChanged
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
                if(value != Id)
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
        public TaskType Type
        {
            get { return this.Type; }
            set
            {
                if(value != Type)
                {
                    this.Type = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        public string Comments
        {
            get { return this.Comments; }
            set
            {
                if(value != Comments)
                {
                    this.Comments = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        public int Status
        {
            get { return this.Status; }
            set
            {
                if(value != Status)
                {
                    this.Status = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
