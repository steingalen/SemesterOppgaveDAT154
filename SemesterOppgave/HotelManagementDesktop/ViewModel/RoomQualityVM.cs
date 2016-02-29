using Models;

namespace HotelManagementDesktop.ViewModel
{
    class RoomQualityVM : BasePropertyChanged
    {
        RoomQuality _roomQuality;

        public string Quality { get { return _roomQuality.Quality; } set { _roomQuality.Quality = value; NotifyPropertyChanged(); } }

        public RoomQualityVM(RoomQuality roomQuality)
        {
            _roomQuality = roomQuality;
        }
    }
}
