using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARS.API.Models
{
    public class TravelUsers : APIEntity
    {
        public int UserId { get; set; }

        public int TravelId { get; set; }

        #region [ Navigation Properties ]

        public Users User { get; set; }

        public Travels Travel { get; set; }

        #endregion
    }
}
