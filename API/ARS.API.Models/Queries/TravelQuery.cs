using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARS.API.Models.Queries
{
    public class TravelQuery : BaseQuery
    {
        public List<int> TravelIds = new List<int>();

        public List<string> From { get; set; } = new List<string>();

        public List<string> To { get; set; } = new List<string>();

        public List<DateTime> Arrival { get; set; } = new List<DateTime>();

        public List<DateTime> Departure { get; set; } = new List<DateTime>();
    }
}
