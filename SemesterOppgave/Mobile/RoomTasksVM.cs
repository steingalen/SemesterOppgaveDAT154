using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpRequest;
using Models;

namespace Mobile
{
    public class RoomTasksVM : BasePropertyChanged
    {
        public RoomTasksVM(int taskType)
        {
            Items = new ObservableCollection<RoomTaskVM>();
            _taskType = taskType;

        }

        public ObservableCollection<RoomTaskVM> Items { get; set; }
        private readonly int _taskType;
        private RoomTask _activeRoomTask;
        public RoomTask ActiveRoomTask {
            get { return _activeRoomTask; }
            set {
                _activeRoomTask = value;
                OnPropertyChanged(nameof(ActiveRoomTask));
            }
        }

        public async Task Populate()
        {
            var taskTypes = JsonSerializer<TaskType>.DeSerializeAsList(await ApiRequests.Get(ApiUrl.TASKTYPES));
            var tasks = JsonSerializer<RoomTask>.DeSerializeAsList(await ApiRequests.Get(ApiUrl.ROOM_TASKS_BY_TASK, _taskType));
            var type = "";

            foreach (var taskType in taskTypes) {
                if (taskType.Id == _taskType)
                    type = taskType.Type;
            }

            foreach (var roomTask in tasks) {
                Items.Add(new RoomTaskVM(roomTask, type));
            }
        }
    }
}
