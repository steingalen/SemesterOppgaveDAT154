using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementDesktop
{
    class DesktopVM
    {
        public ObservableCollection<Model.RoomSize> roomSizes { get; }

        public ObservableCollection<Model.RoomQuality> roomQuality { get; }

        public ObservableCollection<Model.RoomBeds> roomBeds { get; }

        public ObservableCollection<Model.Room> availableRooms { get; }

        public ObservableCollection<Model.Reservation> customerReservations { get;}

        public ObservableCollection<Model.RoomTask> roomTasks { get; }

        public Model.Reservation activeReservation { get; }

        public Model.RoomTask activeRoomTask { get; }

        public Model.RoomSearch activeRoomSearch { get; }

        public Model.RoomTaskSearch activeRoomTaskSearch { get; }
    }
}
