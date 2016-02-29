using Models;

namespace HotelManagementDesktop.ViewModel
{
    class RoomBedsVM : BasePropertyChanged
    {
        RoomBeds _roomBeds;

        public int Size { get { return _roomBeds.Beds; } set { _roomBeds.Beds = value; NotifyPropertyChanged(); } }

        public RoomBedsVM(RoomBeds roomBeds)
        {
            _roomBeds = roomBeds;
        }
    }
}
