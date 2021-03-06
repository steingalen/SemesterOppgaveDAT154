﻿using Models;

namespace Models.ViewModels
{
    public class TaskTypeVM : BasePropertyChanged
    {
        TaskType _taskType;

        public string Type { get { return _taskType.Type; } set { _taskType.Type = value; NotifyPropertyChanged(); } }
        public TaskType TaskType { get { return _taskType;} }

        public TaskTypeVM(TaskType taskType)
        {
            _taskType = taskType;
        }
    }
}
