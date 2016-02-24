using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class WebServiceConnector
    {
        Uri BaseAddress;

        public List<RoomTask> FindRoomTasks(int roomId)
        {
            return new List<RoomTask>();
        }

        public void CreateUpdateRoomTask(RoomTask roomTask)
        {

        }

        public void DeleteRoomTask(RoomTask roomTask)
        {

        }

        public List<Reservation> FindReservations(string customer)
        {
            return new List<Reservation>();
        }

        public void CreateUpdateReservation(Reservation reservation)
        {

        }

        public void DeleteReservation(Reservation reservation)
        {

        }

        public List<Room> FindAvailableRooms(RoomSize size, RoomBeds beds, RoomQuality quality, DateTime start, DateTime end)
        {
            return new List<Room>();
        }

        public WebServiceConnector()
        {
        }
    }
}
