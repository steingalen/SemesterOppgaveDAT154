using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HttpRequest;
using Mobile.Annotations;
using Models;

namespace Mobile
{
    public class RoomTaskVM : BasePropertyChanged
    {
        public RoomTaskVM(RoomTask roomTask, string taskType) {
            _roomTask = roomTask;
            _statuses = new List<Status>() {
                new Status("new", RoomTask.Status), new Status("in progress", RoomTask.Status), new Status("finished", RoomTask.Status)
            };
            _taskType = taskType;
        }

        #region Public Properties
        private RoomTask _roomTask;
        private List<Status> _statuses;
        private string _taskType;
        #endregion

        #region Public Properties
        public RoomTask RoomTask {
            get { return _roomTask; }
            set {
                _roomTask = value;
                OnPropertyChanged(nameof(RoomTask));
            }
        }

        public List<Status> Statuses {
            get { return _statuses; }
            set {
                _statuses = value;
                OnPropertyChanged(nameof(Statuses));
            }
        }

        public string TaskType {
            get {
                return _taskType;
            }
            set {
                _taskType = value;
                OnPropertyChanged(nameof(TaskType));
            }
        }
        #endregion


    }

    public class Status : BasePropertyChanged {
        private bool _checked;
        private string _type;
        public bool Checked {
            get { return _checked; }
            set { _checked = value;
                OnPropertyChanged(nameof(Checked));
            }
        }

        public string Type {
            get { return _type; }
            set {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        public Status(string type, string status)
        {
            _type = type;
            _checked = (type == status);
        }       
    }
}
