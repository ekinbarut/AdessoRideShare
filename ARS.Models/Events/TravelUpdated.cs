using ARS.Common.Models;
using ARS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARS.Models.Events
{
    public class TravelUpdated : BaseEvent
    {
        public int TravelId { get; set; }

        public int DriverId { get; set; }

        public string Description { get; set; }

        public int SeatCount { get; set; }

        public DateTime Departure { get; set; }

        public DateTime Arrival { get; set; }

        public int DepartingCountryId { get; set; }

        public int ArrivingCountryId { get; set; }

        public RideType RideType { get; set; }

    }
}
