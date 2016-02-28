using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementDesktop.ViewModel
{
    class RoomQualityVM : BasePropertyChanged
    {
        Model.RoomQuality _roomQuality;

        public string Quality { get { return _roomQuality.Quality; } set { _roomQuality.Quality = value; NotifyPropertyChanged(); } }

        public RoomQualityVM(Model.RoomQuality roomQuality)
        {
            _roomQuality = roomQuality;
        }
    }
}
