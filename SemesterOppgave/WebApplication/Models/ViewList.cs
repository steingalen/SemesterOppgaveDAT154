using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using HttpRequest.Models;

namespace WebApp.Models
{
    public class ViewList
    {
        public List<RoomQuality> RoomQuality { get; set; }
        public List<RoomSize> RoomSize { get; set; }
        public List<RoomBeds> RoomBeds { get; set; }
    }
}