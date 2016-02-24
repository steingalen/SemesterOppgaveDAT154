using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
    [DataContract]
    public class Customer : INotifyPropertyChanged
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
        public string Email
        {
            get { return this.Email; }
            set
            {
                if (value != Email)
                {
                    this.Email = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        public string FirstName
        {
            get { return this.FirstName; }
            set
            {
                if (value != FirstName)
                {
                    this.FirstName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        public string LastName
        {
            get { return this.LastName; }
            set
            {
                if (value != this.FirstName)
                {
                    this.FirstName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        public string Password
        {
            get { return this.Password; }
            set
            {
                if (value != Password)
                {
                    this.Password = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
