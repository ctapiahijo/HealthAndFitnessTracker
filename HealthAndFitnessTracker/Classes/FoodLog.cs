using System.ComponentModel.DataAnnotations;
using HealthAndFitnessTracker.Interfaces;

#region FoodLogClass
namespace HealthAndFitnessTracker.Classes
{
    public class FoodLog : IPersonId
    {
        #region Properties
        [Key]
        public int FoodlogId { get; set; }
        public DateTime FoodlogDate { get; set; }
        public string MealType { get; set; } = string.Empty;
        public string FoodItem { get; set; } = string.Empty;
        public double FoodCalories { get; set; }
        public int PersonId { get; set; }
        #endregion

        #region Constructors
        public FoodLog() { }
        public FoodLog(DateTime foodlogDate, string mealType, string foodItem, double foodCalories, int PersonId)
        {

            FoodlogDate = foodlogDate;
            MealType = mealType;
            FoodItem = foodItem;
            FoodCalories = foodCalories;
            this.PersonId = PersonId;
        }
        #endregion
    }
}
#endregion