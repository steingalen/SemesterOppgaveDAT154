using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementDesktop.ViewModel
{
    /// <summary>
    /// ViewModel for the Reservation view, which handles finding a customer and searching for
    /// and adding/removing reservations as well as finding rooms.
    /// </summary>
    class ReservationViewVM : BasePropertyChanged
    {

        ObservableCollection<ReservationVM> _customerReservations;
        public ObservableCollection<ReservationVM> CustomerReservations { get { return _customerReservations; } private set { _customerReservations = value; NotifyPropertyChanged(); } }

        ObservableCollection<CustomerVM> _customers;
        public ObservableCollection<CustomerVM> Customers { get { return _customers; } private set { _customers = value; NotifyPropertyChanged(); } }

        CustomerVM _selectedCustomer;
        public CustomerVM SelectedCustomer { get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                _selectedCustomer.UpdateReservations();
            }
        }

        string _customerSearchName;
        public string CustomerSearchName { get { return _customerSearchName; } set { _customerSearchName = value;  NotifyPropertyChanged(); } }

        #region Functions
        async void customerSearch()
        {
            string response = await HttpRequest.ApiRequests.Get(HttpRequest.ApiUrl.CUSTOMERS);

            List<HttpRequest.Models.Customer> cust = HttpRequest.JsonSerializer<HttpRequest.Models.Customer>.DeSerializeAsList(response);

            ObservableCollection<CustomerVM> collection = new ObservableCollection<CustomerVM>();

            foreach (HttpRequest.Models.Customer c in cust)
                collection.Add(new CustomerVM(c));

            Customers = collection;
        }

        #endregion Functions

        #region Commands
        Command _customerSearch;
        public Command CustomerSearch
        {
            get
            {
                return _customerSearch ?? (_customerSearch = new Command(() => customerSearch(), true));
            }
        }
        #endregion Commands
    }
}
