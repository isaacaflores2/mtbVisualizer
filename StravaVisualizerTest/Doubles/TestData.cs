using IO.Swagger.Model;
using StravaVisualizer.Models.Activities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StravaVisualizerTest.Doubles
{
    public class  TestData
    {

        public static ICollection<VisualActivity> SingleVisuActvityList()
        {
            var summary1 = new SummaryActivity(startLatlng: new LatLng(), endLatlng: new LatLng(),
                type: ActivityType.Crossfit, movingTime: 60, athlete: new MetaAthlete(123), id: 1);
            
            summary1.StartLatlng.Add(30.0F);
            summary1.StartLatlng.Add(40.0F);            
            summary1.EndLatlng.Add(30.0F);
            summary1.EndLatlng.Add(40.0F);
            

            ICollection<VisualActivity> activities = new List<VisualActivity>
            {
                new VisualActivity(summary1),                   
            };

            return activities;
        }

        public static ICollection<VisualActivity> VisualActivitiesList()
        {
            var summary1 = new SummaryActivity(startLatlng: new LatLng(), endLatlng: new LatLng(), 
                type:ActivityType.Crossfit, movingTime:60, athlete: new MetaAthlete(123), id: 1);
            var summary2 = new SummaryActivity(startLatlng: new LatLng(), endLatlng: new LatLng(), 
                type: ActivityType.Ride, movingTime: 60, athlete: new MetaAthlete(123), id: 2);
            summary1.StartLatlng.Add(30.0F);
            summary1.StartLatlng.Add(40.0F);
            summary2.StartLatlng.Add(30.6F);
            summary2.StartLatlng.Add(40.6F);
            summary1.EndLatlng.Add(30.0F);
            summary1.EndLatlng.Add(40.0F);
            summary2.EndLatlng.Add(30.6F);
            summary2.EndLatlng.Add(40.6F);

            ICollection<VisualActivity> activities = new List<VisualActivity>
            {
                new VisualActivity(summary1),
                new VisualActivity(summary2)
            };

            return activities; 
        }

        public static ICollection<VisualActivity> NewVisualActivitiesList()
        {
            var summary1 = new SummaryActivity(startLatlng: new LatLng(), endLatlng: new LatLng(),
                   type: ActivityType.Ride, movingTime: 60, athlete: new MetaAthlete(123), id: 2);
            var summary2 = new SummaryActivity(startLatlng: new LatLng(), endLatlng: new LatLng(),
                type: ActivityType.Ride, movingTime: 15, athlete: new MetaAthlete(123), id: 3);
            summary1.StartLatlng.Add(30.0F);
            summary1.StartLatlng.Add(40.0F);
            summary2.StartLatlng.Add(30.6F);
            summary2.StartLatlng.Add(40.6F);
            summary1.EndLatlng.Add(30.0F);
            summary1.EndLatlng.Add(40.0F);
            summary2.EndLatlng.Add(30.6F);
            summary2.EndLatlng.Add(40.6F);

            ICollection<VisualActivity> activities = new List<VisualActivity>
            {
                new VisualActivity(summary1),
                new VisualActivity(summary2)
            };

            return activities;
        }

        public static SummaryActivity SummaryActivity1()
        {
            var summary1 = new SummaryActivity(startLatlng: new LatLng(), endLatlng: new LatLng(), 
                type: ActivityType.Crossfit, movingTime: 60, athlete: new MetaAthlete(123));
            
            summary1.StartLatlng.Add(30.0F);
            summary1.StartLatlng.Add(40.0F);
         
            summary1.EndLatlng.Add(30.0F);
            summary1.EndLatlng.Add(40.0F);
          

            return summary1;
        }

        public static SummaryActivity SummaryActivity2()
        {
            
            var summary2 = new SummaryActivity(startLatlng: new LatLng(), endLatlng: new LatLng(), 
                type: ActivityType.Ride, movingTime: 60, athlete: new MetaAthlete(123));
            
            summary2.StartLatlng.Add(30.6F);
            summary2.StartLatlng.Add(40.6F);
            
            summary2.EndLatlng.Add(30.6F);
            summary2.EndLatlng.Add(40.6F);

            return summary2;
        }

        public static SummaryActivity SummaryActivity3()
        {

            var summary2 = new SummaryActivity(startLatlng: new LatLng(), endLatlng: new LatLng(),
                 type: ActivityType.Run, movingTime: 100, athlete: new MetaAthlete(123));

            summary2.StartLatlng.Add(30.6F);
            summary2.StartLatlng.Add(40.6F);

            summary2.EndLatlng.Add(30.6F);
            summary2.EndLatlng.Add(40.6F);

            return summary2;
        }
    }
}
