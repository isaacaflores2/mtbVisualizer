using IO.Swagger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoCoordinatePortable;
using StravaVisualizer.Models.Map;
using StravaVisualizer.Models.Activities;

namespace StravaVisualizer.Models.Map
{
    public class Map : IMap
    {
        public IEnumerable<VisualActivity> VisualActivities { get; set; }
        public ICollection<Coordinate> Coordinates { get; set;}
        public ICollection<Pin> Pins { get; set; }
        public IDictionary<Coordinate, int> NumVisits { get; set; }

        public Map()
        {
            VisualActivities = null;
            Coordinates = new List<Coordinate>();
            Pins = new List<Pin>();
            NumVisits = new Dictionary<Coordinate, int>();
        }

        public ICollection<Coordinate> getCoordinates(IEnumerable<VisualActivity> activities)
        {
            VisualActivities = activities ?? throw new ArgumentNullException("Activities is null");
            extractCoordinates();
            return Coordinates;
        }

        public void extractCoordinates()
        {            
            foreach(var visualActivity in VisualActivities)
            {
                if (visualActivity == null || visualActivity.StartLat == null || visualActivity.StartLong == null)
                    continue;          
                
                addCoordinate(visualActivity);                
            }
        }

        public void addCoordinate(VisualActivity visualActivity)
        {   
            Coordinates.Add(new Coordinate(
                visualActivity.StartLat,
                visualActivity.StartLong));
        }

        public ICollection<Coordinate> getCoordinatesByType(IEnumerable<VisualActivity> activities, ActivityType type)
        {
            if (activities == null)
                throw new ArgumentNullException("Activities is null");

            var activitiesForType = from activity in activities
                                    where activity.Summary.Type == type
                                    select activity;

            //var activitiesForType = activities
            //    .Where(a => a.Summary.Type == type)
            //    .Select(a_type => { return a_type; });

            VisualActivities = activitiesForType;
            extractCoordinates();
            return Coordinates;
        }

        public void getUniqueCoordinatesByType(IEnumerable<VisualActivity> activities, ActivityType type)
        {
            if (activities == null)
                throw new ArgumentNullException("Activities is null");

            var activitiesForType = from activity in activities
                                   where activity.Summary.Type == type
                                   select activity;
            VisualActivities = activitiesForType;

            foreach (var visualActivity in VisualActivities)
            {
                if (visualActivity == null || visualActivity.StartLat == null || visualActivity.StartLong == null)
                    continue;

                updateNumVisits(visualActivity);
                addCoordinate(visualActivity);
            }

            foreach(var coordinate in NumVisits.Keys)
            {
                Pin pin = new Pin();
                pin.Location = coordinate;
                pin.Text = Convert.ToString(NumVisits[coordinate]);
                Pins.Add(pin);
            }
        }

        public void updateNumVisits(VisualActivity visualActivity)
        {
            float?[] latlongArray = visualActivity.Summary.StartLatlng.ToArray();                        
            var coordinate = new Coordinate(latlongArray[0], latlongArray[1]);
            
            if (NumVisits.ContainsKey(coordinate))
            {
                NumVisits.TryGetValue(coordinate, out var count);
                count++;
                NumVisits[coordinate] = count;
            }
            else
            {
                NumVisits.Add(coordinate, 1);
            }
        }

       
    }
}
