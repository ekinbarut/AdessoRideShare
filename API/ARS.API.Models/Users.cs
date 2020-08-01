using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARS.API.Models
{
    public class Users : APIEntity
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string MobileNumber { get; set; }

        #region [ Navigation Properties ]

        public List<TravelUsers> TravelUsers = new List<TravelUsers>();

        #endregion
    }
}
