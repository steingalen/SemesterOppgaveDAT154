using Models;

namespace HotelManagementDesktop.ViewModel
{
    class RoomSizeVM : BasePropertyChanged
    {
        RoomSize _roomSize;

        public string Size { get { return _roomSize.Size; } set { _roomSize.Size = value; NotifyPropertyChanged(); } }

        public RoomSizeVM(RoomSize roomSize)
        {
            _roomSize = roomSize;
        }
    }
}
