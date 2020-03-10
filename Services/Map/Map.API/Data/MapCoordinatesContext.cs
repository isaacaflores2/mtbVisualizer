﻿using System.Threading.Tasks;
using Map.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Map.API.Data
{
    public class MapCoordinatesContext : DbContext
    {
        public MapCoordinatesContext() : base()
        {
        }

        public MapCoordinatesContext(DbContextOptions<MapCoordinatesContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Coordinates> StartCoordinates { get; set; }

        public void SaveChanges()
        {
            SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await SaveChangesAsync();
        }

        public void Add<T>(T entity)
        {
            Add<T>(entity);
        }
    }
}