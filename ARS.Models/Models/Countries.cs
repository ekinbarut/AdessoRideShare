using ARS.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARS.Models.Models
{
    public class Countries : Entity
    {
        [Key]
        public int CountryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        #region [ Navigation Properties ]

        public List<Travels> Departures { get; set; }

        public List<Travels> Arrivals { get; set; }

        #endregion

    }
}
