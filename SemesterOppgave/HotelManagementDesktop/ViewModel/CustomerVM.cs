using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementDesktop.ViewModel
{
    class CustomerVM : BasePropertyChanged
    {
        HttpRequest.Models.Customer _customer;

        public string Email { get { return _customer.Email; } set { _customer.Email = value;  NotifyPropertyChanged(); } }

        public string FirstName { get { return _customer.FirstName; } set { _customer.FirstName = value;  NotifyPropertyChanged(); } }

        public string LastName { get { return _customer.LastName; } set { _customer.LastName = value;  NotifyPropertyChanged(); } }

        ObservableCollection<ReservationVM> _reservations;
        public ObservableCollection<ReservationVM> Reservations
        {
            get
            { // Get reservations from db
                return _reservations;
            }
            set { _reservations = value; NotifyPropertyChanged(); }
        }

        #region Functions
        void newReservation()
        {
            ReservationVM newReservation = new ReservationVM(this);
            Reservations.Add(newReservation);
        }

        public async void UpdateReservations()
        {
            string reservations = await HttpRequest.ApiRequests.Get(HttpRequest.ApiUrl.RESERVATIONS, FirstName + "/" + LastName);

            if (reservations.Length < 3)
                return;

            Console.WriteLine(reservations);

            
            // FIXXX
            //List<HttpRequest.Models.Reservation> co = HttpRequest.JsonSerializer<HttpRequest.Models.Reservation>.DeSerializeAsList(reservations);
            //var a = co.ToList<HttpRequest.Models.Reservation>();

            //ObservableCollection <ReservationVM> collection = new ObservableCollection<ReservationVM>();
            //collection.Add(new ReservationVM(co));
            //foreach (HttpRequest.Models.Reservation c in a)
            //    collection.Add(new ReservationVM(c));

            //Reservations = collection;
        }

        void deleteReservation(ReservationVM a)
        {
            a.DeleteReservation();
            Reservations.Remove(a);
            // Rest taken care of in VM itself

            UpdateReservations();
        }

        void updateCreateReservation(ReservationVM a)
        {
            a.UpdateCreateReservation();

            UpdateReservations();
        }
        #endregion Functions

        #region Commands
        Command _newReservation;
        public Command NewReservation
        {
            get
            {
                return _newReservation ?? (_newReservation = new Command(() => newReservation(), true));
            }
        }

        //Command _updateReservations;
        //public Command UpdateReservations
        //{
        //    get
        //    {
        //        return _updateReservations ?? (_updateReservations = new Command(() => updateReservations(), true));
        //    }
        //}

        CommandPara1 _deleteReservation;
        public CommandPara1 DeleteReservation
        {
            get
            {
                return _deleteReservation ?? (_deleteReservation = new CommandPara1((object a) => deleteReservation((ReservationVM)a), true));
            }
        }

        CommandPara1 _updateCreateReservation;
        public CommandPara1 UpdateCreateReservation
        {
            get
            {
                return _updateCreateReservation ?? (_updateCreateReservation = new CommandPara1((object a) => updateCreateReservation((ReservationVM)a), true));
            }
        }
        #endregion Commands

        public CustomerVM(HttpRequest.Models.Customer customer)
        {
            _customer = customer;
        }
    }
}
