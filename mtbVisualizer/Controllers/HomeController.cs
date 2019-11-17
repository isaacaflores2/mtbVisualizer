using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MtbVisualizer.Models;
using MtbVisualizer.Data;
using MtbVisualizer.Models;
using MtbVisualizer.Models.Activities;
using MtbVisualizer.Models.MonthSummary;
using Microsoft.Extensions.Logging;

namespace MtbVisualizer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStravaVisualizerRepository _context;
        private readonly IStravaClient _stravaClient;
        private readonly IHttpContextHelper _httpContextHelper;
        private readonly ILogger _logger; 

        public HomeController(IHttpContextHelper httpContextHelper, IStravaClient stravaClient, IStravaVisualizerRepository context, ILogger<HomeController> logger)
        {
            _httpContextHelper = httpContextHelper;
            _stravaClient = stravaClient;
            _context = context;
            _logger = logger; 
        }

        public IActionResult Index()
        {            
            return View("Index");                        
        }

        public PartialViewResult LoadCalendarPartial(DateTime date)
        {
            if (!User.Identity.IsAuthenticated)
            {
                MonthSummary exampleMonthSummary = ExampleData.GetMonthSummary();
                return PartialView("_CalendarPartial", exampleMonthSummary);
            }

            _httpContextHelper.Context = HttpContext;
            string accessToken = _httpContextHelper.getAccessToken();
            int stravaId = Convert.ToInt32(User.FindFirst("stravaId").Value);
            var user = getUpdatedUserActivities(accessToken, stravaId);

            MonthSummary monthSummary = new MonthSummary(date, user.VisualActivities.ToList());

            return PartialView("_CalendarPartial", monthSummary);
        }

        public PartialViewResult LoadTablePartial(DateTime date)
        {
            if (!User.Identity.IsAuthenticated)
            {
                MonthSummary exampleMonthSummary = ExampleData.GetMonthSummary();
                IList<VisualActivity> exampleActivities = exampleMonthSummary.getActivitiesForThisWeek(exampleMonthSummary.Activites);
                return PartialView("_TablePartial", exampleActivities);
            }

            _httpContextHelper.Context = HttpContext;
            string accessToken = _httpContextHelper.getAccessToken();
            int stravaId = Convert.ToInt32(User.FindFirst("stravaId").Value);
            var user = getUpdatedUserActivities(accessToken, stravaId);

            MonthSummary monthSummary = new MonthSummary(date, user.VisualActivities.ToList());
            IList<VisualActivity> activities = monthSummary.getActivitiesForThisWeek(monthSummary.Activites);

            return PartialView("_TablePartial", activities);
        }

        public IActionResult Privacy()
        {
            return View("Privacy");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private StravaUser getUpdatedUserActivities(string accessToken, int id)
        {
            var user = _context.GetStravaUserById(id);

            if (user == null || user.VisualActivities == null || user.VisualActivities.Count == 0)
            {
                logUserActivity(id, "New user. Requeusting all activities from strava. ");
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
                var latestActivities = _stravaClient.getUserActivitiesAfter(accessToken, user, user.LastDownload);
                logUserActivity(id, "Requeusting latest activities from strava. ");

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

        private void logUserActivity(int stravaId, string msg)
        {
            _logger.LogInformation($"{stravaId} : {msg}");
        }
    }
}