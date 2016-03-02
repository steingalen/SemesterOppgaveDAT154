using HotelManagementDesktop.ViewModel;
using Models.ViewModels;

namespace HotelManagementDesktop
{
    class DesktopVM : BasePropertyChanged
    {
        public ReservationViewVM ReservationVM { get; set; } = new ReservationViewVM();
    }
}
