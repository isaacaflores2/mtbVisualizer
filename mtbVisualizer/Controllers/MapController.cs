using IO.Swagger.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MtbVisualizer.Data;
using MtbVisualizer.Models;
using MtbVisualizer.Models.Activities;
using MtbVisualizer.Models.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtbVisualizer.Controllers
{
    
    public class MapController : Controller
    {
        private readonly IHttpContextHelper _httpContextHelper;
        private readonly IStravaClient _stravaClient;
        private readonly IMap _map;
        private readonly IStravaVisualizerRepository _context;
        private readonly ILogger _logger;

        public IDictionary<string, string> AuthProperties { get; set; }

        public MapController(IHttpContextHelper httpContextHelper, IStravaClient stravaClient, IMap map, IStravaVisualizerRepository userActivityRepository, ILogger<MapController> logger) 
        {
            this._httpContextHelper = httpContextHelper;
            this._stravaClient = stravaClient;
            this._map = map;
            this._context = userActivityRepository;
            this._logger = logger; 
        }

        public IActionResult Index()
        {                
            return View("Index");
        }
    
        public PartialViewResult LoadMapPartial()
        {
            if (!User.Identity.IsAuthenticated)
            {
                var exampleCoordinates = _map.getCoordinates(ExampleData.MapVisualActivitiesList());
                return PartialView("_BingMapPartial", exampleCoordinates);
            }

            _httpContextHelper.Context = HttpContext;
            string accessToken = _httpContextHelper.getAccessToken();
            int stravaId = Convert.ToInt32(User.FindFirst("stravaId").Value);
            var user = getUpdatedUserActivities(accessToken, stravaId);
            
            var coordinates = _map.getCoordinates(user.VisualActivities);         
            return PartialView("_BingMapPartial", coordinates);
        }

        public PartialViewResult LoadMapByTypePartial(String type)
        {

            Enum.TryParse(type, out ActivityType activityType);

            if (!User.Identity.IsAuthenticated)
            {                
                var exampleCoordinates = _map.getCoordinatesByType(ExampleData.MapVisualActivitiesList(), activityType);
                return PartialView("_BingMapPartial", exampleCoordinates);
            }

            _httpContextHelper.Context = HttpContext;
            string accessToken = _httpContextHelper.getAccessToken();
            int stravaId = Convert.ToInt32(User.FindFirst("stravaId").Value);
            var user = getUpdatedUserActivities(accessToken, stravaId);
            
            var coordinates = _map.getCoordinatesByType(user.VisualActivities, activityType);
            return PartialView("_BingMapPartial", coordinates);
        }
   
        private StravaUser getUpdatedUserActivities(string accessToken, int id)
        {
            var user = _context.GetStravaUserById(id);

            if (user == null || user.VisualActivities == null || user.VisualActivities.Count == 0)
            {
                var activities = _stravaClient.getAllUserActivities(accessToken, id);
                user = new StravaUser()
                {
                    VisualActivities = activities.ToList(),
                    UserId = id,
                    LastDownload = DateTime.Now.Date
                };
                _context.Add(user);
                _context.SaveChanges();
            }
            else
            {
                var lastDownloadDate = new DateTime(2019, 9, 29);
                var latestActivities = _stravaClient.getUserActivitiesAfter(accessToken, user, user.LastDownload);

                if (latestActivities != null)
                {
                    foreach (var activity in latestActivities)
                    {
                        if (!_context.Contains(activity))
                        {
                            _context.Add(activity);
                            user.VisualActivities.Add(activity);
                        }
                    }
                    _context.SaveChanges();
                }
            }
            return user; 
        } 
    }
}