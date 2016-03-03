using System;
using Models;

namespace Models.ViewModels
{
    public class RoomTaskVM : BasePropertyChanged
    {
        RoomTask _roomTask;

        RoomVM _room;
        public RoomVM Room { get { return _room; }
            set
            {
                _room = value;

                if (value == null)
                {
                    _roomTask.Room = null;
                    _roomTask.RoomId = 0;
                }
                else
                {
                    _roomTask.Room = value.Room;
                    _roomTask.RoomId = value.Room.Id;
                }
                NotifyPropertyChanged();
            } }

        TaskTypeVM _type;
        public TaskTypeVM Type { get { return _type; }
            set
            {
                _type = value;

                if (value == null)
                    _roomTask.Type = null;
                else
                {
                    _roomTask.Type = value.TaskType;
                    _roomTask.TaskTypeId = value.TaskType.Id;
                }

                NotifyPropertyChanged();
            } }

        public string Comments { get { return _roomTask.Comments; } set { _roomTask.Comments = value;  NotifyPropertyChanged(); } }

        public string Status { get { return _roomTask.Status; } set { _roomTask.Status = value;  NotifyPropertyChanged(); } }
        public RoomTask RoomTask { get { return _roomTask; } }

        #region Task Status helpers
        public bool TaskNew
        {
            get { return Status.Contains("new"); }
            set
            {
                _roomTask.Status = "new";
                NotifyPropertyChanged();
                NotifyPropertyChanged("TaskFinished");
                NotifyPropertyChanged("TaskInProgress");
                NotifyPropertyChanged("Status");
            }
        }

        public bool TaskInProgress
        {
            get { return Status.Contains("in progress"); }
            set
            {
                _roomTask.Status = "in progress";
                NotifyPropertyChanged();
                NotifyPropertyChanged("TaskNew");
                NotifyPropertyChanged("TaskFinished");
                NotifyPropertyChanged("Status");
            }
        }
        
        public bool TaskFinished
        {
            get { return Status.Contains("finished"); }
            set
            {
                _roomTask.Status = "finished";
                NotifyPropertyChanged();
                NotifyPropertyChanged("TaskInProgress");
                NotifyPropertyChanged("TaskNew");
                NotifyPropertyChanged("Status");
            }
        }

        #endregion

        public RoomTaskVM(RoomTask roomTask)
        {
            _roomTask = roomTask;

            if(_roomTask.Room != null)
                _room = new RoomVM(_roomTask.Room);

            if(_roomTask.Type != null)
                _type = new TaskTypeVM(_roomTask.Type);
        }
    }
}
