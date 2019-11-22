using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MtbVisualizer.Controllers;
using MtbVisualizer.Data;
using MtbVisualizer.Models;
using MtbVisualizer.Models.Activities;
using MtbVisualizerTest.Doubles;
using NSubstitute;

namespace mtbVisualizerTest
{
    [TestClass]
    class ActivitiesServiceTest
    {
        private IStravaClient stravaClient;
        private IStravaVisualizerRepository context;
        private string accessToken = "access_token";
        private int id = 123;

        [TestInitialize]
        public void Setup()
        {
            stravaClient = Substitute.For<IStravaClient>();
            IEnumerable<VisualActivity> activities = TestData.MonthVisualActivitiesList();
            stravaClient.getAllUserActivities(accessToken, id).Returns(activities);


            context = Substitute.For<IStravaVisualizerRepository>();

        }

        [TestMethod]
        public void Test_GetUpdatedUserActivities()
        {
            

            Assert.Fail();
        }

    }
}
