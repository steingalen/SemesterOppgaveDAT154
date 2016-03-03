using System;
using System.Threading.Tasks;
using HttpRequest;
using Mobile.Common;
using Models;
using Models.ViewModels;

namespace Mobile
{
   
    public class RoomTaskViewModel : BasePropertyChanged
    {
        public RoomTaskViewModel(RoomTask roomTask, string taskType) {
            RoomTask = new RoomTaskVM(roomTask);
            TaskType = taskType;
            
        }


        /// <summary>
        /// Updates the roomtask to the webservice
        /// </summary>
        /// <returns></returns>
        public async Task Update() {
            var json = JsonSerializer<RoomTask>.Serialize(new RoomTask() {Id = RoomTask.RoomTask.Id, Status = RoomTask.Status, Comments = RoomTask.Comments, RoomId = RoomTask.Room.RoomNumber, TaskTypeId = RoomTask.Type.TaskType.Id});
            await ApiRequests.Put(ApiUrl.ROOMTASKS, RoomTask.RoomTask.Id, json);
            
        }   
        

        #region Public Properties
        public RoomTaskVM RoomTask { get; set; }

        public string TaskType { get; set; }
        #endregion

        
    }
}
