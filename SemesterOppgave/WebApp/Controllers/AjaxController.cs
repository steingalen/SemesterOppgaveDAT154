using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HttpRequest;
using HttpRequest.Models;
using WebApp.Models;
using WebAPI.Models;
using Customer = HttpRequest.Models.Customer;
using MakeReservation = WebAPI.Models.MakeReservation;
using Reservation = HttpRequest.Models.Reservation;
using Room = HttpRequest.Models.Room;

namespace WebApp.Controllers
{
    public class AjaxController : Controller {
        // GET: Ajax
        [HttpGet]
        public async Task<ActionResult> Search() {
            //"/?Quality=0&Size=0&Beds=0&Start=0&End=0";
            var quality = HttpContext.Request.Params.Get("Quality");
            var size = HttpContext.Request.Params.Get("Size");
            var beds = HttpContext.Request.Params.Get("Beds");
            var start = HttpContext.Request.Params.Get("Start");
            var end = HttpContext.Request.Params.Get("End");

            var jsonFromWebService =
                await ApiRequests.Get(ApiUrl.ROOMS, quality + "/" + size + "/" + beds + "/" + start + "/" + end);
            var deserialized = JsonSerializer<Room>.DeSerializeAsList(jsonFromWebService);
            var serialized = JsonSerializer<Room>.SerializeList(deserialized);


            return Json(serialized, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> MakeReservation(MakeReservation makeReservation) {
            var json = JsonSerializer<MakeReservation>.Serialize(makeReservation);
            var jsonFromWebService = await ApiRequests.Post(ApiUrl.MAKE_RESERVATION, json);

            return Json(jsonFromWebService, JsonRequestBehavior.AllowGet);
        }

        // Hashe epost + DateTime.now().ToString()
        // Lagre det i customer db
        // Blir invalidert etter x timer
        // Adde hash som member i MakeReservation
        // For å kunne foreta Reservasjon må man skjekke hashen mot databasen

        [HttpPost]
        [Route("api/ajax/signin/")]
        public async Task<ActionResult> SignIn(Customer customer) {
            var json = JsonSerializer<Customer>.Serialize(customer);
            var jsonFromWebService = await ApiRequests.Post(ApiUrl.CUSTOMERS_EXIST, json);
            return Json(jsonFromWebService, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [Route("api/ajax/signup/")]
        public async Task<ActionResult> SignUp(Customer customer)
        {
            var json = JsonSerializer<Customer>.Serialize(customer);
            var jsonFromWebService = await ApiRequests.Post(ApiUrl.CUSTOMERS, json);    
            return Json(jsonFromWebService, JsonRequestBehavior.DenyGet);
        }
    }
}