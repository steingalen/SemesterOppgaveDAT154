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
        #region Customer

        #region Active
        private CustomerVM _activeCustomer;
        public CustomerVM ActiveCustomer
        {
            get { return _activeCustomer; }
            set
            {
                _activeCustomer = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SearchState
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
        #endregion
        #region SearchString
        string _customerSearchEmail = "";
        public string CustomerEmail { get { return _customerSearchEmail; } set { _customerSearchEmail = value; NotifyPropertyChanged(); } }
        #endregion
        #region Functions
        async void customerSearch()
        {
            if (CustomerEmail.Length < 1)
                return;

            CustomerFound = false;
            CustomerNotFound = false;
            CustomerSearchInProgress = true;

            string response = await ApiRequests.Post(ApiUrl.CUSTOMER_SEARCH, "{\"Email\":\"" + CustomerEmail + "\"}");

            if (response.Length > 0) // Empty response = no hits, nothing to serialize
            {
                Customer customer = HttpRequest.JsonSerializer<Customer>.DeSerialize(response);

                if (customer.Id != 0)
                {
                    CustomerFound = true;
                    ActiveCustomer = new CustomerVM(customer);
                    updateReservations();
                }
                else
                    CustomerNotFound = true;
            }
            else
                CustomerNotFound = true;

            CustomerSearchInProgress = false;
        }
#endregion
        #region Commands
        Command _customerSearch;
        public Command CustomerSearch
        {
            get
            {
                return _customerSearch ?? (_customerSearch = new Command(() => customerSearch(), true));
            }
        }
        #endregion

        #endregion

        #region Reservations

        #region Active

        private ReservationVM _activeReservation;
        public ReservationVM ActiveReservation
        {
            get { return _activeReservation; }
            set { _activeReservation = value; NotifyPropertyChanged(); }
        }

        #endregion
        #region SearchState
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
        #endregion
        #region Collection
        ObservableCollection<ReservationVM> _reservations;
        public ObservableCollection<ReservationVM> Reservations
        {
            get
            {
                return _reservations;
            }
            set { _reservations = value; NotifyPropertyChanged(); }
        }
        #endregion Collection
        #region Functions
        void newReservation()
        {
            ReservationVM newReservation = new ReservationVM(ActiveCustomer);
            Reservations.Add(newReservation);
        }

        async void updateReservations()
        {
            ReservationSearchInProgress = true;
            ReservationsFound = false;
            ReservationsNotFound = false;

            string reservationsString = await ApiRequests.Get(ApiUrl.RESERVATIONS_BY_CUSTOMER, ActiveCustomer.Customer.Id);

            List<ReservationDTO> reservationsList = JsonSerializer<ReservationDTO>.DeSerializeAsList(reservationsString);

            if (reservationsList.Count != 0)
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

        async void deleteReservation()
        {
            await ActiveReservation.DeleteReservation();

            updateReservations();
        }

        async void updateCreateReservation()
        {
            await ActiveReservation.UpdateCreateReservation();

            updateReservations();
        }
        #endregion

        #region Commands
        Command _newReservation;
        public Command NewReservation
        {
            get
            {
                return _newReservation ?? (_newReservation = new Command(() => newReservation(), true));
            }
        }

        Command _deleteReservation;
        public Command DeleteReservation
        {
            get
            {
                return _deleteReservation ?? (_deleteReservation = new Command(() => deleteReservation(), true));
            }
        }

        Command _updateCreateReservation;
        public Command UpdateCreateReservation
        {
            get
            {
                return _updateCreateReservation ?? (_updateCreateReservation = new Command(() => updateCreateReservation(), true));
            }
        }
        #endregion

        #endregion

        #region Room

        #region Active

        private RoomVM _activeRoom;
        public RoomVM ActiveRoom
        {
            get { return _activeRoom;}
            set { _activeRoom = value; NotifyPropertyChanged(); }
        }

        #endregion
        #region SearchState

        bool _roomSearchInProgress = false;
        public bool RoomSearchInProgress
        {
            get { return _roomSearchInProgress; }
            set { _roomSearchInProgress = value; NotifyPropertyChanged(); }
        }

        bool _roomsFound = false;
        public bool RoomsFound
        {
            get { return _roomsFound; }
            set { _roomsFound = value; NotifyPropertyChanged(); }
        }

        bool _roomsNotFound = false;
        public bool RoomsNotFound
        {
            get { return _roomsNotFound; }
            set { _roomsNotFound = value; NotifyPropertyChanged(); }
        }

        #endregion
        #region Collections

        ObservableCollection<RoomVM> _rooms;
        public ObservableCollection<RoomVM> Rooms
        {
            get
            {
                return _rooms;
            }
            set { _rooms = value; NotifyPropertyChanged(); }
        }

        ObservableCollection<RoomQualityVM> _roomQualities;
        public ObservableCollection<RoomQualityVM> RoomQualities
        {
            get
            {
                return _roomQualities;
            }
            set { _roomQualities = value; NotifyPropertyChanged(); }
        }

        ObservableCollection<RoomSizeVM> _roomSizes;
        public ObservableCollection<RoomSizeVM> RoomSizes
        {
            get
            {
                return _roomSizes;
            }
            set { _roomSizes = value; NotifyPropertyChanged(); }
        }

        ObservableCollection<RoomBedsVM> _roomBeds;
        public ObservableCollection<RoomBedsVM> RoomBeds
        {
            get
            {
                return _roomBeds;
            }
            set { _roomBeds = value; NotifyPropertyChanged(); }
        }

        #endregion
        #region Functions

        private async void getRoomSearchAttributes()
        {
            string roomBeds = await ApiRequests.Get(ApiUrl.ROOMBEDS);
            string roomQualities = await ApiRequests.Get(ApiUrl.ROOMQUALITY);
            string roomSizes = await ApiRequests.Get(ApiUrl.ROOMSIZE);

            var roomBedsList = JsonSerializer<RoomBeds>.DeSerializeAsList(roomBeds);
            var roomQualitiesList = JsonSerializer<RoomQuality>.DeSerializeAsList(roomQualities);
            var roomSizesList = JsonSerializer<RoomSize>.DeSerializeAsList(roomSizes);

            foreach (RoomBeds r in roomBedsList)
                RoomBeds.Add(new RoomBedsVM(r));
            foreach (RoomQuality r in roomQualitiesList)
                RoomQualities.Add(new RoomQualityVM(r));
            foreach (RoomSize r in roomSizesList)
                RoomSizes.Add(new RoomSizeVM(r));
        }

        #endregion
        #region Commands


        #endregion

        #endregion
    }
}
