using System;
using System.Collections.ObjectModel;
using Models;
using HttpRequest;
using System.Collections.Generic;

namespace HotelManagementDesktop.ViewModel
{
    class CustomerVM : BasePropertyChanged
    {
        Customer _customer;

        public string Email { get { return _customer.Email; } set { _customer.Email = value;  NotifyPropertyChanged(); } }

        public string FirstName { get { return _customer.FirstName; } set { _customer.FirstName = value;  NotifyPropertyChanged(); } }

        public string LastName { get { return _customer.LastName; } set { _customer.LastName = value;  NotifyPropertyChanged(); } }

        #region ReservationSearch
        bool _reservationSearchInProgress = false;
        public bool ReservationSearchInProgress
        {
            get { return _reservationSearchInProgress; }
            set { _reservationSearchInProgress = value; NotifyPropertyChanged(); }
        }

        bool _reservationsFound = false;
        public bool ReservationsFound
        {
            get { return _reservationsFound; }
            set { _reservationsFound = value; NotifyPropertyChanged(); }
        }

        bool _reservationsNotFound = false;
        public bool ReservationsNotFound
        {
            get { return _reservationsNotFound; }
            set { _reservationsNotFound = value; NotifyPropertyChanged(); }
        }
        #endregion ReservationSearch

        ObservableCollection<ReservationVM> _reservations;
        public ObservableCollection<ReservationVM> Reservations
        {
            get
            {
                return _reservations;
            }
            set { _reservations = value; NotifyPropertyChanged(); }
        }

        #region Functions
        void newReservation()
        {
            ReservationVM newReservation = new ReservationVM(this);
            Reservations.Add(newReservation);
        }

        public async void UpdateReservations()
        {
            ReservationSearchInProgress = true;
            ReservationsFound = false;
            ReservationsNotFound = false;

            string reservationsString = await ApiRequests.Get(ApiUrl.RESERVATIONS_BY_CUSTOMER, _customer.Id);

            List<ReservationDTO> reservationsList = JsonSerializer<ReservationDTO>.DeSerializeAsList(reservationsString);

            if(reservationsList.Count != 0)
            {
                ReservationsFound = true;
            }
            else
                ReservationsNotFound = true;

            ReservationSearchInProgress = false;
            
            ObservableCollection<ReservationVM> collection = new ObservableCollection<ReservationVM>();

            foreach (ReservationDTO c in reservationsList)
            {
                Reservation newReservation = new Reservation();
                newReservation.FromReservationDTO(c);
                collection.Add(new ReservationVM(newReservation));
            }

            Reservations = collection;
        }

        void deleteReservation(ReservationVM a)
        {
            a.DeleteReservation();
            Reservations.Remove(a);
            // Rest taken care of in VM itself

            UpdateReservations();
        }

        void updateCreateReservation(ReservationVM a)
        {
            a.UpdateCreateReservation();

            UpdateReservations();
        }
        #endregion Functions

        #region Commands
        Command _newReservation;
        public Command NewReservation
        {
            get
            {
                return _newReservation ?? (_newReservation = new Command(() => newReservation(), true));
            }
        }

        CommandPara1 _deleteReservation;
        public CommandPara1 DeleteReservation
        {
            get
            {
                return _deleteReservation ?? (_deleteReservation = new CommandPara1((object a) => deleteReservation((ReservationVM)a), true));
            }
        }

        CommandPara1 _updateCreateReservation;
        public CommandPara1 UpdateCreateReservation
        {
            get
            {
                return _updateCreateReservation ?? (_updateCreateReservation = new CommandPara1((object a) => updateCreateReservation((ReservationVM)a), true));
            }
        }
        #endregion Commands

        public CustomerVM(Customer customer)
        {
            _customer = customer;
        }
    }
}
