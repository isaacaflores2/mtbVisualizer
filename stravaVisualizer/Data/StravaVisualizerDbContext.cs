﻿using IO.Swagger.Model;
using Microsoft.EntityFrameworkCore;
using StravaVisualizer.Data;
using StravaVisualizer.Models;
using StravaVisualizer.Models.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stravaVisualizer.Data
{   
    public class StravaVisualizerDbContext : DbContext, IStravaVisualizerDbContext
    {        
        public DbSet<StravaUser> StravaUsers { get; set; }

        public DbSet<VisualActivity> VisualActivities { get; set; }

        public StravaVisualizerDbContext(DbContextOptions<StravaVisualizerDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override  void OnModelCreating(ModelBuilder modelBuilder)
        {                     
            modelBuilder.Entity<SummaryActivity>()
                .Ignore(s => s.StartLatlng);
            
            modelBuilder.Entity<SummaryActivity>()
                .Ignore(s => s.EndLatlng);
         
            modelBuilder.Entity<VisualActivity>(v =>
            {
                v.OwnsOne(p => p.Summary);
            });
        }

        public void SaveChanges()
        {
            this.SaveChanges();
        }

        void IStravaVisualizerDbContext.Add<T>(T entity)
        {
            Add<T>(entity);
        }
    }
}
