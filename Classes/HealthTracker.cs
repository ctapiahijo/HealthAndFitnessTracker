using HealthAndFitnessTracker.Interfaces;

namespace HealthAndFitnessTracker.Classes
{
    public class HealthTracker : IHealthTracker
    {
        private readonly FitnessDbContext db;
        public static bool ReturnToLogin { get; set; } = false;
        public HealthTracker(FitnessDbContext dbContext)
        {
            db = dbContext;
        }


        public void Start(FitnessDbContext dbContext)
        {
            Console.WriteLine("Health and Fitness Tracker");
            Console.WriteLine("----------------------------");

            Person? currentUser = LoginOrRegister(dbContext);

            if (currentUser == null)
            {
                Console.WriteLine("Exiting the application.");
                return;
            }

            while (!ReturnToLogin)
            {
                Console.WriteLine("\nMain Menu:");
                Console.WriteLine("1. Add Entries");
                Console.WriteLine("2. Delete Entries");
                Console.WriteLine("3. Update Entries");
                Console.WriteLine("4. View My Entries");
                Console.WriteLine("5. View All Users Info");
                Console.WriteLine("6. Logout");

                Console.Write("Enter your choice: ");
                string? mainChoice = Console.ReadLine();

                if (string.IsNullOrEmpty(mainChoice))
                {
                    Console.WriteLine("Please enter a choice.");
                    continue;
                }

                switch (mainChoice)
                {
                    case "1":
                        currentUser.AddAnEntry(db);
                        db.Persons.Add(currentUser);
                        break;
                    case "2":
                        Console.WriteLine("Select the type of entry to delete:");
                        Console.WriteLine("1. Delete Workout Entry");
                        Console.WriteLine("2. Delete Food Log Entry");
                        Console.WriteLine("3. Delete Body Measurement Entry");
                        Console.Write("Enter your choice: ");
                        string? deleteChoice = Console.ReadLine();

                        if (string.IsNullOrEmpty(deleteChoice))
                        {
                            Console.WriteLine("Invalid choice. Please enter a valid option.");
                            break;
                        }
                        switch (deleteChoice)
                        {
                            case "1":
                                currentUser.DeleteWorkoutEntry(db);
                                break;
                            case "2":
                                currentUser.DeleteFoodLogEntry(db);
                                break;
                            case "3":
                                currentUser.DeleteBodyMeasurementEntry(db);
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please enter a valid option.");
                                break;
                        }
                        break;
                    case "3":
                        Console.WriteLine("Select the type of entry to update:");
                        Console.WriteLine("1. Update Workout Entry");
                        Console.WriteLine("2. Update Food Log Entry");
                        Console.WriteLine("3. Update Body Measurement Entry");
                        Console.Write("Enter your choice: ");
                        string? updateChoice = Console.ReadLine();

                        if (string.IsNullOrEmpty(updateChoice))
                        {
                            Console.WriteLine("Invalid choice. Please enter a valid option.");
                            break;
                        }

                        switch (updateChoice)
                        {
                            case "1":
                                currentUser.UpdateWorkoutEntry(db);
                                break;
                            case "2":
                                currentUser.UpdateFoodLogEntry(db);
                                break;
                            case "3":
                                currentUser.UpdateBodyMeasurementEntry(db);
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please enter a valid option.");
                                break;
                        }
                        break;
                    case "4":
                        currentUser.ViewMyEntries();
                        break;
                    case "5":
                        Person.ViewAllUserInfo(db);
                        break;
                    case "6":
                        currentUser.Logout();
                        Console.WriteLine("Logged out successfully.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        #region UserFunctions_Validations
        private static Person? LoginOrRegister(FitnessDbContext dbContext)
        {
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            string? loginOrRegisterChoice = Console.ReadLine();

            if (loginOrRegisterChoice == "1")
            {
                Console.Write("Enter your username: ");
                string? username = Console.ReadLine();

                Console.Write("Enter your password: ");
                string? password = Console.ReadLine();

                Person? user = dbContext.Persons.FirstOrDefault(u => u.Username == username);

                if (user != null && ValidateUser(user, user.Password))
                {
                    Console.WriteLine("Login successful!");
                    return user;
                }
                else
                {
                    Console.WriteLine("Invalid username or password. Exiting the application.");
                    return null;
                }
            }
            else if (loginOrRegisterChoice == "2")
            {
                Console.Write("Enter your name: ");
                string name = Console.ReadLine() ?? "";

                Console.Write("Enter your age: ");
                if (int.TryParse(Console.ReadLine(), out int age) && age >= 0)
                {
                    Console.Write("Enter your new username: ");
                    string newUsername = Console.ReadLine() ?? "";

                    Console.Write("Enter your password: ");
                    string newPassword = Console.ReadLine() ?? "";

                    Person newUser = CreateUser(name, age, newUsername, newPassword);
                    dbContext.Persons.Add(newUser);
                    dbContext.SaveChanges();

                    Console.WriteLine("Registration successful! You are now the current user.");
                    return newUser;
                }
                else
                {
                    Console.WriteLine("Invalid age. Registration failed.");
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
        private static bool ValidateUser(Person user, string password)
        {
            if (user.Password == password)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Incorrect password. Please try again.");
                return false;
            }
        }
        private static Person CreateUser(string name, int age, string username, string password)
        {
            return new Person { PersonName = name, PersonAge = age, Username = username, Password = password };
        }
        #endregion


    }
}

