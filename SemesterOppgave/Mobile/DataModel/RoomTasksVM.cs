using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HttpRequest;
using Models;
using Models.ViewModels;

namespace Mobile
{
    public class RoomTasksVM : BasePropertyChanged
    {
        public RoomTasksVM(int taskType)
        {
            Items = new ObservableCollection<RoomTaskViewModel>();
            _taskType = taskType;

        }

        private ObservableCollection<RoomTaskViewModel> _items;

        public ObservableCollection<RoomTaskViewModel> Items {
            get { return _items; }
            set { _items = value; NotifyPropertyChanged(nameof(Items)); }
        }
        private readonly int _taskType;

        public async Task Populate() {
            var request = await ApiRequests.Get(ApiUrl.TASKTYPES);
            var taskTypes = JsonSerializer<TaskType>.DeSerializeAsList(request);
            var jsonFromWebService = await ApiRequests.Get(ApiUrl.ROOM_TASKS_BY_TASK, _taskType);
            var tasks = JsonSerializer<RoomTask>.DeSerializeAsList(jsonFromWebService);
            var type = "";

            foreach (var taskType in taskTypes) {
                if (taskType.Id == _taskType)
                    type = taskType.Type;
            }

            foreach (var roomTask in tasks) {
                Items.Add(new RoomTaskViewModel(roomTask, type));
            }
        }
    }
}
