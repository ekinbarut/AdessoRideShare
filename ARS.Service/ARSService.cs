using ARS.Common.Models;
using ARS.Models.Events;
using ARS.Models.Models;
using ARS.Models.Requests;
using ARS.Models.Responses;
using ARS.Service.Base;
using ARS.Service.Interfaces;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ARS.Service
{
    public class ARSService : ServiceBase, IARSService
    {
        private TravelServiceBase travelServiceBase = new TravelServiceBase();
        private UsersServiceBase userServiceBase = new UsersServiceBase();
        private TravelUsersServiceBase travelUserServiceBase = new TravelUsersServiceBase();


        #region [ Queries ]

        public ARSServiceResponse<Travels> GetTravels(TravelQueryRequest req)
        {
            var response = new ARSServiceResponse<Travels>()
            {
                Result = new List<Travels>()
            };

            // Create a default list
            var entities = new List<Travels>();

            #region [ Envelope settings ]

            // Create the including fields according to the envelope
            var includes = req.Includes;

            #endregion

            #region [ Filters ]

            // Check for filters
            Expression<Func<Travels, bool>> filterPredicate = PredicateBuilder.New<Travels>(true);

            // Check for entity ids
            if (req.TravelIds.Any())
            {
                filterPredicate = filterPredicate.And(e => req.TravelIds.Contains(e.TravelId));
            }

            if (req.From.Any())
            {
                filterPredicate = filterPredicate.And(e => req.From.Contains(e.DepartingCountry.Name));
            }

            if (req.To.Any())
            {
                filterPredicate = filterPredicate.And(c => req.To.Contains(c.ArrivingCountry.Name));
            }

            if (req.Arrival.Any())
            {

                filterPredicate = filterPredicate.And(c => req.Arrival.Contains(c.Arrival));
            }

            if (req.Departure.Any())
            {
                filterPredicate = filterPredicate.And(m => req.Departure.Contains(m.Departure));
            }

            if (!String.IsNullOrEmpty(req.SearchKey))
            {
                filterPredicate = filterPredicate.And(s => s.DepartingCountry.Name.Contains(req.SearchKey) || s.ArrivingCountry.Name.Contains(req.SearchKey) || s.Children.Any(c=>c.DepartingCountry.Name.Contains(req.SearchKey)) || s.Children.Any(c => c.ArrivingCountry.Name.Contains(req.SearchKey))) ;
            }

            #endregion

            // Make the query
            var baseServiceResponse = new ARSServiceResponse<Travels>();

            if (filterPredicate.Parameters.Count > 0)
            {
                baseServiceResponse = travelServiceBase.GetIncluding(filterPredicate, includes.ToArray());
            }
            else
            {
                baseServiceResponse = travelServiceBase.GetAllIncluding(includes.ToArray());
            }

            entities = baseServiceResponse.Result;

            // Make the results distinct
            var distinctEntities = entities.GroupBy(se => se.TravelId).Select(g => g.First()).ToList();

            response.Result = distinctEntities;

            // Set the response fields
            response.Result = distinctEntities;
            response.Type = ServiceResponseTypes.Success;

            return response;
        }

        #endregion

        #region [ Commands ]

        public ARSServiceResponse<Travels> CreateTravel(TravelCreated ev)
        {
            // Create the watch
            var sw = new Stopwatch();
            sw.Start();

            // Create a response object
            var response = new ARSServiceResponse<Travels>();

            #region [ Create travel ]

            Travels travel = new Travels()
            {
                Arrival = ev.Arrival,
                ArrivingCountryId = ev.ArrivingCountryId,
                Created = DateTime.UtcNow,
                DepartingCountryId = ev.DepartingCountryId,
                Children = ev.SubTravels.Select(c => new Travels()
                {
                    Arrival = c.Arrival,
                    ArrivingCountryId = c.ArrivingCountryId,
                    Created = DateTime.UtcNow,
                    DepartingCountryId = c.DepartingCountryId,
                    Departure = c.Departure,
                    Description = c.Description,
                    DriverId = c.DriverId,
                    RideType = c.RideType,
                    SeatCount = c.SeatCount,
                    Status = c.Status
                }).ToList(),
                Departure = ev.Departure,
                Description = ev.Description,
                DriverId = ev.DriverId,
                RideType = ev.RideType,
                SeatCount = ev.SeatCount,
                Status = ev.Status
            };

            #endregion

            #region [ Save travel ]

            // Save the travel
            var baseServiceResponse = travelServiceBase.Create(travel);

            if (baseServiceResponse.Type != ServiceResponseTypes.Success)
            {
                // Add log

                response.Type = ServiceResponseTypes.Error;
                response.Errors.Add("There was an error while creting the travel!");
                response.Errors.AddRange(baseServiceResponse.Errors);
                return response;
            }
            
            #endregion

            response.Type = ServiceResponseTypes.Success;
            response.Result.Add(travel);

            return response;
        }

        public ARSServiceResponse<Travels> UpdateTravelStatus(TravelUpdated ev)
        {
            // Create the watch
            var sw = new Stopwatch();
            sw.Start();

            // Create a response object
            var response = new ARSServiceResponse<Travels>();

            #region [ Load travel ]

            Travels travel = travelServiceBase.Get(s => s.TravelId == ev.TravelId).Result.FirstOrDefault();

            if(travel == null)
            {
                //Add Log

                response.Type = ServiceResponseTypes.Error;
                response.Errors.Add("travel could not be found");
                return response;
            }

            #endregion

            #region [ update travel ]

            travel.Status = ev.Status;

            // update the travel
            var baseServiceResponse = travelServiceBase.Update(travel);

            if (baseServiceResponse.Type != ServiceResponseTypes.Success)
            {
                // Add log

                response.Type = ServiceResponseTypes.Error;
                response.Errors.Add("There was an error while creting the travel!");
                response.Errors.AddRange(baseServiceResponse.Errors);
                return response;
            }

            #endregion

            response.Type = ServiceResponseTypes.Success;
            response.Result.Add(travel);

            return response;
        }

        public ARSServiceResponse<Travels> JoinTravel(TravelUpdated ev)
        {
            // Create the watch
            var sw = new Stopwatch();
            sw.Start();

            // Create a response object
            var response = new ARSServiceResponse<Travels>();

            #region [ Load travel ]

            var travel = GetTravels(new TravelQueryRequest()
            {
                Includes = new List<string> { "Passengers" },
                TravelIds = new List<int> { ev.TravelId }
            }).Result.FirstOrDefault();

            if (travel == null)
            {
                //Add Log

                response.Type = ServiceResponseTypes.Error;
                response.Errors.Add("travel could not be found");
                return response;
            }

            #endregion

            #region [ validate data ] 

            //check available seat count
            if(travel.Passengers.Count() >= travel.SeatCount)
            {
                //Add log

                response.Type = ServiceResponseTypes.Declined;
                response.Errors.Add("no more available seats");
                return response;
            }

            #endregion

            #region [ Load User ]

            var user = userServiceBase.Get(s => s.UserId == ev.UserId).Result.FirstOrDefault();

            if (user == null)
            {
                //Add Log

                response.Type = ServiceResponseTypes.Error;
                response.Errors.Add("user could not be found");
                return response;
            }

            #endregion

            #region [ join travel ]

            var passenger = new TravelUsers()
            {
                UserId = user.UserId,
                TravelId = travel.TravelId
            };

            // update the travel
            var baseServiceResponse = travelUserServiceBase.Create(passenger);

            if (baseServiceResponse.Type != ServiceResponseTypes.Success)
            {
                // Add log

                response.Type = ServiceResponseTypes.Error;
                response.Errors.Add("There was an error while creting the travel!");
                response.Errors.AddRange(baseServiceResponse.Errors);
                return response;
            }

            #endregion

            response.Type = ServiceResponseTypes.Success;
            response.Result.Add(travel);

            return response;
        }

        #endregion
    }
}
