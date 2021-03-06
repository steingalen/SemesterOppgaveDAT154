﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Models;
using Models.ViewModels;
using HttpRequest;
using System;

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
            if (ActiveCustomer == null)
                return;

            if(Reservations == null)
                Reservations = new ObservableCollection<ReservationVM>();

            ReservationVM newReservation = new ReservationVM(ActiveCustomer);
            Reservations.Add(newReservation);
            ActiveReservation = newReservation;
        }

        async void updateReservations()
        {
            ReservationSearchInProgress = true;
            ReservationsFound = ReservationsNotFound = false;

            string reservationsString = await ApiRequests.Get(ApiUrl.RESERVATIONS_BY_CUSTOMER, ActiveCustomer.Customer.Id);

            List<ReservationDTO> reservationsList = JsonSerializer<ReservationDTO>.DeSerializeAsList(reservationsString);

            if (reservationsList.Count != 0)
                ReservationsFound = true;
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

        async void checkout()
        {
            if (ActiveReservation.Room == null)
                return;

            RoomTask newCleaningJob = new RoomTask()
            {
                RoomId = ActiveReservation.Room.RoomNumber,
                Status = "New",
                TaskTypeId = 3
            };

            await ApiRequests.Post(ApiUrl.ROOMTASKS, JsonSerializer<RoomTask>.Serialize(newCleaningJob));
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

        Command _checkout;
        public Command Checkout
        {
            get
            {
                return _checkout ?? (_checkout = new Command(() => checkout(), true));
            }
        }
        #endregion

        #endregion

        #region Rooms

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

        bool _attributeSearchInProgress = false;
        public bool AttributeSearchInProgress
        {
            get { return _attributeSearchInProgress; }
            set { _attributeSearchInProgress = value; NotifyPropertyChanged(); }
        }

        bool _attributesFound = false;
        public bool AttributesFound
        {
            get { return _attributesFound; }
            set { _attributesFound = value; NotifyPropertyChanged(); }
        }

        bool _attributesNotFound = false;
        public bool AttributesNotFound
        {
            get { return _attributesNotFound; }
            set { _attributesNotFound = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region SearchData

        private DateTime _activeStartDate = DateTime.Now.AddTicks(0);
        public DateTime ActiveStartDate {
            get { return _activeStartDate; }
            set { _activeStartDate = value; NotifyPropertyChanged(); }
        }

        private DateTime _activeEndDate = DateTime.Now.AddDays(1);
        public DateTime ActiveEndDate
        {
            get { return _activeEndDate; }
            set { _activeEndDate = value; NotifyPropertyChanged(); }
        }

        private RoomSizeVM _activeRoomSize;
        public RoomSizeVM ActiveRoomSize {
            get { return _activeRoomSize; }
            set { _activeRoomSize = value; NotifyPropertyChanged();}
        }

        private RoomBedsVM _activeRoomBeds;
        public RoomBedsVM ActiveRoomBeds
        {
            get { return _activeRoomBeds; }
            set { _activeRoomBeds = value; NotifyPropertyChanged(); }
        }

        private RoomQualityVM _activeRoomQuality;
        public RoomQualityVM ActiveRoomQuality
        {
            get { return _activeRoomQuality; }
            set { _activeRoomQuality = value; NotifyPropertyChanged(); }
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
                if(!AttributesFound)
                    getRoomSearchAttributes();

                return _roomQualities;
            }
            set { _roomQualities = value; NotifyPropertyChanged(); }
        }

        ObservableCollection<RoomSizeVM> _roomSizes;
        public ObservableCollection<RoomSizeVM> RoomSizes
        {
            get
            {
                if (!AttributesFound)
                    getRoomSearchAttributes();

                return _roomSizes;
            }
            set { _roomSizes = value; NotifyPropertyChanged(); }
        }

        ObservableCollection<RoomBedsVM> _roomBeds;
        public ObservableCollection<RoomBedsVM> RoomBeds
        {
            get
            {
                if (!AttributesFound)
                    getRoomSearchAttributes();

                return _roomBeds;
            }
            set { _roomBeds = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region Functions

        private void selectRoom()
        {
            ActiveReservation.Room = ActiveRoom;
            ActiveReservation.Start = ActiveStartDate.AddTicks(0);
            ActiveReservation.Slutt = ActiveEndDate.AddTicks(0);

            // Clear room list since we're exiting
            Rooms = null;
            RoomsFound = false;
        }

        private async void startRoomSearch()
        {
            RoomsFound = RoomsNotFound = false;
            RoomSearchInProgress = true;

            string roomsString = await ApiRequests.Get(ApiUrl.ROOMS,
                ActiveRoomQuality.Id + "/" + ActiveRoomBeds.Id + "/" +
                ActiveRoomSize.Id + "/" + ActiveStartDate.ToString("yyyy-MM-dd") + "/" +
                ActiveEndDate.ToString("yyyy-MM-dd"));
            
            List<Room> roomList = null;

            if(roomsString.Length != 0)
                roomList = JsonSerializer<Room>.DeSerializeAsList(roomsString);
            else
                roomList = new List<Room>();

            if (roomList.Count != 0)
                RoomsFound = true;
            else
                RoomsNotFound = true;

            RoomSearchInProgress = false;

            ObservableCollection<RoomVM> collection = new ObservableCollection<RoomVM>();

            foreach (Room c in roomList)
            {
                RoomVM newVM = new RoomVM(c);
                collection.Add(newVM);
            }

            Rooms = collection;
        }

        private async void getRoomSearchAttributes()
        {
            AttributesFound = AttributesNotFound = false;
            AttributeSearchInProgress = true;

            string roomBeds = await ApiRequests.Get(ApiUrl.ROOMBEDS);
            string roomQualities = await ApiRequests.Get(ApiUrl.ROOMQUALITY);
            string roomSizes = await ApiRequests.Get(ApiUrl.ROOMSIZE);

            var roomBedsList = JsonSerializer<RoomBeds>.DeSerializeAsList(roomBeds);
            var roomQualitiesList = JsonSerializer<RoomQuality>.DeSerializeAsList(roomQualities);
            var roomSizesList = JsonSerializer<RoomSize>.DeSerializeAsList(roomSizes);

            if (roomBedsList.Count == 0 || roomQualitiesList.Count == 0 || roomSizesList.Count == 0)
            {
                AttributesNotFound = true;
                AttributeSearchInProgress = false;
                return;
            }

            AttributesFound = true;
            AttributeSearchInProgress = false;

            ObservableCollection<RoomBedsVM> beds = new ObservableCollection<RoomBedsVM>();
            ObservableCollection<RoomQualityVM> qualities = new ObservableCollection<RoomQualityVM>();
            ObservableCollection<RoomSizeVM> sizes = new ObservableCollection<RoomSizeVM>();

            var anyBed = RoomBedsVM.GetRoomBedsAny();
            var anyQuality = RoomQualityVM.GetRoomQualityAny();
            var anySize = RoomSizeVM.GetRoomSizeAny();

            // Add 'Any' values
            beds.Add(anyBed);
            qualities.Add(anyQuality);
            sizes.Add(anySize);

            foreach (RoomBeds r in roomBedsList)
                beds.Add(new RoomBedsVM(r));
            foreach (RoomQuality r in roomQualitiesList)
                qualities.Add(new RoomQualityVM(r));
            foreach (RoomSize r in roomSizesList)
                sizes.Add(new RoomSizeVM(r));

            RoomBeds = beds;
            RoomQualities = qualities;
            RoomSizes = sizes;

            ActiveRoomBeds = anyBed;
            ActiveRoomQuality = anyQuality;
            ActiveRoomSize = anySize;
        }

        private void toRoomPicker()
        {
            ActiveStartDate = ActiveReservation.Start.AddTicks(0);
            ActiveEndDate = ActiveReservation.Slutt.AddTicks(0);
        }

        #endregion

        #region Commands

        Command _startRoomSearch;
        public Command StartRoomSearch
        {
            get
            {
                return _startRoomSearch ?? (_startRoomSearch = new Command(() => startRoomSearch(), true));
            }
        }

        Command _selectRoom;
        public Command SelectRoom
        {
            get
            {
                return _selectRoom ?? (_selectRoom = new Command(() => selectRoom(), true));
            }
        }

        Command _toRoomPicker;
        public Command ToRoomPicker
        {
            get
            {
                return _toRoomPicker ?? (_toRoomPicker = new Command(() => toRoomPicker(), true));
            }
        }

        #endregion

        #endregion
    }
}
