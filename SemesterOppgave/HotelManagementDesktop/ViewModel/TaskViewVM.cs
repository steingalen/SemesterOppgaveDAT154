using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        private ObservableCollection<TaskTypeVM> _taskTypes;
        public ObservableCollection<TaskTypeVM> TaskTypes
        {
            get
            {
                return _taskTypes; }
            set { _taskTypes = value; NotifyPropertyChanged(); }
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

        bool _taskTypesSearchInProgress = false;
        public bool TaskTypesSearchInProgress
        {
            get { return _taskTypesSearchInProgress; }
            set { _taskTypesSearchInProgress = value; NotifyPropertyChanged(); }
        }

        bool _taskTypesFound = false;
        public bool TaskTypesFound
        {
            get { return _taskTypesFound; }
            set { _taskTypesFound = value; NotifyPropertyChanged(); }
        }

        bool _taskTypesNotFound = false;
        public bool TaskTypesNotFound
        {
            get { return _taskTypesNotFound; }
            set { _taskTypesNotFound = value; NotifyPropertyChanged(); }
        }

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

            if(TaskTypes == null)
                getRoomTaskTypes();
        }

        public void CameFromMainMenu()
        {
            RoomNumber = 0;
            FromReservation = false;
            NotFromReservation = true;

            // Clear list
            RoomTasks = new ObservableCollection<RoomTaskVM>();

            if (TaskTypes == null)
                getRoomTaskTypes();
        }

        async void getRoomTaskTypes()
        {
            TaskTypesSearchInProgress = true;
            TaskTypesFound = TaskTypesNotFound = false;

            string json = await ApiRequests.Get(ApiUrl.TASKTYPES);

            List<TaskType> taskTypesList = JsonSerializer<TaskType>.DeSerializeAsList(json);

            ObservableCollection<TaskTypeVM> collection = new ObservableCollection<TaskTypeVM>();

            foreach(TaskType t in taskTypesList)
                collection.Add(new TaskTypeVM(t));

            TaskTypes = collection;

            TaskTypesFound = true;
            TaskTypesSearchInProgress = false;
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
                newTask.Type = TaskTypes.Where((TaskTypeVM a) => { return a.TaskType.Id == t.TaskTypeId; }).First();
                collection.Add(newTask);
            }

            RoomTasks = collection;
        }

        async void updateCreateActiveRoomTask()
        {
            // Clear out other objects, just keep Id
            ActiveRoomTask.RoomTask.Type = null;

            if (ActiveRoomTask.RoomTask.Id != 0)// Existing reservation, update
            {
                await ApiRequests.Put(ApiUrl.ROOMTASKS, ActiveRoomTask.RoomTask.Id,
                        JsonSerializer<RoomTask>.Serialize(ActiveRoomTask.RoomTask));
            }
            else
            {
                await ApiRequests.Post(ApiUrl.ROOMTASKS, JsonSerializer<RoomTask>.Serialize(ActiveRoomTask.RoomTask));
            }

            updateRoomTasks();
        }

        async void deleteActiveRoomTask()
        {
            if (ActiveRoomTask.RoomTask.Id != 0)
                await ApiRequests.Delete(ApiUrl.ROOMTASKS, ActiveRoomTask.RoomTask.Id);

            updateRoomTasks();
        }

        void addNewTask()
        {
            if (RoomNumber == 0) // No room selected
                return;

            if(RoomTasks == null)
                RoomTasks = new ObservableCollection<RoomTaskVM>();

            RoomTask newTask = new RoomTask() {RoomId = RoomNumber};

            newTask.Type = TaskTypes.First().TaskType;

            RoomTaskVM newRoomTask = new RoomTaskVM(newTask);

            newRoomTask.Type = TaskTypes.First();
            newRoomTask.TaskNew = true;

            RoomTasks.Add(newRoomTask);
            ActiveRoomTask = newRoomTask;
        }

        #endregion

        #region Commands

        Command _newRoomTask;
        public Command NewRoomTask
        {
            get
            {
                return _newRoomTask ?? (_newRoomTask = new Command(() => addNewTask(), true));
            }
        }

        Command _deleteRoomTask;
        public Command DeleteRoomTask
        {
            get
            {
                return _deleteRoomTask ?? (_deleteRoomTask = new Command(() => deleteActiveRoomTask(), true));
            }
        }

        Command _startSearch;
        public Command StartSearch
        {
            get
            {
                return _startSearch ?? (_startSearch = new Command(() => updateRoomTasks(), true));
            }
        }

        Command _updateCreateRoomTask;
        public Command UpdateCreateRoomTask
        {
            get
            {
                return _updateCreateRoomTask ?? (_updateCreateRoomTask = new Command(() => updateCreateActiveRoomTask(), true));
            }
        }

        #endregion
    }
}
