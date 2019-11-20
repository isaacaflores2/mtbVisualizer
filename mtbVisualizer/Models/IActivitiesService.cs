using MtbVisualizer.Models.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mtbVisualizer.Models
{
    interface IActivitiesService
    {
        StravaUser getUpdatedUserActivities();
    }
}
