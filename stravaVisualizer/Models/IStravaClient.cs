using IO.Swagger.Model;
using StravaVisualizer.Models.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StravaVisualizer.Models
{
    public interface IStravaClient
    {
        IEnumerable<VisualActivity> getAllUserActivities(string accessToken, int id);
        IEnumerable<VisualActivity> getUserActivitiesAfter(string accessToken, StravaUser stravaUser, DateTime afterDate);
    }
}
