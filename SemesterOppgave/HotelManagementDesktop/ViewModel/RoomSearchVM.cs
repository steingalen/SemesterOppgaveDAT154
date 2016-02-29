using System.Collections.ObjectModel;

namespace HotelManagementDesktop.ViewModel
{
    class RoomSearchVM : BasePropertyChanged
    {
        ObservableCollection<RoomSizeVM> _roomSizes;
        public ObservableCollection<RoomSizeVM> RoomSizes { get { return _roomSizes; } private set { _roomSizes = value; NotifyPropertyChanged(); } }

        ObservableCollection<RoomQualityVM> _roomQuality;
        public ObservableCollection<RoomQualityVM> RoomQuality { get { return _roomQuality; } private set { _roomQuality = value; NotifyPropertyChanged(); } }

        ObservableCollection<RoomBedsVM> _roomBeds;
        public ObservableCollection<RoomBedsVM> RoomBeds { get { return _roomBeds; } private set { _roomBeds = value; NotifyPropertyChanged(); } }

        ObservableCollection<RoomVM> _foundRooms;
        public ObservableCollection<RoomVM> FoundRooms { get { return _foundRooms; } private set { _foundRooms = value; NotifyPropertyChanged(); } }

        ReservationVM _activeReservation;
        public ReservationVM ActiveReservation { get { return _activeReservation; } private set { _activeReservation = value;  NotifyPropertyChanged(); } }

        #region Functions
        void getRoomSizes()
        {

        }

        void getRoomQuality()
        {

        }

        void getRoomBeds()
        {

        }

        void selectRoom(RoomVM selectedRoom)
        {
            ActiveReservation.Room = selectedRoom;

            ActiveReservation = null;
            FoundRooms = null;
        }

        void openRoomSearch(ReservationVM activeReservation)
        {
            _activeReservation = activeReservation;

            if (RoomSizes == null)
                getRoomSizes();
            if (RoomQuality == null)
                getRoomQuality();
            if (RoomBeds == null)
                getRoomBeds();
        }

        void roomSearch()
        {
            // Contact db etc
        }
        #endregion Functions

        #region Commands
        Command _roomSearch;
        public Command RoomSearch
        {
            get
            {
                return _roomSearch ?? (_roomSearch = new Command(() => roomSearch(), true));
            }
        }

        CommandPara1 _openRoomSearch;
        public CommandPara1 OpenRoomSearch
        {
            get
            {
                return _openRoomSearch ?? (_openRoomSearch = new CommandPara1((object a) => openRoomSearch((ReservationVM)a), true));
            }
        }

        CommandPara1 _selectRoom;
        public CommandPara1 SelectRoom
        {
            get
            {
                return _selectRoom ?? (_selectRoom = new CommandPara1((object a) => selectRoom((RoomVM)a), true));
            }
        }
        #endregion Commands

        public RoomSearchVM()
        {
        }
    }
}
