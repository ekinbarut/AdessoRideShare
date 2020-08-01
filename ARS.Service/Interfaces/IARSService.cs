using ARS.Common.Models.Responses;
using ARS.Models.Events;
using ARS.Models.Models;
using ARS.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARS.Service.Interfaces
{
    public interface IARSService
    {
        ARSServiceResponse<Travels> GetTravels(TravelQueryRequest req);

        ARSServiceResponse<Travels> CreateTravel(TravelCreated ev);

        ARSServiceResponse<Travels> UpdateTravelStatus(TravelUpdated req);

        ARSServiceResponse<Travels> JoinTravel(TravelUpdated req);
    }
}
