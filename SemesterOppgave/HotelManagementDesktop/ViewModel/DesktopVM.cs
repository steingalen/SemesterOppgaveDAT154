using HotelManagementDesktop.ViewModel;
using Models.ViewModels;

namespace HotelManagementDesktop
{
    class DesktopVM : BasePropertyChanged
    {

        #region ViewVMs

        public ReservationViewVM ReservationVM { get; set; } = new ReservationViewVM();

        public TaskViewVM TaskViewVM { get; set; } = new TaskViewVM();

        #endregion

        #region Functions

        void reservationViewToTaskView(int roomNumber)
        {
            TaskViewVM.CameFromReservationView(roomNumber);
        }

        void mainMenuToTaskView()
        {
            TaskViewVM.CameFromMainMenu();
        }

        #endregion

        #region Commands

        private CommandPara1 _reservationViewToTaskView;
        public CommandPara1 ReservationViewToTaskView
        {
            get
            {
                return _reservationViewToTaskView ?? (_reservationViewToTaskView = new CommandPara1((a) => reservationViewToTaskView((int)a), true));
            }
        }

        private Command _mainMenuToTaskView;
        public Command MainMenuToTaskView
        {
            get
            {
                return _mainMenuToTaskView ?? (_mainMenuToTaskView = new Command(() => mainMenuToTaskView(), true));
            }
        }

        #endregion
    }
}
