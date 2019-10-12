using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoCoordinatePortable;
using Newtonsoft.Json;
using System.Diagnostics;
using IO.Swagger.Client;
using IO.Swagger.Api;
using IO.Swagger.Model;
using StravaVisualizer.Models.Activities;

namespace StravaVisualizer.Models
{
    public class StravaClient: IStravaClient
    {
        private readonly IActivitiesApi _activitiesApi;

        private readonly IAthletesApi _athletesApi;

        public StravaClient(IActivitiesApi activitiesApi, IAthletesApi athletesApi)
        {
            this._activitiesApi = activitiesApi;
            this._athletesApi = athletesApi;
        }

        public IEnumerable<VisualActivity> getAllUserActivities(string accessToken, int id)
        {
            var summaryActivities =  requestAllUserActivitiesAsync(accessToken, id).Result;
            List<VisualActivity> visualActivites = new List<VisualActivity>();
            foreach(var summary in summaryActivities)
            {
                visualActivites.Add(new VisualActivity(summary));
            }
            return visualActivites.AsEnumerable(); 
        }

        public async Task<IEnumerable<SummaryActivity>> requestAllUserActivitiesAsync(string accessToken, int id)
        {           
            Configuration.Default.AccessToken = accessToken;

            try
            {
                var athleteStats = await _athletesApi.GetStatsAsync(id);
                int totalActivites = calcTotalActivityCount(athleteStats);
                return  await requestActivities(totalActivites);                             
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ActivitiesApi.getLoggedInAthleteActivities: " + e.Message);
            }
            return null;
        }

        private int calcTotalActivityCount(ActivityStats athleteStats)
        {            
            int total = athleteStats.AllRideTotals.Count.Value;
            total += athleteStats.AllRunTotals.Count.Value;
            total += athleteStats.AllSwimTotals.Count.Value;
            
            return total;
        }

        public async Task<IEnumerable<SummaryActivity>> requestActivities( int total)
        {
            List<SummaryActivity> activities = new List<SummaryActivity>();

            //int requiredPages = (int) Math.Ceiling(total /30.0);
            //for(int i = 1; i <= requiredPages; i++)
            //{
            //    var activitesPage =  await _activitiesApi.GetLoggedInAthleteActivitiesAsync(page: i);                
            //    activities.AddRange(activitesPage);
            //}

            int i = 0;
            while (true)
            {
                var activitesPage = await _activitiesApi.GetLoggedInAthleteActivitiesAsync(page: i);
                if(activitesPage == null )
                {
                    break;
                }

                activities.AddRange(activitesPage);
                i++;
            }


            return activities;
        }
       
        public IEnumerable<VisualActivity> getUserActivitiesAfter(string accessToken, StravaUser stravaUser, DateTime afterDate)
        {
            List<VisualActivity> visualActivites = new List<VisualActivity>();

            if(stravaUser.VisualActivities == null)
            {
                return visualActivites;
            }

            var summaryActivities =  requestActivitiesAfterAsync(accessToken, stravaUser, afterDate).Result;

            if (summaryActivities == null)
            {
                return null;
            }

            foreach (var summary in summaryActivities)
            {
                visualActivites.Add(new VisualActivity(summary));
            }
                        
            return visualActivites.AsEnumerable();
        }
    
        public async Task<IEnumerable<SummaryActivity>> requestActivitiesAfterAsync(string accessToken, StravaUser stravaUser, DateTime afterDate)
        {
            Configuration.Default.AccessToken = accessToken;
            var athleteStats = await _athletesApi.GetStatsAsync(stravaUser.UserId);
            int totalActivites = calcTotalActivityCount(athleteStats);
            int requiredActivities = getActivityDifferenceSinceLastDownload(stravaUser, totalActivites);

            try
            {
                return await requestActivitiesAfter(requiredActivities, afterDate);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ActivitiesApi.getLoggedInAthleteActivities: " + e.Message);
            }
            return null;
        }

        private int getActivityDifferenceSinceLastDownload(StravaUser stravaUser, int totalActivities)
        {
            int activitesDownloaded = stravaUser.VisualActivities.Count();
            return totalActivities - activitesDownloaded;
        }

        public async Task<IEnumerable<SummaryActivity>> requestActivitiesAfter(int requiredActivities, DateTime afterDate)
        {
            List<SummaryActivity> activities = new List<SummaryActivity>();
            DateTimeOffset dto = new DateTimeOffset(afterDate);
            var afterDateEpoch = (int)dto.ToUnixTimeSeconds();
            int requiredPages = (int)Math.Ceiling(requiredActivities / 30.0);

            for (int i = 0; i <= requiredPages; i= activities.Count())
            {
                var activitesPage = await _activitiesApi.GetLoggedInAthleteActivitiesAsync(page: i, after: afterDateEpoch);
                activities.AddRange(activitesPage);
            }
            return activities;
        }
              
    }
}
