using System;
using System.Collections.Generic;
using System.Text;

namespace HMS.Entities
{
    public class Booking
    {
        public int ID { get; set; }
        public int AccomodationID { get; set; }
        public Accomodation Accomodation { get; set; }
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Number of staying nights
        /// </summary>
        public int Duration { get; set; }
    }
}
