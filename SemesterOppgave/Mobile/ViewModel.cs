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
    public class ViewModel<T> where T : class {
        public ViewModel(int taskType)
        {
            Items = new ObservableCollection<T>();
            _taskType = taskType;

        }

        public ObservableCollection<T> Items { get; private set; }
        private readonly int _taskType;
        

        public async Task Populate()
        {
            var tasks = JsonSerializer<T>.DeSerializeAsList(await ApiRequests.Get(ApiUrl.ROOM_TASKS_BY_TASK, _taskType));
            Items = new ObservableCollection<T>(tasks);
        }

      
    }
}
