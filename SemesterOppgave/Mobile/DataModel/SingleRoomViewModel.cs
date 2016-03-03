using System.Windows.Input;
using Mobile.Common;

namespace Mobile.DataModel
{
    public class SingleRoomViewModel {


        public SingleRoomViewModel()
        {
            _canExecute = true;
        }
        #region Private variables
        private ICommand _clickCommand;
        private readonly bool _canExecute;
        #endregion

        #region Public properties
        public RoomTaskViewModel RoomTaskViewModel { get; set; }
        public ICommand ClickCommand {
            get {
                return _clickCommand ?? (_clickCommand = new RelayCommand(UpdateRoomTask, () => _canExecute));
            }
        }
        #endregion

        /// <summary>
        /// UpdateCommand from butt0
        /// </summary>
        public async void UpdateRoomTask() {
            await RoomTaskViewModel.Update();
        }
    }
}
