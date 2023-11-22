using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

#region PersonClass
namespace HealthAndFitnessTracker
{
    public class Person
    {
        #region Properties
        [Key]
        public int PersonId { get; set; }
        public string? Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PersonName { get; set; } = string.Empty;
        public int PersonAge { get; set; }

        public string CommonActivityType = string.Empty;

        #endregion
        #region Lists
        public List<BodyMeasurements> BodyMeasurements { get; set; } = new List<BodyMeasurements>();
        public List<FoodLog> FoodLogs { get; set; } = new List<FoodLog>();
        public List<Workout> Workouts { get; set; } = new List<Workout>();
        #endregion
        #region Dictionary
        public static Dictionary<Person, (List<BodyMeasurements>, List<FoodLog>, List<Workout>)> PersonDictionary { get; set; } = new();
        #endregion
        #region Constructors

        // The Person class handles data related to a person, including body measurements, food logs, and workouts.
        // This constructor initializes properties for a person, body measurements, food logs, and workouts.
        public Person()
        {

            BodyMeasurements = new List<BodyMeasurements>();
            FoodLogs = new List<FoodLog>();
            Workouts = new List<Workout>();
            PersonDictionary[this] = (BodyMeasurements, FoodLogs, Workouts);
        }
        public Person(string name, int age, string username, string password)
        {
            this.PersonName = name;
            this.PersonAge = age;
            this.Username = username;
            this.Password = password;
            BodyMeasurements = new List<BodyMeasurements>();
            FoodLogs = new List<FoodLog>();
            Workouts = new List<Workout>();
            PersonDictionary[this] = (BodyMeasurements, FoodLogs, Workouts);
        }
        public Person(string name, int age, string username, string password, BodyMeasurements bodyMeasurement, FoodLog foodLog, Workout workout)
        {

            this.PersonName = name;
            this.PersonAge = age;
            this.Username = username;
            this.Password = password;

            BodyMeasurements = new List<BodyMeasurements> { bodyMeasurement };
            FoodLogs = new List<FoodLog> { foodLog };
            Workouts = new List<Workout> { workout };
            PersonDictionary[this] = (BodyMeasurements, FoodLogs, Workouts);
        }
        #endregion
        #region Functions

        /// <summary>
        /// Calculates the total calories burned from all workouts.
        /// </summary>
        /// <returns>The total calories burned.</returns>
        public double TotalCaloriesBurned()
        {
            double total = 0;
            foreach (var item in this.Workouts)
            {
                total += item.CaloriesBurned;
            }
            return total;
        }






        /// <summary>
        /// Calculates the average workout time based on the total duration and the number of workouts.
        /// </summary>
        /// <returns>The average workout time as a TimeSpan.</returns>
        public TimeSpan AveWorkoutTime()
        {
            TimeSpan duration = new(0, 0, 0);
            foreach (var item in this.Workouts)
            {
                duration += item.Duration;

            }
            return duration / Workouts.Count;
        }







        /// <summary>
        /// Determines the most common workout type based on the number of occurrences.
        /// </summary>
        /// <returns>The most common workout type as a string. If no workouts are recorded, it returns "No workouts recorded."</returns>
        public string MostCommonWorkoutType()
        {
            Dictionary<string, int> workoutTypeCounts = new();

            foreach (var workout in Workouts)
            {
                if (workoutTypeCounts.ContainsKey(workout.WorkoutType))
                {
                    workoutTypeCounts[workout.WorkoutType]++;
                }
                else
                {
                    workoutTypeCounts[workout.WorkoutType] = 1;
                }
            }

            if (workoutTypeCounts.Count == 0)
            {
                return "No workouts recorded";
            }

            var mostCommonType = workoutTypeCounts.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            return mostCommonType;
        }

