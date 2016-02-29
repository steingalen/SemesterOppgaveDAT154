using Models;

namespace HotelManagementDesktop.ViewModel
{
    class TaskTypeVM : BasePropertyChanged
    {
        TaskType _taskType;

        public string Type { get { return _taskType.Type; } set { _taskType.Type = value; NotifyPropertyChanged(); } }

        public TaskTypeVM(TaskType taskType)
        {
            _taskType = taskType;
        }
    }
}
