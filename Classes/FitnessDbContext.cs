using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;

#region FitnessDatabase
namespace HealthAndFitnessTracker.Classes
{
    public class FitnessDbContext : DbContext
    {
        #region Tables
        public DbSet<Person> Persons { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<FoodLog> FoodLogs { get; set; }
        public DbSet<BodyMeasurements> BodyMeasurements { get; set; }
        #endregion

        #region DatabasePath
        public string DbPath { get; private set; }
        #endregion

        #region Constructor
        public FitnessDbContext()
        {
            var folder = Environment.SpecialFolder.Desktop;
            string path = Environment.GetFolderPath(folder);
            DbPath = Path.Combine(path, "Fitness.db");
        }
        #endregion

        #region DbBuilder
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }
        #endregion
    }
}
#endregion