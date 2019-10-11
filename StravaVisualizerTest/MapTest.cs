using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IO.Swagger.Model;
using StravaVisualizer.Models;
using Microsoft.AspNetCore.Http;
using StravaVisualizer.Models.Map;
using StravaVisualizer.Models.Activities;
using StravaVisualizerTest.Doubles;
using System.Linq;

namespace StravaVisualizerTest
{
    [TestClass]
    public class MapTest
    {
        ICollection<VisualActivity> visualActivities;

        [TestInitialize]
        public void Setup()
        {
            visualActivities = TestData.VisualActivitiesList();
        }

        [TestMethod]
        public void Test_getCoordinates_withNull_Input()
        {
            Map map = new Map();
               
            Assert.ThrowsException< ArgumentNullException>(()=> map.getCoordinates(null));
        }

        [TestMethod]
        public void Test_getCoordinates_withValid_Input()
        {
            Map map = new Map();

            var result = (List<Coordinate>) map.getCoordinates(visualActivities);

            Assert.AreEqual(30.0F, result[0].Latitude);
            Assert.AreEqual(40.0F, result[0].Longitude);
            Assert.AreEqual(30.6F, result[1].Latitude);
            Assert.AreEqual(40.6F, result[1].Longitude);
        }

        [TestMethod]
        public void Test_extractCoordinates()
        {
            Map map = new Map();
            map.VisualActivities = visualActivities;

            map.extractCoordinates();
            var result = (List<Coordinate>)map.Coordinates;

            Assert.AreEqual(30.0F, result[0].Latitude);
            Assert.AreEqual(40.0F, result[0].Longitude);
            Assert.AreEqual(30.6F, result[1].Latitude);
            Assert.AreEqual(40.6F, result[1].Longitude);            
        }

        [TestMethod]
        public void Test_addCoordinate()
        {
            Map map = new Map();
            map.VisualActivities = visualActivities;

            var coordinate = (from activity in visualActivities
                             select activity).FirstOrDefault();

            map.addCoordinate(coordinate);
            var result = (List<Coordinate>)map.Coordinates;

            Assert.AreEqual(1, result.Count);                
        }

        [TestMethod]
        public void Test_getCoordinatesByType_withNull_Input()
        {
            Map map = new Map();

            Assert.ThrowsException<ArgumentNullException>(() => map.getCoordinatesByType(null, ActivityType.Ride));
        }

        [TestMethod]
        public void Test_getCoordinatesBytype_withValid_Input()
        {
            Map map = new Map();

            var result = (List<Coordinate>)map.getCoordinatesByType(visualActivities, ActivityType.Ride);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(30.6F, result[0].Latitude);
            Assert.AreEqual(40.6F, result[0].Longitude);           
        }

        [TestMethod]
        public void Test_getUniqueCoordinatesByType()
        {
            Map map = new Map();
            var summary = new SummaryActivity(type: ActivityType.Ride, startLatlng: new LatLng(), 
                endLatlng: new LatLng(), movingTime: 60, athlete: new MetaAthlete(123));
            summary.StartLatlng.Add(30.0F);
            summary.StartLatlng.Add(40.0F);
            summary.EndLatlng.Add(30.0F);
            summary.EndLatlng.Add(40.0F);
            visualActivities.Add(new VisualActivity(summary));
            

            map.getUniqueCoordinatesByType(visualActivities, ActivityType.Ride);
            var result = (List<Coordinate>) map.Coordinates;

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(30.6F, result[0].Latitude);
            Assert.AreEqual(40.6F, result[0].Longitude);
            Assert.AreEqual(30.0F, result[1].Latitude);
            Assert.AreEqual(40.0F, result[1].Longitude);
          
        }

        [TestMethod]
        public void Test_updateNumVisits()
        {
            Map map = new Map();
            var summary = new SummaryActivity(type: ActivityType.Ride, startLatlng: new LatLng(), 
                endLatlng: new LatLng(), movingTime: 60, athlete: new MetaAthlete(123));
            summary.StartLatlng.Add(30.0F);
            summary.StartLatlng.Add(40.0F);
            summary.EndLatlng.Add(30.0F);
            summary.EndLatlng.Add(40.0F);
            visualActivities.Add(new VisualActivity(summary));

            foreach ( var activity in visualActivities)
            {
                map.updateNumVisits(activity);
            }            
            var result = map.NumVisits;

            Assert.AreEqual(2, result.Count);
            result.TryGetValue(new Coordinate(30.0F, 40.0F), out var visits);
            Assert.AreEqual(2, visits);
        }
    }
}
