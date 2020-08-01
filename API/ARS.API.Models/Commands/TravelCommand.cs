using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARS.API.Models.Commands
{
    public class TravelCommand : BaseCommand
    {
        public int TravelId { get; set; }

        public int? ParentId { get; set; }

        public int DriverId { get; set; }

        public int PassengerId { get; set; }

        public string Description { get; set; }

        public int SeatCount { get; set; }

        public DateTime Departure { get; set; }

        public DateTime Arrival { get; set; }

        public int DepartingCountryId { get; set; }

        public int ArrivingCountryId { get; set; }

        public RideType RideType { get; set; }

        public Travels ParentTravel { get; set; }

        public Users Driver { get; set; }

        public Countries DepartingCountry { get; set; }

        public Countries ArrivingCountry { get; set; }

        public List<Travels> Children { get; set; } = new List<Travels>();
    }
}
