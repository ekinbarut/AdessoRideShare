using System;
using ARS.Common.Models;
using ARS.Models.Events;
using ARS.Service;
using ARS.Service.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ARS.Test
{
    [TestClass]
    public class ServiceTests
    {
        IARSService service = new ARSService();

        [TestMethod]
        public void CreateTravel()
        {
            var response = service.CreateTravel(new TravelCreated()
            {
                Arrival = DateTime.UtcNow.AddDays(2),
                ArrivingCountryId = 1,
                DepartingCountryId = 4,
                Departure = DateTime.UtcNow.AddDays(1),
                Description = "test",
                DriverId = 1,
                RideType = Common.Models.RideType.Car,
                SeatCount = 4,
                Status = Common.Models.EntityStatus.Active,
                SubTravels = new System.Collections.Generic.List<TravelCreated>()
                {
                    new TravelCreated()
                    {
                        Arrival = DateTime.UtcNow.AddDays(2),
                        ArrivingCountryId = 2,
                        DepartingCountryId = 3,
                        Departure = DateTime.UtcNow.AddDays(1),
                        Description = "test",
                        DriverId = 1,
                        RideType = Common.Models.RideType.Car,
                        SeatCount = 4,
                        Status = Common.Models.EntityStatus.Active,
                    }
                },
                TimeStamp = DateTime.UtcNow
            });

            Assert.AreEqual(ServiceResponseTypes.Success, response.Type);
        }

        [TestMethod]
        public void GetTravels()
        {
            var response = service.GetTravels(new Models.Requests.TravelQueryRequest()
            {
                SearchKey = "Country1"
            });

            Assert.AreEqual(ServiceResponseTypes.Success, response.Type);
        }
    }
}
