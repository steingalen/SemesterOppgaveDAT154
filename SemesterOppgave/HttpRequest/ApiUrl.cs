using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HttpRequest
{
    public class ApiUrl
    {
        private static readonly string _baseUri = "http://localhost:62707/api/";
        public static readonly string ROOMS = _baseUri + "rooms/";
        public static readonly string ROOMQUALITY = _baseUri + "roomqualities/";
        public static readonly string ROOMSIZE = _baseUri + "roomsizes/";
        public static readonly string ROOMBEDS = _baseUri + "roombeds/";
        public static readonly string RESERVATIONS = _baseUri + "reservations/";
        public static readonly string TASKTYPES = _baseUri + "taskTypes/";
        public static readonly string ROOMTASKS = _baseUri + "roomTasks/";
        public static readonly string CUSTOMERS = _baseUri + "customers/";
        public static readonly string MAKE_RESERVATION = _baseUri + "makereservations/";
        public static readonly string CUSTOMERS_EXIST = _baseUri + "customersexist/";
    }
}
