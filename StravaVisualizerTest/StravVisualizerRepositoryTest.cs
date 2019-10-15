using IO.Swagger.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StravaVisualizer.Models.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using StravaVisualizer.Data;
using Microsoft.EntityFrameworkCore;
using StravaVisualizerTest.Doubles;

namespace StravaVisualizerTest
{   
    [TestClass]
    public class StravVisualizerRepositoryTest
    {
        List<VisualActivity> visualActivities;
        IQueryable<StravaUser> users;
        IStravaVisualizerDbContext userActivityDbContext;
        IStravaVisualizerRepository userActivityRepository;

        [TestInitialize]
        public void Setup()
        {
            visualActivities = (List<VisualActivity>) TestData.VisualActivitiesList();

            users = new List<StravaUser>
            {
                new StravaUser {VisualActivities = visualActivities, UserId = 1, LastDownload = DateTime.Now},
                new StravaUser {VisualActivities = visualActivities, UserId = 2, LastDownload = DateTime.Now},
                new StravaUser {VisualActivities = visualActivities, UserId = 3, LastDownload = DateTime.Now},                

            }.AsQueryable();

            visualActivities[0].Summary.StartLatlng.Add(30.0F);
            visualActivities[0].Summary.StartLatlng.Add(40.0F);
            visualActivities[1].Summary.StartLatlng.Add(30.6F);
            visualActivities[1].Summary.StartLatlng.Add(40.6F);
            
            var userMockSet = Substitute.For<DbSet<StravaUser>, IQueryable<StravaUser>>();
            ((IQueryable<StravaUser>)userMockSet).Provider.Returns(users.Provider);
            ((IQueryable<StravaUser>)userMockSet).Expression.Returns(users.Expression);
            ((IQueryable<StravaUser>)userMockSet).ElementType.Returns(users.ElementType);
            ((IQueryable<StravaUser>)userMockSet).GetEnumerator().Returns(users.GetEnumerator());                          
            userActivityDbContext = Substitute.For<IStravaVisualizerDbContext>();
            userActivityDbContext.StravaUsers.Returns(userMockSet);

            var mockActivities = visualActivities.AsQueryable();
            var activityMockSet = Substitute.For<DbSet<VisualActivity>, IQueryable<VisualActivity>>();
            ((IQueryable<VisualActivity>)activityMockSet).Provider.Returns(mockActivities.Provider);
            ((IQueryable<VisualActivity>)activityMockSet).Expression.Returns(mockActivities.Expression);
            ((IQueryable<VisualActivity>)activityMockSet).ElementType.Returns(mockActivities.ElementType);
            ((IQueryable<VisualActivity>)activityMockSet).GetEnumerator().Returns(mockActivities.GetEnumerator());
            ((IQueryable<VisualActivity>)activityMockSet).Contains(visualActivities[0]).Returns(true);
            userActivityDbContext.VisualActivities.Returns(activityMockSet);

            userActivityRepository = new StravaVisualizerRepository(userActivityDbContext);
        }

        [TestMethod]
        public void Test_GetUserActivities()
        {
            var result = userActivityRepository.GetUserActivities();

            Assert.AreEqual(1, result.ElementAt(0).UserId);
        }

        [TestMethod]
        public void Test_GetUserActivitiesById()
        {
            var result = userActivityRepository.GetStravaUserById(2);

            Assert.AreEqual(2, result.UserId);
        }
        
        [TestMethod]
        public void Test_AddNewUserActivity()
        {
            //var newUserActivity = new UserActivity()
            //{
            //    Activities = new List<SummaryActivity>(),
            //    UserId = 1,
            //    LastDownload = DateTime.Now.Date
            //};

            //userActivityRepository.Add(newUserActivity);
            //var dbSet = userActivityRepository.GetUserActivities();

            //Assert.AreEqual(4, dbSet.Count());

            //Assert.ThrowsException<ArgumentNullException>(() => userActivityRepository.Add(null));
        }
    }
}
