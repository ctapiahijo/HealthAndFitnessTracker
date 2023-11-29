using HealthAndFitnessTracker.Classes;

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

    }

}