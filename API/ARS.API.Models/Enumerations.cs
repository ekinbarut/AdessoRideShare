using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARS.API.Models
{
    public enum EntityStatus
    {
        Deleted = -99,
        Passive = -1,
        Draft = 0,
        Active = 1
    }

    public enum ServiceResponseTypes
    {
        Error = -99,
        Declined = -1,
        Success = 1
    }
    public enum RideType
    {
        Car = 10,
        Bus = 20,
        Van = 30
    }
}
