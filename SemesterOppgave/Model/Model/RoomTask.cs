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
        int id;
        public int Id
        {
            get { return this.id; }
            set
            {
                if(value != Id)
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
        TaskType type;
        public TaskType Type
        {
            get { return this.type; }
            set
            {
                if(value != Type)
                {
                    this.type = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        string comments;
        public string Comments
        {
            get { return this.comments; }
            set
            {
                if(value != Comments)
                {
                    this.comments = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        int status;
        public int Status
        {
            get { return this.status; }
            set
            {
                if(value != Status)
                {
                    this.status = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
