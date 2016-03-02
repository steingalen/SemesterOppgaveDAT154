using System;
using Models;
using HttpRequest;
using System.Threading.Tasks;

namespace HotelManagementDesktop.ViewModel
{
    class ReservationVM : BasePropertyChanged
    {
        Reservation _reservation;

        RoomVM _room;
        public RoomVM Room { get { return _room; }
            set {
                _room = value;
                _reservation.Room = value.Room;
                _reservation.RoomId = value.Room.Id;
                NotifyPropertyChanged(); }
        }

        CustomerVM _customer;
        public CustomerVM Customer { get { return _customer; }
            set
            {
                _customer = value;
                _reservation.Customer = value.Customer;
                _reservation.CustomerId = value.Customer.Id;
                NotifyPropertyChanged();
            } }

        public DateTime Start { get { return _reservation.Start; } set { _reservation.Start = value;  NotifyPropertyChanged(); } }

        public DateTime Slutt { get { return _reservation.Slutt; } set { _reservation.Slutt = value; NotifyPropertyChanged(); } }

        #region Functions

        public async Task<bool> DeleteReservation()
        {
            if (_reservation.Id != 0) // Valid Id, which means it exists in database
                await ApiRequests.Delete(ApiUrl.RESERVATIONS, _reservation.Id);

            return true;
        }

        public async Task<bool> UpdateCreateReservation()
        {
            // Maybe check validity, i.e. if Room is set
            await updateCreateReservation();

            return true;
        }

        private async Task<bool> updateCreateReservation()
        {
            if (_reservation.Id != 0) // Existing reservation, update
            {
                await
                    ApiRequests.Put(ApiUrl.RESERVATIONS, _reservation.Id,
                        JsonSerializer<Reservation>.Serialize(_reservation));
            }
            else // Create new reservation, receive and use as base object, then update with room/customer
            {
                MakeReservation create = new MakeReservation() { Email = Customer.Email, Beds = 0, Start = Start, End = Slutt, Quality = "Any", Size = "Any" };
                string json = JsonSerializer<MakeReservation>.Serialize(create);
                string newResString = await ApiRequests.Post(ApiUrl.MAKE_RESERVATION, JsonSerializer<MakeReservation>.Serialize(create));

                if (newResString.Length == 0) // No reservation available??
                    return true;

                ReservationDTO newReservationDTO = JsonSerializer<ReservationDTO>.DeSerialize(newResString);
                Reservation newReservation = new Reservation();
                newReservation.FromReservationDTO(newReservationDTO);

                _reservation = newReservation;

                if (Room != null)
                {
                    _reservation.Room = Room.Room;

                    await ApiRequests.Put(ApiUrl.RESERVATIONS, _reservation.Id,
                        JsonSerializer<Reservation>.Serialize(_reservation));
                }
                else
                    _reservation.Room = null; // Reservation could return room
            }

            return true;
        }

        #endregion

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
