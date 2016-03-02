using Models;

namespace Models.ViewModels
{
    public class RoomSizeVM : BasePropertyChanged
    {
        RoomSize _roomSize;

        public int Id { get { return _roomSize.Id; } }

        public string Size { get { return _roomSize.Size; } set { _roomSize.Size = value; NotifyPropertyChanged(); } }

        public static RoomSizeVM GetRoomSizeAny()
        {
            RoomSize any = new RoomSize() { Size = "Any" };

            RoomSizeVM anyVM = new RoomSizeVM(any);

            return anyVM;
        }

        internal RoomSizeVM(RoomSize roomSize)
        {
            _roomSize = roomSize;
        }
    }
}
