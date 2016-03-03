using System;
using System.Threading.Tasks;
using HttpRequest;
using Mobile.Common;
using Models;
using Models.ViewModels;

namespace Mobile
{
   
    public class RoomTaskViewModel : BasePropertyChanged
    {
        public RoomTaskViewModel(RoomTask roomTask, string taskType) {
            RoomTask = new RoomTaskVM(roomTask);
            TaskType = taskType;

            _taskNew = RoomTask.Status == "New";
            _taskInProgress = RoomTask.Status == "In progress";
            _taskFinished = RoomTask.Status == "Finished";
        }


        /// <summary>
        /// Updates the roomtask to the webservice
        /// </summary>
        /// <returns></returns>
        public async Task Update() {
            var json = JsonSerializer<RoomTask>.Serialize(new RoomTask() {Id = RoomTask.RoomTask.Id, Status = RoomTask.Status, Comments = RoomTask.Comments, RoomId = RoomTask.Room.RoomNumber, TaskTypeId = RoomTask.Type.TaskType.Id});
            await ApiRequests.Put(ApiUrl.ROOMTASKS, RoomTask.RoomTask.Id, json);
            
        }   

        #region Private variables
        private bool _taskNew;
        private bool _taskInProgress;
        private bool _taskFinished;
        #endregion

        #region Public Properties
        public RoomTaskVM RoomTask { get; set; }

        public string TaskType { get; set; }
        #endregion


        public bool TaskNew {
            get { return _taskNew; }
            set {
                _taskInProgress = _taskFinished = false;
                _taskNew = true;
                RoomTask.Status = "New";
                NotifyPropertyChanged(nameof(TaskNew));
                NotifyPropertyChanged(nameof(TaskFinished));
                NotifyPropertyChanged(nameof(TaskInProgress));
            }
        }

        public bool TaskInProgress {
            get { return _taskInProgress; }
            set {
                _taskNew = _taskFinished = false;
                _taskInProgress = true;
                RoomTask.Status = "In progress";
                NotifyPropertyChanged(nameof(TaskInProgress));
                NotifyPropertyChanged(nameof(TaskNew));
                NotifyPropertyChanged(nameof(TaskFinished));
            }
        }

        public bool TaskFinished {
            get { return _taskFinished; }
            set {
                _taskNew = _taskInProgress = false;
                _taskFinished = true;
                RoomTask.Status = "Finished";
                NotifyPropertyChanged(nameof(TaskFinished));
                NotifyPropertyChanged(nameof(TaskNew));
                NotifyPropertyChanged(nameof(TaskInProgress));
            }
        }
    }
}
