using ARS.API.Models;
using ARS.API.Models.Commands;
using ARS.Common.Models.Responses;
using ARS.Models.Events;
using ARS.Service.Interfaces;
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
    [RoutePrefix("travels")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TravelsController : ApiController
    {
        private readonly IARSService arsService;

        public TravelsController(IARSService arsService)
        {
            this.arsService = arsService;
        }

        [Route("create"), HttpPost]
        [ResponseType(typeof(ARSServiceResponse<Travels>))]
        public async Task<IHttpActionResult> CreateTravel([FromBody]TravelCommand command)
        {
            #region [ Request processing ]

            // Create the main object
            var ev = new TravelCreated()
            {
                Arrival = command.Arrival,
                ArrivingCountryId = command.ArrivingCountryId,
                DepartingCountryId = command.DepartingCountryId,
                Departure = command.Departure,
                Description = command.Description,
                DriverId = command.DriverId,
                RideType = (Common.Models.RideType)command.RideType,
                SeatCount = command.SeatCount,
                Status = Common.Models.EntityStatus.Active,
                TimeStamp = DateTime.UtcNow,
                SubTravels = command.Children.Select(c => new TravelCreated()
                {
                    Arrival = c.Arrival,
                    ArrivingCountryId = c.ArrivingCountryId,
                    DepartingCountryId = c.DepartingCountryId,
                    Departure = c.Departure,
                    Description = c.Description,
                    DriverId = c.DriverId,
                    RideType = (Common.Models.RideType)c.RideType,
                    SeatCount = c.SeatCount,
                    Status = Common.Models.EntityStatus.Active,
                    TimeStamp = DateTime.UtcNow,
                }).ToList()
            };

            #endregion

            #region [ Service call ]

            // Make an async call
            var serviceResponse = await Task<ARSServiceResponse<ARS.Models.Models.Travels>>.Factory.StartNew(() =>
            {

                // Do the service call
                var innerResponse = arsService.CreateTravel(ev);

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

        [Route("update/status"), HttpPost]
        [ResponseType(typeof(ARSServiceResponse<Travels>))]
        public async Task<IHttpActionResult> UpdateTravelStatus([FromBody]TravelCommand command)
        {
            #region [ Request processing ]

            // Create the main object
            var ev = new TravelUpdated()
            {
               TravelId = command.TravelId,
               Status = (Common.Models.EntityStatus)command.Status,
            };

            #endregion

            #region [ Service call ]

            // Make an async call
            var serviceResponse = await Task<ARSServiceResponse<ARS.Models.Models.Travels>>.Factory.StartNew(() =>
            {
                // Do the service call
                var innerResponse = arsService.UpdateTravelStatus(ev);

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

        [Route("join"), HttpPost]
        [ResponseType(typeof(ARSServiceResponse<Travels>))]
        public async Task<IHttpActionResult> JoinTravel([FromBody]TravelCommand command)
        {
            #region [ Request processing ]

            // Create the main object
            var ev = new TravelUpdated()
            {
                TravelId = command.TravelId,
                PassengerId = command.PassengerId
            };

            #endregion

            #region [ Service call ]

            // Make an async call
            var serviceResponse = await Task<ARSServiceResponse<ARS.Models.Models.Travels>>.Factory.StartNew(() =>
            {

                // Do the service call
                var innerResponse = arsService.JoinTravel(ev);

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
