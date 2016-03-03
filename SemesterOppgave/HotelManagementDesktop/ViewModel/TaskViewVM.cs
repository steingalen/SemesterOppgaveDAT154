using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using HttpRequest;
using Models;
using Models.ViewModels;

namespace HotelManagementDesktop.ViewModel
{
    class TaskViewVM : BasePropertyChanged
    {

        #region Active

        private RoomTaskVM _activeRoomTask;
        public RoomTaskVM ActiveRoomTask {
            get { return _activeRoomTask; }
            set { _activeRoomTask = value; NotifyPropertyChanged(); }
        }

        private int _roomNumber;
        public int RoomNumber
        {
            get { return _roomNumber; }
            set { _roomNumber = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region Collections

        private ObservableCollection<RoomTaskVM> _roomTasks;
        public ObservableCollection<RoomTaskVM> RoomTasks {
            get { return _roomTasks; }
            set { _roomTasks = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region EntryStatus

        private bool _fromReservation;
        public bool FromReservation {
            get { return _fromReservation; }
            set { _fromReservation = value; NotifyPropertyChanged(); }
        }

        private bool _notFromReservation;
        public bool NotFromReservation
        {
            get { return _notFromReservation; }
            set { _notFromReservation = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region SearchState

        bool _searchInProgress = false;
        public bool SearchInProgress
        {
            get { return _searchInProgress; }
            set { _searchInProgress = value; NotifyPropertyChanged(); }
        }

        bool _found = false;
        public bool Found
        {
            get { return _found; }
            set { _found = value; NotifyPropertyChanged(); }
        }

        bool _notFound = false;
        public bool NotFound
        {
            get { return _notFound; }
            set { _notFound = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region Functions

        public void CameFromReservationView(int roomNumber)
        {
            RoomNumber = roomNumber;
            NotFromReservation = false;
            FromReservation = true;

            // Update list
            updateRoomTasks();
        }

        public void CameFromMainMenu()
        {
            RoomNumber = 0;
            FromReservation = false;
            NotFromReservation = true;

            // Clear list
            RoomTasks = new ObservableCollection<RoomTaskVM>();
        }

        async void updateRoomTasks()
        {
            if (RoomNumber == 0)
                return;

            Found = NotFound = false;
            SearchInProgress = true;

            string tasksString = await ApiRequests.Get(ApiUrl.ROOM_TASKS_BY_ROOM, RoomNumber);

            List<RoomTask> roomTaskList = JsonSerializer<RoomTask>.DeSerializeAsList(tasksString);

            if (roomTaskList.Count != 0)
                Found = true;
            else
                NotFound = true;

            SearchInProgress = false;

            ObservableCollection<RoomTaskVM> collection = new ObservableCollection<RoomTaskVM>();

            foreach (RoomTask t in roomTaskList)
            {
                RoomTaskVM newTask = new RoomTaskVM(t);
                collection.Add(newTask);
            }

            RoomTasks = collection;
        }

        void addNewTask()
        {
            if (RoomNumber == 0) // No room selected
                return;

            if(RoomTasks == null)
                RoomTasks = new ObservableCollection<RoomTaskVM>();

            RoomTaskVM newRoomTask = new RoomTaskVM(new RoomTask());

            RoomTasks.Add(newRoomTask);
            ActiveRoomTask = newRoomTask;
        }

        #endregion
    }
}
