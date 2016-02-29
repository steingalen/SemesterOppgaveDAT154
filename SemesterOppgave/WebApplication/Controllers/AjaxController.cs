using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HttpRequest;
using Microsoft.AspNet.Identity.Owin;
using MakeReservation = WebAPI.Models.MakeReservation;
using Room = HttpRequest.Models.Room;

namespace WebApplication.Controllers
{
    public class AjaxController : Controller
    {
        // GET: Ajax
        [HttpGet]
        public async Task<ActionResult> Search()
        {
            //"/?Quality=0&Size=0&Beds=0&Start=0&End=0";
            var quality = HttpContext.Request.Params.Get("Quality");
            var size = HttpContext.Request.Params.Get("Size");
            var beds = HttpContext.Request.Params.Get("Beds");
            var start = HttpContext.Request.Params.Get("Start");
            var end = HttpContext.Request.Params.Get("End");

            var jsonFromWebService =
                await ApiRequests.Get(ApiUrl.ROOMS, quality + "/"  + beds + "/" + size + "/" + start + "/" + end);
            var deserialized = JsonSerializer<Room>.DeSerializeAsList(jsonFromWebService);
            var serialized = JsonSerializer<Room>.SerializeList(deserialized);


            return Json(serialized, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> MakeReservation(MakeReservation makeReservation) {
            
            var reservation = makeReservation;
            reservation.Email = System.Web.HttpContext.Current.User.Identity.Name;

            var json = JsonSerializer<MakeReservation>.Serialize(reservation);
            var jsonFromWebService = await ApiRequests.Post(ApiUrl.MAKE_RESERVATION, json);

            return Json(jsonFromWebService, JsonRequestBehavior.DenyGet);
        }

        public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
                base.OnActionExecuting(filterContext);
            }
        }

    }
}