using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using MtbVisualizer.Controllers;
using MtbVisualizer.Data;
using MtbVisualizer.Models;
using MtbVisualizer.Models.Activities;
using MtbVisualizer.Models.MonthSummary;
using MtbVisualizerTest.Doubles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace MtbVisualizerTest
{
    [TestClass]
    public class HomeControllerTest
    {
        private IHttpContextHelper httpContextHelper;
        private IStravaClient stravaClient;
        private IStravaVisualizerRepository context;
        private IEnumerable<StravaUser> userActivities;
        private ILogger<HomeController> logger;


        [TestInitialize]
        public void Setup()
        {
            httpContextHelper = Substitute.For<IHttpContextHelper>();
            httpContextHelper.getAccessToken().Returns("access_token");
            

            stravaClient = Substitute.For<IStravaClient>();
            IEnumerable<VisualActivity> activities = TestData.MonthVisualActivitiesList();
            //IEnumerable<VisualActivity> newUserActivities = TestData.NewVisualActivitiesList();
            stravaClient.getAllUserActivities("access_token", 123).Returns(activities);
            //stravaClient.getAllUserActivities("access_token", 2222).Returns(newUserActivities);
            //stravaClient.getUserActivitiesAfter("access_token", Arg.Any<StravaUser>(), Arg.Any<DateTime>()).Returns(newUserActivities);

            var userActivity = new StravaUser { VisualActivities = (List<VisualActivity>)activities, UserId = 123, LastDownload = DateTime.Now };
            userActivities = new List<StravaUser>
            {
                new StravaUser {VisualActivities = (List<VisualActivity>)activities, UserId = 1, LastDownload = DateTime.Now},
                userActivity,
                new StravaUser {VisualActivities = (List<VisualActivity>)activities, UserId = 3, LastDownload = DateTime.Now},

            }.AsQueryable();

            context = Substitute.For<IStravaVisualizerRepository>();
            context.GetUserActivities().Returns(userActivities);
            context.GetStravaUserById(123).Returns(userActivity);
            context.GetStravaUserById(2222).Returns(new StravaUser());

            logger = new NullLogger<HomeController>();
        }

        [TestMethod]
        public void Test_Index_Return_View()
        {
            HomeController controller = new HomeController(httpContextHelper, stravaClient, context, logger);
            var claims = new Claim[] { new Claim("stravaId", "123") };
            var identity = new ClaimsIdentity(claims, "mock");
            var user = new ClaimsPrincipal(identity);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var result = controller.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void Test_Index_For_User_With_No_Data()
        {
            HomeController controller = new HomeController(httpContextHelper, stravaClient, context, logger);
            var claims = new Claim[] { new Claim("stravaId", "222") };
            var identity = new ClaimsIdentity(claims, "mock");
            var user = new ClaimsPrincipal(identity);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var result = controller.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void Test_Calendar_Partial_View()
        {
            HomeController controller = new HomeController(httpContextHelper, stravaClient, context, logger);
            var claims = new Claim[] { new Claim("stravaId", "123") };
            var identity = new ClaimsIdentity(claims, "mock");
            var user = new ClaimsPrincipal(identity);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var testDate = new DateTime(2019, 10, 17);
            var result = controller.LoadCalendarPartial(testDate) as PartialViewResult;

            Assert.AreEqual("_CalendarPartial", result.ViewName);
            Assert.AreEqual(30, ((MonthSummary)result.Model).Activites.Count());
        }

        [TestMethod]
        public void Test_Table_Partial_View()
        {
            HomeController controller = new HomeController(httpContextHelper, stravaClient, context, logger);
            var claims = new Claim[] { new Claim("stravaId", "123") };
            var identity = new ClaimsIdentity(claims, "mock");
            var user = new ClaimsPrincipal(identity);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var testDate = new DateTime(2019, 10, 17);
            var result = controller.LoadTablePartial(testDate) as PartialViewResult;

            Assert.AreEqual("_TablePartial", result.ViewName);
            Assert.AreEqual(7, ((IList<VisualActivity>)result.Model).Count);
        }

        [TestMethod]
        public void Test_Privacy_Return_View()
        {
            HomeController controller = new HomeController(httpContextHelper, stravaClient, context, logger);

            var result = controller.Privacy() as ViewResult;

            Assert.AreEqual("Privacy", result.ViewName);
        }
    }
}
