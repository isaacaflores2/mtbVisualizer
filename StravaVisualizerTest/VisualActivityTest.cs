using IO.Swagger.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StravaVisualizer.Models.Activities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StravaVisualizerTest
{   
    [TestClass]
    public class VisualActivityTest
    {
        [TestMethod]        
        public void Test_ConvertSummaryToActivity()
        {
            SummaryActivity summaryActivity = new SummaryActivity(type: ActivityType.Crossfit, 
                startLatlng: new LatLng(), endLatlng: new LatLng(), movingTime: 60, athlete: new MetaAthlete(123));
            summaryActivity.StartLatlng.Add(30.0F);
            summaryActivity.StartLatlng.Add(40.0F);
            summaryActivity.EndLatlng.Add(30.0F);
            summaryActivity.EndLatlng.Add(40.0F);

            VisualActivity activity = new VisualActivity(summaryActivity);

            Assert.AreEqual(activity.StartLat, summaryActivity.StartLatlng[0]);
            Assert.AreEqual(activity.StartLong, summaryActivity.StartLatlng[1]);
            Assert.AreEqual(activity.EndLat, summaryActivity.EndLatlng[0]);
            Assert.AreEqual(activity.EndLong, summaryActivity.EndLatlng[1]);
        }
    }
}
