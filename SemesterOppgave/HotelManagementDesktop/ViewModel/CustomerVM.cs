using System;
using System.Collections.ObjectModel;
using Models;
using HttpRequest;
using System.Collections.Generic;

namespace HotelManagementDesktop.ViewModel
{
    class CustomerVM : BasePropertyChanged
    {
        internal Customer Customer;

        public string Email { get { return Customer.Email; } set { Customer.Email = value;  NotifyPropertyChanged(); } }

        public string FirstName { get { return Customer.FirstName; } set { Customer.FirstName = value;  NotifyPropertyChanged(); } }

        public string LastName { get { return Customer.LastName; } set { Customer.LastName = value;  NotifyPropertyChanged(); } }

        public CustomerVM(Customer customer)
        {
            Customer = customer;
        }
    }
}
