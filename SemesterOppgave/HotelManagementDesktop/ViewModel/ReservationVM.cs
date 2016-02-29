using System;
using Models;

namespace HotelManagementDesktop.ViewModel
{
    class ReservationVM : BasePropertyChanged
    {
        Reservation _reservation;

        RoomVM _room;
        public RoomVM Room { get { return _room; } set { _room = value; NotifyPropertyChanged(); } }

        CustomerVM _customer;
        public CustomerVM Customer { get { return _customer; } set { _customer = value;  NotifyPropertyChanged(); } }

        public DateTime Start { get { return _reservation.Start; } set { _reservation.Start = value;  NotifyPropertyChanged(); } }

        public DateTime Slutt { get { return _reservation.Slutt; } set { _reservation.Slutt = value; NotifyPropertyChanged(); } }

        #region Functions
        public void DeleteReservation()
        {
            // Send delete to database if applicable
        }

        public void UpdateCreateReservation()
        {
            // Find out if already in DB?? Then add/update
        }
        #endregion Functions

        #region Commands
        
        #endregion Commands
        public ReservationVM(Reservation reservation)
        {
            _reservation = reservation;

            _room = new RoomVM(_reservation.Room);
            //_customer = new CustomerVM(_reservation.Customer);
        }

        public ReservationVM(CustomerVM customer)
        {
            _customer = customer;
        }
    }
}
