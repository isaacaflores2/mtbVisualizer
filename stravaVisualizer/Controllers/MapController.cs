using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using stravaVisualizer.Data;
using stravaVisualizer.Models;
using GeoCoordinatePortable;
using StravaVisualizer.Models;
using IO.Swagger.Model;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using StravaVisualizer.Models.Map;
using StravaVisualizer.Models.Activities;

namespace stravaVisualizer.Controllers
{   
    [Authorize]
    public class MapController : Controller
    {
        private readonly IHttpContextHelper _httpContextHelper;
        private readonly IStravaClient _stravaClient;
        private readonly IMap _map;
        private readonly IStravaVisualizerRepository context;
         
        public IDictionary<string, string> AuthProperties { get; set; }

        public MapController(IHttpContextHelper httpContextHelper, IStravaClient stravaClient, IMap map, IStravaVisualizerRepository userActivityRepository) 
        {
            this._httpContextHelper = httpContextHelper;
            this._stravaClient = stravaClient;
            this._map = map;
            this.context = userActivityRepository;
        }

        public IActionResult Index()
        {                
            return View("MapAsync");
        }
    
        public PartialViewResult LoadMap()
        {
            _httpContextHelper.Context = HttpContext;
            string accessToken = _httpContextHelper.getAccessToken();
            int stravaId = Convert.ToInt32(User.FindFirst("stravaId").Value);
            var user = context.GetStravaUserById(stravaId);

            if (user == null || user.VisualActivities == null || user.VisualActivities.Count == 0)
            {
                var activities = _stravaClient.getAllUserActivities(accessToken, stravaId);
                user = new StravaUser()
                {
                    VisualActivities = activities.ToList(),
                    UserId = stravaId,
                    LastDownload = DateTime.Now.Date
                };
                context.Add(user);
                context.SaveChanges();
            }
            else
            {
                var lastDownloadDate = new DateTime(2019, 9, 29);
                var lastestActivities = _stravaClient.getUserActivitiesAfter(accessToken, user, user.LastDownload);
                                
                foreach (var activity in lastestActivities)
                {
                    if (!context.Contains(activity))
                    {
                        context.Add(activity);
                        user.VisualActivities.Add(activity);
                    }
                }
                context.SaveChanges();
            }
            
            var coordinates = _map.getCoordinates(user.VisualActivities);         
            return PartialView("_BingMapPartial", coordinates);
        }

        public PartialViewResult LoadMapByType(ActivityType type)
        {
            _httpContextHelper.Context = HttpContext;
            string accessToken = _httpContextHelper.getAccessToken();
            int stravaId = Convert.ToInt32(User.FindFirst("stravaId").Value);
            var user = getUpdatedUserActivities(accessToken, stravaId);
            
            var coordinates = _map.getCoordinatesByType(user.VisualActivities, type);

            return PartialView("_BingMapPartial", coordinates);
        }
   
        private StravaUser getUpdatedUserActivities(string accessToken, int id)
        {
            var user = context.GetStravaUserById(id);

            if (user == null || user.VisualActivities == null || user.VisualActivities.Count == 0)
            {
                var activities = _stravaClient.getAllUserActivities(accessToken, id);
                user = new StravaUser()
                {
                    VisualActivities = activities.ToList(),
                    UserId = id,
                    LastDownload = DateTime.Now.Date
                };
                context.Add(user);
                context.SaveChanges();
            }
            else
            {
                var lastDownloadDate = new DateTime(2019, 9, 29);
                var lastestActivities = _stravaClient.getUserActivitiesAfter(accessToken, user, user.LastDownload);

                foreach (var activity in lastestActivities)
                {
                    if (!context.Contains(activity))
                    {
                        context.Add(activity);
                        user.VisualActivities.Add(activity);
                    }
                }
                context.SaveChanges();
            }
            return user; 
        }

        private async Task<string> getAccessToken()
        {                                   
            return await HttpContext.GetTokenAsync("access_token");
        }
                
    }
}