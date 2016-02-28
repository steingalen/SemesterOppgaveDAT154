using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HotelManagementDesktop.ViewModel;

namespace HotelManagementDesktop
{
    class DesktopVM : BasePropertyChanged
    {
        /*
        #region Members
        ObservableCollection<Model.RoomTask> _roomTasks;
        public ObservableCollection<Model.RoomTask> RoomTasks { get { return _roomTasks; } private set { _roomTasks = value; NotifyPropertyChanged(); } }

        
        Model.RoomTask _activeRoomTask;
        public Model.RoomTask ActiveRoomTask { get { return _activeRoomTask; } private set { _activeRoomTask = value; NotifyPropertyChanged(); } }

        
        Model.RoomSearch _activeRoomSearch;
        public Model.RoomSearch ActiveRoomSearch { get { return _activeRoomSearch; } private set { _activeRoomSearch = value; NotifyPropertyChanged(); } }

        Model.RoomTaskSearch _activeRoomTaskSearch;
        public Model.RoomTaskSearch ActiveRoomTaskSearch { get { return _activeRoomTaskSearch; } private set { _activeRoomTaskSearch = value; NotifyPropertyChanged(); } }
        #endregion Members
        */
        public ReservationViewVM ReservationVM { get; set; } = new ReservationViewVM();

        public RoomSearchVM roomSearchVM { get; set; } = new RoomSearchVM();
    }
}
