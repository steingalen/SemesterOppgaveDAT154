using Models;

namespace Models.ViewModels
{
    public class RoomTaskVM : BasePropertyChanged
    {
        RoomTask _roomTask;

        RoomVM _room;
        public RoomVM Room { get { return _room; } set { _room = value;  NotifyPropertyChanged(); } }

        TaskTypeVM _type;
        public TaskTypeVM Type { get { return _type; } set { _type = value; NotifyPropertyChanged(); } }

        public string Comments { get { return _roomTask.Comments; } set { _roomTask.Comments = value;  NotifyPropertyChanged(); } }

        public string Status { get { return _roomTask.Status; } set { _roomTask.Status = value;  NotifyPropertyChanged(); } }
        public RoomTask RoomTask { get { return _roomTask; } }

        public RoomTaskVM(RoomTask roomTask)
        {
            _roomTask = roomTask;

            _room = new RoomVM(_roomTask.Room);
            _type = new TaskTypeVM(_roomTask.Type);
        }
    }
}
