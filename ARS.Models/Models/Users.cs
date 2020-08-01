using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARS.Models.Models
{
    public class Users : Entity
    {
        [Key]
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
