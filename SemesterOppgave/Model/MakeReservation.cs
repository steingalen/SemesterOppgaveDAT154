using System;

namespace Models {
    public class MakeReservation {
        
        public int Id { get; set; }
        
        public string Email { get; set; }
        
        public DateTime Start { get; set; }
        
        public DateTime End { get; set; }
        
        public string Quality { get; set; }
        
        public int Beds { get; set; }
        
        public string Size { get; set; }
    }
}