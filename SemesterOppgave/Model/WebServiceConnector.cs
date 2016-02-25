using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class WebServiceConnector
    {
        Uri BaseAddress;
        
        public ObservableCollection<RoomTask> FindRoomTasks(RoomTaskSearch searchParameters)
        {
            // Temp
            ObservableCollection<RoomTask> list = new ObservableCollection<RoomTask>();

            list.Add(new RoomTask() { Room = new Room(), Id = 0, Status = 0, Type = new TaskType(), Comments = "OK " });


            return list;
        }

        public void CreateUpdateRoomTask(RoomTask roomTask)
        {
            // TODO
        }

        public void DeleteRoomTask(RoomTask roomTask)
        {
            // TODO
        }

        public ObservableCollection<Reservation> FindReservations(string customer)
        {
            // Temp
            ObservableCollection<Reservation> list = new ObservableCollection<Reservation>();

            Reservation r = new Reservation() { Customer = new Customer() { FirstName = "A" }, Id = 0, Room = new Room(), Start = new DateTime(), Slutt = new DateTime() };

            list.Add(r);

            return list;
        }

        public void CreateUpdateReservation(Reservation reservation)
        {
            // TODO
        }

        public void DeleteReservation(Reservation reservation)
        {
            // TODO
        }

        public ObservableCollection<Room> FindAvailableRooms(RoomSearch searchParameters)
        {
            // Temp
            ObservableCollection<Room> list = new ObservableCollection<Room>();

            Room r = new Room() { Beds = new RoomBeds(), Quality = new RoomQuality(), RoomNumber = 0, Size = new RoomSize() };

            list.Add(r);

            return list;
        }

        public ObservableCollection<RoomQuality> GetAllRoomQuality()
        {
            // Temp
            ObservableCollection<RoomQuality> list = new ObservableCollection<RoomQuality>();

            list.Add(new RoomQuality() { Id = 0, Quality = "Excellent" });

            return list;
        }

        public ObservableCollection<RoomSize> GetAllRoomSize()
        {
            // Temp
            ObservableCollection<RoomSize> list = new ObservableCollection<RoomSize>();

            list.Add(new RoomSize() { Id = 0, Size = "Large" });

            return list;
        }

        public ObservableCollection<RoomBeds> GetAllRoomBeds()
        {
            // Temp
            ObservableCollection<RoomBeds> list = new ObservableCollection<RoomBeds>();

            list.Add(new RoomBeds() { Id = 0, Beds = 2 });

            return list;
        }

        public WebServiceConnector()
        {
        }
    }
}
