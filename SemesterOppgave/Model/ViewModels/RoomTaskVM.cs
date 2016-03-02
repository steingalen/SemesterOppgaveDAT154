using Models;

namespace Models.ViewModels
{
    public class RoomTaskVM : BasePropertyChanged
    {
        RoomTask _roomTask;

        RoomVM _room;
        public RoomVM Room { get { return _room; } set { _room = value;  NotifyPropertyChanged(); } }

        TaskTypeVM _type;
        public TaskTypeVM Type { get { return _type; } set { _type = value; NotifyPropertyChanged(); } }

        public string Comments { get { return _roomTask.Comments; } set { _roomTask.Comments = value;  NotifyPropertyChanged(); } }

        public string Status { get { return _roomTask.Status; } set { _roomTask.Status = value;  NotifyPropertyChanged(); } }

        #region StatusBools

        private bool _taskNew;
        public bool TaskNew
        {
            get { return _taskNew; }
            set
            {
                _taskInProgress = _taskFinished = false;
                _taskNew = true;
                _roomTask.Status = "New";
                NotifyPropertyChanged();
                NotifyPropertyChanged("TaskInProgress");
                NotifyPropertyChanged("TaskFinished");
            }
        }

        private bool _taskInProgress;
        public bool TaskInProgress
        {
            get { return _taskInProgress; }
            set
            {
                _taskNew = _taskFinished = false;
                _taskInProgress = true;
                _roomTask.Status = "In Progress";
                NotifyPropertyChanged();
                NotifyPropertyChanged("TaskNew");
                NotifyPropertyChanged("TaskFinished");
            }
        }

        private bool _taskFinished;
        public bool TaskFinished
        {
            get { return _taskNew; }
            set
            {
                _taskNew = _taskInProgress = false;
                _taskFinished = true;
                _roomTask.Status = "Finished";
                NotifyPropertyChanged();
                NotifyPropertyChanged("TaskNew");
                NotifyPropertyChanged("TaskInProgress");
            }
        }

        #endregion


        public RoomTaskVM(RoomTask roomTask)
        {
            _roomTask = roomTask;

            _room = new RoomVM(_roomTask.Room);
            _type = new TaskTypeVM(_roomTask.Type);
        }
    }
}
