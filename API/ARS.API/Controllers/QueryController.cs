using ARS.API.Models;
using ARS.API.Models.Queries;
using ARS.Common.Models.Responses;
using ARS.Models.Requests;
using ARS.Service.Interfaces;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace ARS.API.Controllers
{
    [RoutePrefix("query")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class QueryController : ApiController
    {
        private readonly IARSService arsService;

        public QueryController(IARSService arsService)
        {
            this.arsService = arsService;
        }

        [Route("travels"), HttpGet, HttpPost]
        [ResponseType(typeof(ARSServiceResponse<Travels>))]
        public async Task<IHttpActionResult> GetCategories([FromBody]TravelQuery query)
        {

            #region [ Request processing ]

            // Create the main object
            TravelQueryRequest req = new TravelQueryRequest()
            {
                Arrival = query.Arrival,
                Departure = query.Departure,
                From = query.From,
                Includes = query.Includes,
                SearchKey = query.SearchKey,
                To = query.To,
                TravelIds = query.TravelIds,
            };

            #endregion

            #region [ Service call ]

            // Make an async call
            var serviceResponse = await Task<ARSServiceResponse<ARS.Models.Models.Travels>>.Factory.StartNew(() =>
            {
                // Do the service call
                var innerResponse = arsService.GetTravels(req);

                // Set the inner response fields
                return innerResponse;
            });

            #endregion

            #region [ Response creation ]

            // Create API response from service response
            var response = new ARSServiceResponse<Travels>()
            {
                Type = serviceResponse.Type,
                Errors = serviceResponse.Errors,
                Result = serviceResponse.Result.Select(c => new Travels() 
                {
                    Arrival = c.Arrival,
                    ArrivingCountryId = c.ArrivingCountryId,
                    DepartingCountryId = c.DepartingCountryId,
                    Departure = c.Departure,
                    Description = c.Description,
                    DriverId = c.DriverId,
                    ParentId = c.ParentId,
                    RideType = (RideType)c.RideType,
                    SeatCount = c.SeatCount,
                    Status = (EntityStatus)c.Status,
                    TravelId = c.TravelId,
                    Children = c.Children.Any() ? c.Children.Select(cc => new Travels()
                    {
                        Arrival = cc.Arrival,
                        ArrivingCountryId = cc.ArrivingCountryId,
                        DepartingCountryId = cc.DepartingCountryId,
                        Departure = cc.Departure,
                        Description = cc.Description,
                        DriverId = cc.DriverId,
                        ParentId = cc.ParentId,
                        RideType = (RideType)cc.RideType,
                        SeatCount = cc.SeatCount,
                        Status = (EntityStatus)cc.Status,
                        TravelId = cc.TravelId,
                    }).ToList() : new List<Travels>(),
                }).ToList()
            };

            #endregion

            return base.Ok(response);
        }
    }
}
