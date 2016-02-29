using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementDesktop.ViewModel
{
    class RoomVM : BasePropertyChanged
    {
        Models.Room _room;

        public int RoomNumber { get { return _room.Id; } set { _room.Id = value;  NotifyPropertyChanged(); } }

        RoomSizeVM _roomSize;
        public RoomSizeVM RoomSize { get { return _roomSize; } set { _roomSize = value;  NotifyPropertyChanged(); } }

        RoomBedsVM _roomBeds;
        public RoomBedsVM RoomBeds { get { return _roomBeds; } set { _roomBeds = value;  NotifyPropertyChanged(); } }

        RoomQualityVM _roomQuality;
        public RoomQualityVM RoomQuality { get { return _roomQuality; } set { _roomQuality = value;  NotifyPropertyChanged(); } }

        public RoomVM(HttpRequest.Models.Room room)
        {
            _room = room;

            _roomSize = new RoomSizeVM(room.Size);
            _roomBeds = new RoomBedsVM(room.Beds);
            _roomQuality = new RoomQualityVM(room.Quality);
        }
    }
}
