﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementDesktop.ViewModel
{
    class RoomBedsVM : BasePropertyChanged
    {
        HttpRequest.Models.RoomBeds _roomBeds;

        public int Size { get { return _roomBeds.Beds; } set { _roomBeds.Beds = value; NotifyPropertyChanged(); } }

        public RoomBedsVM(HttpRequest.Models.RoomBeds roomBeds)
        {
            _roomBeds = roomBeds;
        }
    }
}
