using System.Collections.Generic;
using System.Collections.ObjectModel;
using Models;
using HttpRequest;
using System;
using System.Runtime.Serialization;

namespace HotelManagementDesktop.ViewModel
{
    /// <summary>
    /// ViewModel for the Reservation view, which handles finding a customer and searching for
    /// and adding/removing reservations as well as finding rooms.
    /// </summary>
    class ReservationViewVM : BasePropertyChanged
    {
        CustomerVM _activeCustomer;
        public CustomerVM ActiveCustomer { get { return _activeCustomer; }
            set
            {
                _activeCustomer = value;
                _activeCustomer.UpdateReservations();
                NotifyPropertyChanged();
            }
        }

        #region CustomerSearch
        bool _customerSearchInProgress = false;
        public bool CustomerSearchInProgress
        {
            get { return _customerSearchInProgress; }
            set { _customerSearchInProgress = value; NotifyPropertyChanged(); }
        }

        bool _customerFound = false;
        public bool CustomerFound
        {
            get { return _customerFound; }
            set { _customerFound = value; NotifyPropertyChanged(); }
        }

        bool _customerNotFound = false;
        public bool CustomerNotFound
        {
            get { return _customerNotFound; }
            set { _customerNotFound = value; NotifyPropertyChanged(); }
        }
        #endregion CustomerSearch

        string _customerSearchEmail;
        public string CustomerEmail { get { return _customerSearchEmail; } set { _customerSearchEmail = value;  NotifyPropertyChanged(); } }

        #region Functions
        async void customerSearch()
        {
            CustomerFound = false;
            CustomerNotFound = false;
            CustomerSearchInProgress = true;

            string response = await ApiRequests.Post(ApiUrl.CUSTOMER_SEARCH, "{\"Email\":\"" + CustomerEmail + "\"}");
            
            Customer customer = HttpRequest.JsonSerializer<Customer>.DeSerialize(response);
            Console.Write(customer + "  " + customer.FirstName);
            if (customer.Id != 0)
            {
                CustomerFound = true;
                ActiveCustomer = new CustomerVM(customer);
            }
            else
                CustomerNotFound = true;

            CustomerSearchInProgress = false;
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
