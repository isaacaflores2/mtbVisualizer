using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IO.Swagger.Model;
using Microsoft.EntityFrameworkCore;
using StravaVisualizer.Data;

namespace StravaVisualizer.Models.Activities
{
    public class StravaVisualizerRepository: IStravaVisualizerRepository
    {
        private readonly IStravaVisualizerDbContext stravaVisualizerDbContext;

        public StravaVisualizerRepository(IStravaVisualizerDbContext stravaVisualizerDbContext)
        {
            this.stravaVisualizerDbContext = stravaVisualizerDbContext;
        }

        
        public IQueryable<StravaUser> GetUserActivities()
        {
            return stravaVisualizerDbContext.StravaUsers;

        }

        public StravaUser GetStravaUserById(int id)
        {
            //var userActivityForId = (from user in stravaVisualizerDbContext.StravaUsers
            //                        where user.UserId == id
            //                        select user).FirstOrDefault();

            var userActivityForId = stravaVisualizerDbContext.StravaUsers
                .Where(user => user.UserId == id)
                .Include(user => user.VisualActivities)
                .FirstOrDefault();

            return userActivityForId;
        }
        

        public void SaveChanges()
        {
            stravaVisualizerDbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await stravaVisualizerDbContext.SaveChangesAsync();
        }

        public void Add<T>(T entity) where T : class
        {
            stravaVisualizerDbContext.Add<T>(entity);
        }
    }
}
