using StravaVisualizer.Models.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StravaVisualizer.Models.MonthSummary
{
    public class MonthSummary
    {
        public int Month { get; set; }
        public DateTime WeekStartDate { get; set; }
        public ICollection<VisualActivity> Activites { get; set; }


        public MonthSummary(DateTime today, IList<VisualActivity> visualActivities)
        {
            Month = today.Month;
            WeekStartDate = getCurrentWeekStartDate(today);
            Activites = new List<VisualActivity>();

            getActivitiesForThisMonth(visualActivities);
        }

        public int getCurrentMonth()
        {
            return Month;
        }

        public static DateTime getCurrentWeekStartDate(DateTime today)
        {
            var todayDay = today.Day;
            var dayOfWeek = (int)today.DayOfWeek;
            var date = todayDay - (dayOfWeek - 1);
            return new DateTime(today.Year, today.Month, date);
        }

        public void getActivitiesForThisMonth(ICollection<VisualActivity> visualActivities)
        {
            Activites = (from activity in visualActivities
                         where activity.Summary.StartDate.Value.Month == Month &&
                         activity.Summary.StartDate.Value.Year == WeekStartDate.Year
                         select activity).ToList();
        }
    }
}
