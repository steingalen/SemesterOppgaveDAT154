using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementDesktop.ViewModel
{
    class TaskTypeVM : BasePropertyChanged
    {
        HttpRequest.Models.TaskType _taskType;

        public string Type { get { return _taskType.Type; } set { _taskType.Type = value; NotifyPropertyChanged(); } }

        public TaskTypeVM(HttpRequest.Models.TaskType taskType)
        {
            _taskType = taskType;
        }
    }
}
