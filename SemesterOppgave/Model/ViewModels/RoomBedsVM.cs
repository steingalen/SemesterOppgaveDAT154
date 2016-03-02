using Models;

namespace Models.ViewModels
{
    class RoomBedsVM : BasePropertyChanged
    {
        RoomBeds _roomBeds;

        internal int Id { get { return _roomBeds.Id; } }

        public int Beds { get { return _roomBeds.Beds; } set { _roomBeds.Beds = value; NotifyPropertyChanged(); } }

        public static RoomBedsVM GetRoomBedsAny()
        {
            RoomBeds any = new RoomBeds() { Beds = 0 };

            RoomBedsVM anyVM = new RoomBedsVM(any);

            return anyVM;
        }

        public RoomBedsVM(RoomBeds roomBeds)
        {
            _roomBeds = roomBeds;
        }
    }
}
