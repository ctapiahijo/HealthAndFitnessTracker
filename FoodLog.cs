using System.ComponentModel.DataAnnotations;


#region FoodLogClass
namespace HealthAndFitnessTracker
{
    public class FoodLog
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

            this.FoodlogDate = foodlogDate;
            this.MealType = mealType;
            this.FoodItem = foodItem;
            this.FoodCalories = foodCalories;
            this.PersonId = PersonId;
        }
        #endregion
    }
}
#endregion