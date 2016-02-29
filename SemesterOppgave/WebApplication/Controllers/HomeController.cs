﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HttpRequest;
using HttpRequest.Models;
using WebApp.Models;

namespace WebApplication.Controllers {
    public class HomeController : Controller {
        // GET: Index
        public async Task<ActionResult> Index() {

            // Fetches static data
            var quality = await ApiRequests.Get(ApiUrl.ROOMQUALITY);
            var sizes = await ApiRequests.Get(ApiUrl.ROOMSIZE);
            var beds = await ApiRequests.Get(ApiUrl.ROOMBEDS);

            // Populate wrapper
            var viewList = new ViewList() {
                RoomQuality = JsonSerializer<RoomQuality>.DeSerializeAsList(quality),
                RoomSize = JsonSerializer<RoomSize>.DeSerializeAsList(sizes),
                RoomBeds = JsonSerializer<RoomBeds>.DeSerializeAsList(beds),
            };

            return View(viewList);
        }
    }
}