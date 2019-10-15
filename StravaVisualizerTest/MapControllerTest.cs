using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stravaVisualizer.Controllers;
using NSubstitute;
using Microsoft.AspNetCore.Http;
using StravaVisualizer.Models;
using System.Security.Claims;
using System.Collections.Generic;
using IO.Swagger.Model;
using StravaVisualizer.Models.Map;
using StravaVisualizer.Models.Activities;
using System;
using System.Linq;
using StravaVisualizerTest.Doubles;

namespace StravaVisualizerTest
{
    [TestClass]
    public class MapControllerTest
    {
        private IHttpContextHelper httpContextHelper;
        private IStravaClient stravaClient;
        private IMap map;
        private IEnumerable<StravaUser> userActivities;
        private IStravaVisualizerRepository userActivityRepository;

        [TestInitialize]
        public void Setup()
        {
            httpContextHelper = Substitute.For<IHttpContextHelper>();
            httpContextHelper.getAccessToken().Returns("access_token");

            stravaClient = Substitute.For<IStravaClient>();         
            IEnumerable<VisualActivity> activities = TestData.VisualActivitiesList(); 
            IEnumerable<VisualActivity> newUserActivities = TestData.NewVisualActivitiesList();
            stravaClient.getAllUserActivities("access_token", 123).Returns(activities);
            stravaClient.getAllUserActivities("access_token", 2222).Returns(newUserActivities);
            stravaClient.getUserActivitiesAfter("access_token", Arg.Any<StravaUser>(), Arg.Any<DateTime>()).Returns(newUserActivities);
        
            map = new Map();

            var userActivity = new StravaUser { VisualActivities = (List<VisualActivity>) activities, UserId = 2, LastDownload = DateTime.Now };
            userActivities = new List<StravaUser>
            {
                new StravaUser {VisualActivities = (List<VisualActivity>)activities, UserId = 1, LastDownload = DateTime.Now},
                userActivity,
                new StravaUser {VisualActivities = (List<VisualActivity>)activities, UserId = 3, LastDownload = DateTime.Now},

            }.AsQueryable();
            userActivityRepository = Substitute.For<IStravaVisualizerRepository>();
            userActivityRepository.GetUserActivities().Returns(userActivities);            
            userActivityRepository.GetStravaUserById(123).Returns(userActivity);
            userActivityRepository.GetStravaUserById(2222).Returns(new StravaUser());
            
        }

        [TestMethod]
        public void Test_Index_Return_View()
        {          
            MapController controller = new MapController(httpContextHelper, stravaClient, map, userActivityRepository);

            var result = controller.Index() as ViewResult;

            Assert.AreEqual("MapAsync", result.ViewName);
        }

        [TestMethod]
        public void Test_LoadMap_Return_View()
        {           
            MapController controller = new MapController(httpContextHelper, stravaClient, map, userActivityRepository);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("stravaId", "123")               
            }, "mock"));
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var result = controller.LoadMap() as PartialViewResult;

            Assert.AreEqual("_BingMapPartial", result.ViewName);
        }

        [TestMethod]
        public void Test_LoadMap_Context_Data_For_Returning_User()
        {
            MapController controller = new MapController(httpContextHelper, stravaClient, map, userActivityRepository);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("stravaId", "123")
            }, "mock"));
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var result = controller.LoadMap() as PartialViewResult;

            Assert.AreEqual(4, ((List<Coordinate>)result.Model).Count());
            Assert.AreEqual(typeof(List<Coordinate>), result.Model.GetType());
            Assert.AreEqual( 30.6F , ((IList<Coordinate>)result.Model)[1].Latitude);
        }

        [TestMethod]
        public void Test_LoadMap_Context_Data_For_New_User()
        {
            MapController controller = new MapController(httpContextHelper, stravaClient, map, userActivityRepository);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("stravaId", "2222")
            }, "mock"));
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var result = controller.LoadMap() as PartialViewResult;

            Assert.AreEqual(2, ((List<Coordinate>)result.Model).Count());
            Assert.AreEqual(typeof(List<Coordinate>), result.Model.GetType());
            Assert.AreEqual(30.6F, ((IList<Coordinate>)result.Model)[1].Latitude);
        }
        
        [TestMethod]
        public void Test_LoadMapByType()
        {
            MapController controller = new MapController(httpContextHelper, stravaClient, map, userActivityRepository);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("stravaId", "123")
            }, "mock"));
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var result = controller.LoadMapByType("Ride") as PartialViewResult;
            var activities = result.Model as List<Coordinate>;
            Assert.AreEqual(3, ((List<Coordinate>)result.Model).Count());            
            Assert.AreEqual(30.6F,  activities[0].Latitude);
            Assert.AreEqual(40.6F,  activities[0].Longitude);
        }
    }
}
