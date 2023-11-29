using HealthAndFitnessTracker.Classes;
using Xunit;

namespace XXUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void TotalCaloriesConsumed_ShouldCalculateCorrectly()
        {
            // Arrange
            var foodLogs = new List<FoodLog>
        {
            new FoodLog { FoodlogDate = DateTime.Now.Date, FoodCalories = 500.0 },
            new FoodLog { FoodlogDate = DateTime.Now.Date, FoodCalories = 300.0 },

        };

            var healthTracker = new Person();
            healthTracker.FoodLogs = foodLogs;

            // Act
            var result = healthTracker.TotalCaloriesConsumed(DateTime.Now.Date);

            // Assert
            Assert.Equal(800.0, result);
        }

        [Fact]
        public void TotalCaloriesBurned_ShouldCalculateCorrectly()
        {
            // Arrange
            var workouts = new List<Workout>
            {
                new Workout { CaloriesBurned = 200.0 },
                new Workout { CaloriesBurned = 300.0 },

            };

            var healthTracker = new Person();
            healthTracker.Workouts = workouts;

            // Act
            var result = healthTracker.TotalCaloriesBurned();

            // Assert
            Assert.Equal(500.0, result);
        }

        [Fact]
        public void AveWorkoutTime_ShouldCalculateCorrectly()
        {
            // Arrange
            var workouts = new List<Workout>
            {
                new Workout { Duration = TimeSpan.FromMinutes(30) },
                new Workout { Duration = TimeSpan.FromMinutes(45) },

            };

            var healthTracker = new Person();
            healthTracker.Workouts = workouts;

            // Act
            var result = healthTracker.AveWorkoutTime();

            // Assert
            Assert.Equal(TimeSpan.FromMinutes(37.5), result);
        }

        [Fact]
        public void MostCommonWorkoutType_ShouldReturnCorrectResult()
        {
            // Arrange
            var workouts = new List<Workout>
            {
                new Workout { WorkoutType = "Running" },
                new Workout { WorkoutType = "Weightlifting" },
                new Workout { WorkoutType = "Running" },
                new Workout { WorkoutType = "Yoga" },

            };

            var healthTracker = new Person();
            healthTracker.Workouts = workouts;

            // Act
            var result = healthTracker.MostCommonWorkoutType();

            // Assert
            Assert.Equal("Running", result);
        }

    }

}