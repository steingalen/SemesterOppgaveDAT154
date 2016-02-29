using Models;

namespace HotelManagementDesktop.ViewModel
{
    class RoomVM : BasePropertyChanged
    {
        Room _room;

        public int RoomNumber { get { return _room.Id; } set { _room.Id = value;  NotifyPropertyChanged(); } }

        RoomSizeVM _roomSize;
        public RoomSizeVM RoomSize { get { return _roomSize; } set { _roomSize = value;  NotifyPropertyChanged(); } }

        RoomBedsVM _roomBeds;
        public RoomBedsVM RoomBeds { get { return _roomBeds; } set { _roomBeds = value;  NotifyPropertyChanged(); } }

        RoomQualityVM _roomQuality;
        public RoomQualityVM RoomQuality { get { return _roomQuality; } set { _roomQuality = value;  NotifyPropertyChanged(); } }

        public RoomVM(Room room)
        {
            _room = room;

            _roomSize = new RoomSizeVM(room.Size);
            _roomBeds = new RoomBedsVM(room.Beds);
            _roomQuality = new RoomQualityVM(room.Quality);
        }
    }
}
