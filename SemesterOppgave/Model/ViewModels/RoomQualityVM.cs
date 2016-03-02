using Models;

namespace Models.ViewModels
{
    class RoomQualityVM : BasePropertyChanged
    {
        RoomQuality _roomQuality;

        internal int Id { get { return _roomQuality.Id; } }

        public string Quality { get { return _roomQuality.Quality; } set { _roomQuality.Quality = value; NotifyPropertyChanged(); } }

        public static RoomQualityVM GetRoomQualityAny()
        {
            RoomQuality any = new RoomQuality() {Quality = "Any"};

            RoomQualityVM anyVM = new RoomQualityVM(any);

            return anyVM;
        }

        internal RoomQualityVM(RoomQuality roomQuality)
        {
            _roomQuality = roomQuality;
        }
    }
}
