using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARS.API.Models
{
    public class Countries : APIEntity
    {
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
