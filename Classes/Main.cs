using Microsoft.EntityFrameworkCore;

namespace HealthAndFitnessTracker.Classes
{
    internal class MainClass
    {
        static void Main()
        {

            #region DatabaseTablesandDataInsertionCommands
            FitnessDbContext db = new();

            db.Workouts.ExecuteDelete();
            db.FoodLogs.ExecuteDelete();
            db.BodyMeasurements.ExecuteDelete();
            db.Persons.ExecuteDelete();
            db.Database.ExecuteSqlRaw("DELETE FROM sqlite_sequence WHERE name='Persons'");
            db.Database.ExecuteSqlRaw("DELETE FROM sqlite_sequence WHERE name='BodyMeasurements'");
            db.Database.ExecuteSqlRaw("DELETE FROM sqlite_sequence WHERE name='FoodLogs'");
            db.Database.ExecuteSqlRaw("DELETE FROM sqlite_sequence WHERE name='Workouts'");
            InsertDataIntoDatabase();
            #endregion


            var healthTracker = new HealthTracker(db);
            healthTracker.Start(db);


        }

        #region DatabaseInfo_ForTesting
        static void InsertDataIntoDatabase()
        {
            using FitnessDbContext db = new();

            Person person1 = new("John Nash", 30, "john.nash", "password123");
            person1.BodyMeasurements.Add(new BodyMeasurements
            {
                BodymeasurementDate = DateTime.Now,
                Weight = 75.5,
                Height = 175.0,
                BodyfatPercentage = 15.0
            });
            person1.BodyMeasurements.Add(new BodyMeasurements
            {
                BodymeasurementDate = DateTime.Now.AddDays(-10),
                Weight = 74.0,
                Height = 174.5,
                BodyfatPercentage = 14.5
            });

            person1.FoodLogs.Add(new FoodLog
            {
                FoodlogDate = DateTime.Now,
                MealType = "Lunch",
                FoodItem = "Chicken Salad",
                FoodCalories = 500.0
            });
            person1.FoodLogs.Add(new FoodLog
            {
                FoodlogDate = DateTime.Now.AddDays(-10),
                MealType = "Dinner",
                FoodItem = "Grilled Salmon",
                FoodCalories = 600.0
            });

            person1.Workouts.Add(new Workout
            {
                WorkoutDate = DateTime.Now,
                WorkoutType = "Running",
                WorkoutStart = new TimeOnly(8, 0),
                WorkoutFinished = new TimeOnly(9, 0),
                CaloriesBurned = 300.0
            });
            person1.Workouts.Add(new Workout
            {
                WorkoutDate = DateTime.Now.AddDays(-10),
                WorkoutType = "Weightlifting",
                WorkoutStart = new TimeOnly(7, 30),
                WorkoutFinished = new TimeOnly(8, 30),
                CaloriesBurned = 250.0
            });

            Person person2 = new("Michael Johnson", 28, "michael.johnson", "pass123");
            person2.BodyMeasurements.Add(new BodyMeasurements
            {
                BodymeasurementDate = DateTime.Now.AddDays(-15),
                Weight = 78.2,
                Height = 180.0,
                BodyfatPercentage = 14.5
            });
            person2.BodyMeasurements.Add(new BodyMeasurements
            {
                BodymeasurementDate = DateTime.Now.AddDays(-20),
                Weight = 77.0,
                Height = 179.5,
                BodyfatPercentage = 14.0
            });

            person2.FoodLogs.Add(new FoodLog
            {
                FoodlogDate = DateTime.Now.AddDays(-15),
                MealType = "Dinner",
                FoodItem = "Grilled Salmon",
                FoodCalories = 400.0
            });
            person2.FoodLogs.Add(new FoodLog
            {
                FoodlogDate = DateTime.Now.AddDays(-20),
                MealType = "Lunch",
                FoodItem = "Avocado Salad",
                FoodCalories = 350.0
            });

            person2.Workouts.Add(new Workout
            {
                WorkoutDate = DateTime.Now.AddDays(-15),
                WorkoutType = "Weightlifting",
                WorkoutStart = new TimeOnly(7, 30),
                WorkoutFinished = new TimeOnly(8, 30),
                CaloriesBurned = 250.0
            });
            person2.Workouts.Add(new Workout
            {
                WorkoutDate = DateTime.Now.AddDays(-20),
                WorkoutType = "Running",
                WorkoutStart = new TimeOnly(6, 45),
                WorkoutFinished = new TimeOnly(7, 30),
                CaloriesBurned = 300.0
            });

            Person person3 = new("Emily Davis", 25, "emily.davis", "pass456");
            person3.BodyMeasurements.Add(new BodyMeasurements
            {
                BodymeasurementDate = DateTime.Now.AddDays(-20),
                Weight = 65.5,
                Height = 165.0,
                BodyfatPercentage = 20.0
            });
            person3.BodyMeasurements.Add(new BodyMeasurements
            {
                BodymeasurementDate = DateTime.Now.AddDays(-25),
                Weight = 64.0,
                Height = 164.5,
                BodyfatPercentage = 19.5
            });

            person3.FoodLogs.Add(new FoodLog
            {
                FoodlogDate = DateTime.Now.AddDays(-20),
                MealType = "Lunch",
                FoodItem = "Grilled Salmon",
                FoodCalories = 350.0
            });
            person3.FoodLogs.Add(new FoodLog
            {
                FoodlogDate = DateTime.Now.AddDays(-25),
                MealType = "Dinner",
                FoodItem = "Vegetarian Stir-Fry",
                FoodCalories = 450.0
            });

            person3.Workouts.Add(new Workout
            {
                WorkoutDate = DateTime.Now.AddDays(-20),
                WorkoutType = "Running",
                WorkoutStart = new TimeOnly(6, 45),
                WorkoutFinished = new TimeOnly(7, 30),
                CaloriesBurned = 300.0
            });
            person3.Workouts.Add(new Workout
            {
                WorkoutDate = DateTime.Now.AddDays(-25),
                WorkoutType = "Yoga",
                WorkoutStart = new TimeOnly(18, 0),
                WorkoutFinished = new TimeOnly(19, 0),
                CaloriesBurned = 150.0
            });

            db.Persons.Add(person1);
            db.Persons.Add(person2);
            db.Persons.Add(person3);
            db.SaveChanges();


        }
        #endregion
    }
}