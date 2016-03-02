using Models;

namespace Models.ViewModels
{
    public class RoomVM : BasePropertyChanged
    {
        public Room Room { get; private set; }

        public int RoomNumber { get { return Room.Id; } set { Room.Id = value;  NotifyPropertyChanged(); } }

        RoomSizeVM _roomSize;
        public RoomSizeVM RoomSize { get { return _roomSize; } set { _roomSize = value;  NotifyPropertyChanged(); } }

        RoomBedsVM _roomBeds;
        public RoomBedsVM RoomBeds { get { return _roomBeds; } set { _roomBeds = value;  NotifyPropertyChanged(); } }

        RoomQualityVM _roomQuality;
        public RoomQualityVM RoomQuality { get { return _roomQuality; } set { _roomQuality = value;  NotifyPropertyChanged(); } }

        public RoomVM(Room room)
        {
            Room = room;

            _roomSize = new RoomSizeVM(room.Size);
            _roomBeds = new RoomBedsVM(room.Beds);
            _roomQuality = new RoomQualityVM(room.Quality);
        }
    }
}
