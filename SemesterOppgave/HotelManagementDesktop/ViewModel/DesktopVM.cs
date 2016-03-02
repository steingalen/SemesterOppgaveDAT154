using HotelManagementDesktop.ViewModel;
using Models;

namespace HotelManagementDesktop
{
    class DesktopVM : BasePropertyChanged
    {
        public ReservationViewVM ReservationVM { get; set; } = new ReservationViewVM();
    }
}
