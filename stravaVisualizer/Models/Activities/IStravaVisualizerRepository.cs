using IO.Swagger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StravaVisualizer.Models.Activities
{
    public interface IStravaVisualizerRepository
    {
        IQueryable<StravaUser> GetUserActivities();
        StravaUser GetStravaUserById(int id);
        void Add<T>(T entity) where T : class;
        void SaveChanges();
        Task SaveChangesAsync();

    }
}
