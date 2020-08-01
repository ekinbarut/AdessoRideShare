using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARS.Models.Models
{
    public class TravelUsers : Entity
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }

        [Key, Column(Order = 1)]
        public int TravelId { get; set; }

        #region [ Navigation Properties ]

        [ForeignKey("UserId")]
        public Users User { get; set; }

        [ForeignKey("TravelId")]
        public Travels Travel { get; set; }

        #endregion
    }
}
