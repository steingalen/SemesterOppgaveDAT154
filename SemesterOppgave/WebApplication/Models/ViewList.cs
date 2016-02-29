using System.Collections.Generic;
using Models;

namespace WebApplication.Models
{
    public class ViewList
    {
        public List<RoomQuality> RoomQuality { get; set; }
        public List<RoomSize> RoomSize { get; set; }
        public List<RoomBeds> RoomBeds { get; set; }
    }
}