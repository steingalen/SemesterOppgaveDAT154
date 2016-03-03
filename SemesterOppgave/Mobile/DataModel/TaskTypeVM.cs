using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HttpRequest;
using Models;

namespace Mobile.DataModel
{
    class TaskTypeVM
    {
        public TaskTypeVM() {
            Items = new ObservableCollection<TaskType>();
        }

        public ObservableCollection<TaskType> Items  { get; private set; }

        public async Task Populate() {
            var tasks = JsonSerializer<TaskType>.DeSerializeAsList(await ApiRequests.Get(ApiUrl.TASKTYPES));
            Items = new ObservableCollection<TaskType>(tasks);
        }

        public int GetTaskTypeIdBasedOnTaskType(string taskType) {
            var id = -1;

            foreach (var type in Items) {
                if (type.Type == taskType)
                    id = type.Id;
            }

            return id;
        }

        /// <summary>
        /// Fetches roomtasks from webservice based on the task type
        /// </summary>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public async Task<List<RoomTask>> GetRoomTasksBasedOnTaskType(string taskType) {

            var taskTypeId = GetTaskTypeIdBasedOnTaskType(taskType);

            if (taskTypeId == -1)
                return new List<RoomTask>();

            var roomTasks = JsonSerializer<RoomTask>.DeSerializeAsList(await ApiRequests.Get(ApiUrl.ROOM_TASKS_BY_TASK, taskTypeId));

            return roomTasks ?? new List<RoomTask>();
        }


    }
}
