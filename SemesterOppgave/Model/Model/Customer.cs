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
        string email;
        public string Email
        {
            get { return this.email; }
            set
            {
                if (value != Email)
                {
                    this.email = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        string firstName;
        public string FirstName
        {
            get { return this.firstName; }
            set
            {
                if (value != FirstName)
                {
                    this.firstName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        string lastName;
        public string LastName
        {
            get { return this.lastName; }
            set
            {
                if (value != this.LastName)
                {
                    this.lastName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        string password;
        public string Password
        {
            get { return this.password; }
            set
            {
                if (value != Password)
                {
                    this.password = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
