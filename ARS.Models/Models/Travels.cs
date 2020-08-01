using ARS.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARS.Models.Models
{
    public class Travels : Entity
    {
        [Key]
        public int TravelId { get; set; }

        public int? ParentId { get; set; }

        public int DriverId { get; set; }

        public string Description { get; set; }

        public int SeatCount { get; set; }

        public DateTime Departure { get; set; }

        public DateTime Arrival { get; set; }

        public int DepartingCountryId { get; set; }

        public int ArrivingCountryId { get; set; }

        public RideType RideType { get; set; }

        #region [ Navigation Properties ]

        [ForeignKey("ParentId")]
        public Travels ParentTravel { get; set; }

        [ForeignKey("DriverId")]
        public Users Driver { get; set; }

        [ForeignKey("DepartingCountryId")]
        public Countries DepartingCountry { get; set; }

        [ForeignKey("ArrivingCountryId")]
        public Countries ArrivingCountry { get; set; }

        public List<TravelUsers> Passengers = new List<TravelUsers>();

        [InverseProperty("ParentTravel")]
        public List<Travels> Children { get; set; } = new List<Travels>();

        #endregion
    }
}
