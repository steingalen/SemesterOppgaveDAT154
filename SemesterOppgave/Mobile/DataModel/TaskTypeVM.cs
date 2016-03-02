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


    }
}
