using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HttpRequest;
using Models;

namespace Mobile
{
    public class RoomTaskVM
    {
        public RoomTaskVM(int taskType)
        {
            Items = new ObservableCollection<RoomTask>();
            _taskType = taskType;
        }

        public ObservableCollection<RoomTask> Items { get; private set; }
        private int _taskType;

        public async Task Populate()
        {
            var tasks = JsonSerializer<RoomTask>.DeSerializeAsList(await ApiRequests.Get(ApiUrl.ROOM_TASKS_BY_TASK, _taskType));
            Items = new ObservableCollection<RoomTask>(tasks);
        }

    }
}
