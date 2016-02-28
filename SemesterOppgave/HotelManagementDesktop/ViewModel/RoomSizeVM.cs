using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementDesktop.ViewModel
{
    class RoomSizeVM : BasePropertyChanged
    {
        Model.RoomSize _roomSize;

        public string Size { get { return _roomSize.Size; } set { _roomSize.Size = value; NotifyPropertyChanged(); } }

        public RoomSizeVM(Model.RoomSize roomSize)
        {
            _roomSize = roomSize;
        }
    }
}
