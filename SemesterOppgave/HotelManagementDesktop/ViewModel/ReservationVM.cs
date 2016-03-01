using System;
using Models;
using HttpRequest;

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
        public async void DeleteReservation()
        {
            if (_reservation.Id != 0)
            {
                var p = await ApiRequests.Delete(ApiUrl.RESERVATIONS, _reservation.Id);
                Console.WriteLine(p);
            }
        }

        public async void UpdateCreateReservation()
        {
            // Find out if already in DB?? Then add/update
            if (_reservation.Id != 0)
                await ApiRequests.Put(ApiUrl.RESERVATIONS, _reservation.Id, JsonSerializer<Reservation>.Serialize(_reservation));
            else
                await ApiRequests.Post(ApiUrl.RESERVATIONS, JsonSerializer<Reservation>.Serialize(_reservation));
        }
        #endregion Functions

        #region Commands
        
        #endregion Commands
        public ReservationVM(Reservation reservation)
        {
            _reservation = reservation;

            _room = new RoomVM(_reservation.Room);
            _customer = new CustomerVM(_reservation.Customer);
        }

        public ReservationVM(CustomerVM customer)
        {
            _reservation = new Reservation();

            Customer = customer;
        }
    }
}
