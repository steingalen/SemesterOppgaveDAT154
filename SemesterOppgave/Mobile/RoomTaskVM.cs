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
    class RoomTaskVM
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
