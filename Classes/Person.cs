using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

#region PersonClass
namespace HealthAndFitnessTracker.Classes
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
            PersonName = name;
            PersonAge = age;
            Username = username;
            Password = password;
            BodyMeasurements = new List<BodyMeasurements>();
            FoodLogs = new List<FoodLog>();
            Workouts = new List<Workout>();
            PersonDictionary[this] = (BodyMeasurements, FoodLogs, Workouts);
        }
        public Person(string name, int age, string username, string password, BodyMeasurements bodyMeasurement, FoodLog foodLog, Workout workout)
        {

            PersonName = name;
            PersonAge = age;
            Username = username;
            Password = password;

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
            foreach (var item in Workouts)
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
            foreach (var item in Workouts)
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







        /// <summary>
        /// Calculates the total calories consumed on a specific date.
        /// </summary>
        /// <param name="date">The date for which to calculate the total calories consumed.</param>
        /// <returns>The total calories consumed on the specified date as a double.</returns>
        public double TotalCaloriesConsumed(DateTime date)
        {
            return FoodLogs.Where(log => log.FoodlogDate == date).Sum(log => log.FoodCalories);
        }





        /// <summary>
        /// Calculates the total calories consumed from all food logs.
        /// </summary>
        /// <returns>The total calories consumed from all food logs as a double.</returns>
        public double TotalCaloriesConsumed()
        {
            return FoodLogs.Sum(log => log.FoodCalories);
        }





        /// <summary>
        /// Calculates the distribution of calories among different food items in the food logs.
        /// </summary>
        /// <returns>A dictionary where keys are food items and values are the total calories consumed for each food item.</returns>
        public Dictionary<string, double> CaloricDistribution()
        {
            Dictionary<string, double> distribution = new();

            foreach (var log in FoodLogs)
            {
                string foodItem = log.FoodItem;

                if (distribution.ContainsKey(foodItem))
                {
                    distribution[foodItem] += log.FoodCalories;
                }
                else
                {
                    distribution[foodItem] = log.FoodCalories;
                }
            }

            return distribution;
        }





        /// <summary>
        /// Identifies the most frequently consumed food item based on the total calories consumed.
        /// </summary>
        /// <returns>The name of the most consumed food item or "No food logs recorded" if no logs are available.</returns>
        public string MostConsumedFood()
        {
            var mostConsumedFood = FoodLogs
                .GroupBy(log => log.FoodItem)
                .Select(group => new
                {
                    FoodItem = group.Key,
                    TotalCalories = group.Sum(log => log.FoodCalories)
                })
                .OrderByDescending(item => item.TotalCalories)
                .FirstOrDefault();

            if (mostConsumedFood != null)
            {
                return mostConsumedFood.FoodItem;
            }
            else
            {
                return "No food logs recorded";
            }
        }





        /// <summary>
        /// Calculates the difference between total calories consumed and total calories burned to determine caloric intake vs. expenditure.
        /// </summary>
        /// <returns>The difference between total calories consumed and total calories burned.</returns>
        public double CaloricIntakeVsExpenditure()
        {
            double totalcaloriesburned = TotalCaloriesBurned();
            double totalcaloriesconsumed = TotalCaloriesConsumed();

            Console.WriteLine($"Total calories consumed: {totalcaloriesconsumed}");
            Console.WriteLine($"Total calories burned: {totalcaloriesburned}");

            return totalcaloriesconsumed - totalcaloriesburned;
        }






        /// <summary>
        /// Calculates the difference between total calories consumed and total calories burned within a specified date range to determine caloric intake vs. expenditure.
        /// </summary>
        /// <param name="startDate">The start date of the date range.</param>
        /// <param name="endDate">The end date of the date range.</param>
        /// <returns>The difference between total calories consumed and total calories burned within the date range.</returns>
        public double CaloricIntakeVsExpenditure(DateTime startDate, DateTime endDate)
        {
            double totalCaloriesConsumedInRange = FoodLogs
                .Where(log => log.FoodlogDate >= startDate && log.FoodlogDate <= endDate)
                .Sum(log => log.FoodCalories);

            double totalCaloriesBurnedInRange = Workouts
                .Where(workout => workout.WorkoutDate >= startDate && workout.WorkoutDate <= endDate)
                .Sum(workout => workout.CaloriesBurned);

            Console.WriteLine($"Total calories burned during this date range: {totalCaloriesBurnedInRange}");
            Console.WriteLine($"Total calories consumed during this date range: {totalCaloriesConsumedInRange}");

            return totalCaloriesConsumedInRange - totalCaloriesBurnedInRange;
        }





        /// <summary>
        /// Adds a new entry, including a workout, food log, and body measurements, to the database.
        /// </summary>
        /// <param name="db">The FitnessDbContext instance.</param>
        public void AddAnEntry(FitnessDbContext db)
        {
            var workout = AddWorkoutEntry();
            db.Workouts.Add(workout);
            var foodlog = AddFoodLog();
            db.FoodLogs.Add(foodlog);
            var bodymeasurements = AddBodyMeasurement();
            db.BodyMeasurements.Add(bodymeasurements);

            db.SaveChanges();
        }





        /// <summary>
        /// Gathers details from the user to create a new workout entry.
        /// </summary>
        /// <returns>A new Workout instance based on user input.</returns>
        public Workout AddWorkoutEntry()
        {
            Console.WriteLine("\n--------Workout details----------");

            string? P_workoutdate = null;
            DateTime workoutdate;

            while (string.IsNullOrEmpty(P_workoutdate) || !DateTime.TryParse(P_workoutdate, out workoutdate))
            {
                Console.Write("Please enter the workout date: ");
                P_workoutdate = Console.ReadLine();
            }

            // Gather other parameters from the user
            Console.Write("Please enter the workout type: ");
            string workoutType = Console.ReadLine() ?? "";

            Console.Write("Please enter the workout start time (hh:mm): ");
            TimeOnly workoutstart = TimeOnly.Parse(Console.ReadLine() ?? "");

            Console.Write("Please enter the workout finish time (hh:mm): ");
            TimeOnly workoutfinished = TimeOnly.Parse(Console.ReadLine() ?? "");

            Console.Write("Please enter the calories burned: ");
            double caloriesBurned;
            while (!double.TryParse(Console.ReadLine(), out caloriesBurned))
            {
                Console.WriteLine("Please enter a valid number for calories burned:");
            }

            return new Workout(workoutdate, workoutType, workoutstart, workoutfinished, caloriesBurned, PersonId);
        }





        /// <summary>
        /// Gathers details from the user to create a new food log entry.
        /// </summary>
        /// <returns>A new FoodLog instance based on user input.</returns>
        public FoodLog AddFoodLog()
        {
            Console.WriteLine("\n--------Food Log details----------");

            string? P_foodlogDate = null;
            DateTime foodlogDate;

            while (string.IsNullOrEmpty(P_foodlogDate) || !DateTime.TryParse(P_foodlogDate, out foodlogDate))
            {
                Console.Write("Please enter the food log date: ");
                P_foodlogDate = Console.ReadLine();
            }

            Console.Write("Please enter the meal type: ");
            string? P_mealType = Console.ReadLine() ?? "";

            Console.Write("Please enter the food item: ");
            string? P_foodItem = Console.ReadLine() ?? "";

            Console.Write("Please enter the food calories: ");
            double P_foodCalories;
            while (!double.TryParse(Console.ReadLine(), out P_foodCalories))
            {
                Console.WriteLine("Please enter a valid number for food calories:");
            }

            return new FoodLog(foodlogDate, P_mealType, P_foodItem, P_foodCalories, PersonId);
        }






        /// <summary>
        /// Gathers details from the user to create a new body measurement entry.
        /// </summary>
        /// <returns>A new BodyMeasurements instance based on user input.</returns>
        public BodyMeasurements AddBodyMeasurement()
        {
            Console.WriteLine("\n--------Body Measurement details----------");

            string? P_bodymeasurementDate = null;
            DateTime bodymeasurementDate;

            while (string.IsNullOrEmpty(P_bodymeasurementDate) || !DateTime.TryParse(P_bodymeasurementDate, out bodymeasurementDate))
            {
                Console.Write("Please enter the body measurement date: ");
                P_bodymeasurementDate = Console.ReadLine();
            }

            Console.Write("Please enter the weight: ");
            double weight;
            while (!double.TryParse(Console.ReadLine(), out weight))
            {
                Console.WriteLine("Please enter a valid number for weight:");
            }

            Console.Write("Please enter the height: ");
            double height;
            while (!double.TryParse(Console.ReadLine(), out height))
            {
                Console.WriteLine("Please enter a valid number for height:");
            }

            Console.Write("Please enter the body fat percentage: ");
            double bodyfatPercentage;
            while (!double.TryParse(Console.ReadLine(), out bodyfatPercentage))
            {
                Console.WriteLine("Please enter a valid number for body fat percentage:");
            }

            return new BodyMeasurements(bodymeasurementDate, weight, height, bodyfatPercentage, PersonId);
        }






        /// <summary>
        /// Deletes a workout entry from the database and the current user's workout list.
        /// </summary>
        /// <param name="db">The FitnessDbContext instance representing the database.</param>
        public void DeleteWorkoutEntry(FitnessDbContext db)
        {
            Console.WriteLine("-------- Delete Workout Entry --------");

            if (Workouts.Count == 0)
            {
                Console.WriteLine("No workout entries to delete.");
                return;
            }

            Console.WriteLine("Select a workout entry to delete:");

            for (int i = 0; i < Workouts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Date: {Workouts[i].WorkoutDate}, Type: {Workouts[i].WorkoutType}");
            }

            Console.Write("Enter the number of the workout entry to delete: ");
            if (int.TryParse(Console.ReadLine(), out int selectedEntryIndex) && selectedEntryIndex > 0 && selectedEntryIndex <= Workouts.Count)
            {
                var selectedWorkout = Workouts[selectedEntryIndex - 1];

                db.Workouts.Remove(selectedWorkout);
                Workouts.Remove(selectedWorkout);

                db.SaveChanges();

                Console.WriteLine("Workout entry deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid selection. No workout entry deleted.");
            }
        }





        /// <summary>
        /// Deletes a food log entry from the database and the current user's food log list.
        /// </summary>
        /// <param name="db">The FitnessDbContext instance representing the database.</param>
        public void DeleteFoodLogEntry(FitnessDbContext db)
        {
            Console.WriteLine("-------- Delete Food Log Entry --------");

            if (FoodLogs.Count == 0)
            {
                Console.WriteLine("No food log entries to delete.");
                return;
            }

            Console.WriteLine("Select a food log entry to delete:");

            for (int i = 0; i < FoodLogs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Date: {FoodLogs[i].FoodlogDate}, Meal Type: {FoodLogs[i].MealType}, Food Item: {FoodLogs[i].FoodItem}");
            }

            Console.Write("Enter the number of the food log entry to delete: ");
            if (int.TryParse(Console.ReadLine(), out int selectedEntryIndex) && selectedEntryIndex > 0 && selectedEntryIndex <= FoodLogs.Count)
            {
                var selectedFoodLog = FoodLogs[selectedEntryIndex - 1];

                db.FoodLogs.Remove(selectedFoodLog);
                FoodLogs.Remove(selectedFoodLog);

                db.SaveChanges();

                Console.WriteLine("Food log entry deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid selection. No food log entry deleted.");
            }
        }






        /// <summary>
        /// Deletes a body measurement entry from the database and the current user's body measurement list.
        /// </summary>
        /// <param name="db">The FitnessDbContext instance representing the database.</param>
        public void DeleteBodyMeasurementEntry(FitnessDbContext db)
        {
            Console.WriteLine("-------- Delete Body Measurement Entry --------");

            if (BodyMeasurements.Count == 0)
            {
                Console.WriteLine("No body measurement entries to delete.");
                return;
            }

            Console.WriteLine("Select a body measurement entry to delete:");

            for (int i = 0; i < BodyMeasurements.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Date: {BodyMeasurements[i].BodymeasurementDate}, Weight: {BodyMeasurements[i].Weight}, Height: {BodyMeasurements[i].Height}");
            }

            Console.Write("Enter the number of the body measurement entry to delete: ");
            if (int.TryParse(Console.ReadLine(), out int selectedEntryIndex) && selectedEntryIndex > 0 && selectedEntryIndex <= BodyMeasurements.Count)
            {
                var selectedBodyMeasurement = BodyMeasurements[selectedEntryIndex - 1];

                db.BodyMeasurements.Remove(selectedBodyMeasurement);
                BodyMeasurements.Remove(selectedBodyMeasurement);

                db.SaveChanges();

                Console.WriteLine("Body measurement entry deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid selection. No body measurement entry deleted.");
            }
        }






        /// <summary>
        /// Updates a workout entry in the database and the current user's workout list.
        /// </summary>
        /// <param name="db">The FitnessDbContext instance representing the database.</param>
        public void UpdateWorkoutEntry(FitnessDbContext db)
        {
            Console.WriteLine("-------- Update Workout Entry --------");

            if (Workouts.Count == 0)
            {
                Console.WriteLine("No workout entries to update.");
                return;
            }

            Console.WriteLine("Select a workout entry to update:");

            for (int i = 0; i < Workouts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Date: {Workouts[i].WorkoutDate}, Type: {Workouts[i].WorkoutType}");
            }

            Console.Write("Enter the number of the workout entry to update: ");
            if (int.TryParse(Console.ReadLine(), out int selectedEntryIndex) && selectedEntryIndex > 0 && selectedEntryIndex <= Workouts.Count)
            {
                var selectedWorkout = Workouts[selectedEntryIndex - 1];

                Console.WriteLine($"Selected Workout: Date: {selectedWorkout.WorkoutDate}, Type: {selectedWorkout.WorkoutType}, Duration: {selectedWorkout.Duration}, Calories Burned: {selectedWorkout.CaloriesBurned}");

                Console.WriteLine("Choose what you want to update:");
                Console.WriteLine("1. Update Workout Type");
                Console.WriteLine("2. Update Workout Date");
                Console.WriteLine("3. Update Workout Start Time");
                Console.WriteLine("4. Update Workout Finish Time");
                Console.WriteLine("5. Update Calories Burned");
                Console.Write("Enter your choice: ");
                string? updateOption = Console.ReadLine();

                if (string.IsNullOrEmpty(updateOption))
                {
                    Console.WriteLine("Invalid choice. No updates made.");
                    return;
                }

                switch (updateOption)
                {
                    case "1":
                        Console.Write("Enter the new workout type: ");
                        selectedWorkout.WorkoutType = Console.ReadLine() ?? "";
                        break;
                    case "2":
                        Console.Write("Enter the new workout date: ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime newDate))
                        {
                            selectedWorkout.WorkoutDate = newDate;
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format. No updates made.");
                        }
                        break;
                    case "3":
                        Console.Write("Enter the new workout start time (hh:mm): ");
                        if (TimeOnly.TryParse(Console.ReadLine(), out TimeOnly newStartTime))
                        {
                            selectedWorkout.Duration = selectedWorkout.WorkoutFinished - newStartTime;
                        }
                        else
                        {
                            Console.WriteLine("Invalid time format. No updates made.");
                        }
                        break;
                    case "4":
                        Console.Write("Enter the new workout finish time (hh:mm): ");
                        if (TimeOnly.TryParse(Console.ReadLine(), out TimeOnly newFinishTime))
                        {
                            selectedWorkout.Duration = newFinishTime - selectedWorkout.WorkoutStart;
                        }
                        else
                        {
                            Console.WriteLine("Invalid time format. No updates made.");
                        }
                        break;
                    case "5":
                        Console.Write("Enter the new calories burned: ");
                        if (double.TryParse(Console.ReadLine(), out double newCaloriesBurned))
                        {
                            selectedWorkout.CaloriesBurned = newCaloriesBurned;
                        }
                        else
                        {
                            Console.WriteLine("Invalid calories format. No updates made.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice. No updates made.");
                        break;
                }

                db.SaveChanges();

                Console.WriteLine("Workout entry updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid selection. No workout entry updated.");
            }
        }







        /// <summary>
        /// Updates a food log entry in the database and the current user's food log list.
        /// </summary>
        /// <param name="db">The FitnessDbContext instance representing the database.</param>
        public void UpdateFoodLogEntry(FitnessDbContext db)
        {
            Console.WriteLine("\nUpdate Food Log Entry");
            Console.WriteLine("----------------------");


            DisplayFoodLogs();

            Console.Write("Enter the number of the food log entry you want to update: ");
            if (int.TryParse(Console.ReadLine(), out int selectedEntryIndex) && selectedEntryIndex > 0 && selectedEntryIndex <= FoodLogs.Count)
            {
                var selectedFoodLog = FoodLogs[selectedEntryIndex - 1];

                Console.WriteLine($"Selected Food Log: Date: {selectedFoodLog.FoodlogDate}, Meal Type: {selectedFoodLog.MealType}, Food Item: {selectedFoodLog.FoodItem}, Calories: {selectedFoodLog.FoodCalories}");

                Console.WriteLine("Choose what you want to update:");
                Console.WriteLine("1. Update Meal Type");
                Console.WriteLine("2. Update Food Item");
                Console.WriteLine("3. Update Food Calories");
                Console.WriteLine("4. Update Food Log Date");
                Console.Write("Enter your choice: ");
                string? updateOption = Console.ReadLine();

                if (string.IsNullOrEmpty(updateOption))
                {
                    Console.WriteLine("Invalid choice. No updates made.");
                    return;
                }

                switch (updateOption)
                {
                    case "1":
                        Console.Write("Enter the new meal type: ");
                        selectedFoodLog.MealType = Console.ReadLine() ?? "";
                        break;
                    case "2":
                        Console.Write("Enter the new food item: ");
                        selectedFoodLog.FoodItem = Console.ReadLine() ?? "";
                        break;
                    case "3":
                        Console.Write("Enter the new food calories: ");
                        if (double.TryParse(Console.ReadLine(), out double newFoodCalories))
                        {
                            selectedFoodLog.FoodCalories = newFoodCalories;
                        }
                        else
                        {
                            Console.WriteLine("Invalid calories format. No updates made.");
                        }
                        break;
                    case "4":
                        Console.Write("Enter the new food log date: ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime newFoodLogDate))
                        {
                            selectedFoodLog.FoodlogDate = newFoodLogDate;
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format. No updates made.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice. No updates made.");
                        break;
                }

                db.SaveChanges();

                Console.WriteLine("Food log entry updated successfully!");
            }
            else
            {
                Console.WriteLine("Invalid entry number. No updates made.");
            }
        }



        private void DisplayFoodLogs()
        {
            Console.WriteLine("\nExisting Food Logs");
            Console.WriteLine("-------------------");

            for (int i = 0; i < FoodLogs.Count; i++)
            {
                var foodLog = FoodLogs[i];
                Console.WriteLine($"{i + 1}. Date: {foodLog.FoodlogDate}, Meal Type: {foodLog.MealType}, Food Item: {foodLog.FoodItem}, Calories: {foodLog.FoodCalories}");
            }
        }





        /// <summary>
        /// Updates a body measurement entry in the database and the current user's body measurement list.
        /// </summary>
        /// <param name="db">The FitnessDbContext instance representing the database.</param>
        public void UpdateBodyMeasurementEntry(FitnessDbContext db)
        {
            Console.WriteLine("\nUpdate Body Measurement Entry");
            Console.WriteLine("-----------------------------");

            DisplayBodyMeasurements();

            Console.Write("Enter the number of the body measurement entry you want to update: ");
            if (int.TryParse(Console.ReadLine(), out int selectedEntryIndex) && selectedEntryIndex > 0 && selectedEntryIndex <= BodyMeasurements.Count)
            {
                var selectedBodyMeasurement = BodyMeasurements[selectedEntryIndex - 1];

                Console.WriteLine($"Selected Body Measurement: Date: {selectedBodyMeasurement.BodymeasurementDate}, Weight: {selectedBodyMeasurement.Weight}, Height: {selectedBodyMeasurement.Height}, Body Fat Percentage: {selectedBodyMeasurement.BodyfatPercentage}");

                Console.WriteLine("Choose what you want to update:");
                Console.WriteLine("1. Update Weight");
                Console.WriteLine("2. Update Height");
                Console.WriteLine("3. Update Body Fat Percentage");
                Console.WriteLine("4. Update Body Measurement Date");
                Console.Write("Enter your choice: ");
                string? updateOption = Console.ReadLine();

                if (string.IsNullOrEmpty(updateOption))
                {
                    Console.WriteLine("Invalid choice. No updates made.");
                    return;
                }

                switch (updateOption)
                {
                    case "1":
                        Console.Write("Enter the new weight: ");
                        if (double.TryParse(Console.ReadLine(), out double newWeight))
                        {
                            selectedBodyMeasurement.Weight = newWeight;
                        }
                        else
                        {
                            Console.WriteLine("Invalid weight format. No updates made.");
                        }
                        break;
                    case "2":
                        Console.Write("Enter the new height: ");
                        if (double.TryParse(Console.ReadLine(), out double newHeight))
                        {
                            selectedBodyMeasurement.Height = newHeight;
                        }
                        else
                        {
                            Console.WriteLine("Invalid height format. No updates made.");
                        }
                        break;
                    case "3":
                        Console.Write("Enter the new body fat percentage: ");
                        if (double.TryParse(Console.ReadLine(), out double newBodyFatPercentage))
                        {
                            selectedBodyMeasurement.BodyfatPercentage = newBodyFatPercentage;
                        }
                        else
                        {
                            Console.WriteLine("Invalid body fat percentage format. No updates made.");
                        }
                        break;
                    case "4":
                        Console.Write("Enter the new body measurement date: ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime newBodyMeasurementDate))
                        {
                            selectedBodyMeasurement.BodymeasurementDate = newBodyMeasurementDate;
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format. No updates made.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice. No updates made.");
                        break;
                }

                db.SaveChanges();

                Console.WriteLine("Body measurement entry updated successfully!");
            }
            else
            {
                Console.WriteLine("Invalid entry number. No updates made.");
            }
        }





        private void DisplayBodyMeasurements()
        {
            Console.WriteLine("\nExisting Body Measurements");
            Console.WriteLine("--------------------------");

            for (int i = 0; i < BodyMeasurements.Count; i++)
            {
                var bodyMeasurement = BodyMeasurements[i];
                Console.WriteLine($"{i + 1}. Date: {bodyMeasurement.BodymeasurementDate}, Weight: {bodyMeasurement.Weight}, Height: {bodyMeasurement.Height}, Body Fat Percentage: {bodyMeasurement.BodyfatPercentage}");
            }
        }







        /// <summary>
        /// Displays a menu to view various entries (Workouts, Food Logs, Body Measurements) for the current user.
        /// </summary>
        public void ViewMyEntries()
        {
            while (true)
            {
                Console.WriteLine("\nView My Entries Menu:");
                Console.WriteLine("1. View Workouts");
                Console.WriteLine("2. View Food Logs");
                Console.WriteLine("3. View Body Measurements");
                Console.WriteLine("4. Go Back to Main Menu");

                Console.Write("Enter your choice: ");
                string? viewEntriesChoice = Console.ReadLine();

                if (string.IsNullOrEmpty(viewEntriesChoice))
                {
                    Console.WriteLine("Please enter a choice.");
                    continue;
                }

                switch (viewEntriesChoice)
                {
                    case "1":
                        DisplayWorkouts();
                        break;
                    case "2":
                        DisplayFoodLogs();
                        break;
                    case "3":
                        DisplayBodyMeasurements();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }





        /// <summary>
        /// Displays the list of workouts for the current user.
        /// </summary>
        public void DisplayWorkouts()
        {
            Console.WriteLine("\nYour Workouts");
            Console.WriteLine("--------------");

            foreach (var workout in Workouts)
            {
                Console.WriteLine($"Date: {workout.WorkoutDate}, Type: {workout.WorkoutType}, Duration: {workout.Duration}, Calories Burned: {workout.CaloriesBurned}");
            }
        }





        /// <summary>
        /// Logs out the current user.
        /// </summary>
        public void Logout()
        {
            Console.WriteLine("Logging out...");
            Username = null;
            HealthTracker.ReturnToLogin = true;
        }






        /// <summary>
        /// Displays detailed information about all users, including their body measurements, food logs, and workouts.
        /// </summary>
        /// <param name="db">The FitnessDbContext instance representing the database.</param>
        public static void ViewAllUserInfo(FitnessDbContext db)
        {
            var persons = db.Persons.Include(p => p.BodyMeasurements).Include(p => p.FoodLogs).Include(p => p.Workouts).ToList();

            foreach (var person in persons)
            {
                Console.WriteLine($"Person: {person.PersonName}, Age: {person.PersonAge}");

                Console.WriteLine("BodyMeasurements:");
                foreach (var bodyMeasurement in person.BodyMeasurements)
                {
                    Console.WriteLine($"  Date: {bodyMeasurement.BodymeasurementDate}, Weight: {bodyMeasurement.Weight}, Height: {bodyMeasurement.Height}");
                }

                Console.WriteLine("FoodLogs:");
                foreach (var foodLog in person.FoodLogs)
                {
                    Console.WriteLine($"  Date: {foodLog.FoodlogDate}, MealType: {foodLog.MealType}, FoodItem: {foodLog.FoodItem}, Calories: {foodLog.FoodCalories}");
                }

                Console.WriteLine("Workouts:");
                foreach (var workout in person.Workouts)
                {
                    Console.WriteLine($"  Date: {workout.WorkoutDate}, Type: {workout.WorkoutType}, Duration: {workout.Duration}, Calories Burned: {workout.CaloriesBurned}");
                }

                Console.WriteLine("----------------------------------");
            }

        }

        #endregion


    }
}
#endregion