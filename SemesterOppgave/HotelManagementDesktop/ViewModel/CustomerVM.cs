using Models;
using Models.ViewModels;

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
