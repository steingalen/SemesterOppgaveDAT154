using Models;

namespace Models.ViewModels
{
    public class RoomQualityVM : BasePropertyChanged
    {
        RoomQuality _roomQuality;

        public int Id { get { return _roomQuality.Id; } }

        public string Quality { get { return _roomQuality.Quality; } set { _roomQuality.Quality = value; NotifyPropertyChanged(); } }

        public static RoomQualityVM GetRoomQualityAny()
        {
            RoomQuality any = new RoomQuality() {Quality = "Any"};

            RoomQualityVM anyVM = new RoomQualityVM(any);

            return anyVM;
        }

        public RoomQualityVM(RoomQuality roomQuality)
        {
            _roomQuality = roomQuality;
        }
    }
}
