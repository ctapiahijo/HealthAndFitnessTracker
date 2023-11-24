using HealthAndFitnessTracker.Interfaces;

#region HealthAndFitnessTracker
namespace HealthAndFitnessTracker.Classes
{
    public class HealthTracker
    {
        private readonly FitnessDbContext db = new();
        public static bool ReturnToLogin { get; set; } = false;
        public HealthTracker()
        {
            
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






        // Single Responsibility Principle (SRP) Comment:
        // The LoginOrRegister method is responsible for handling user login or registration,
        // adhering to SRP by focusing on user authentication and registration tasks.

        // Dependency Inversion Principle (DIP) Comment:
        // The FitnessDbContext dependency is injected into the LoginOrRegister method,
        // following DIP by allowing the method to work with any context that adheres to the
        // FitnessDbContext abstraction, enhancing flexibility in database operations.

        /// <summary>
        /// Allows the user to either log in or register, based on user input.
        /// </summary>
        /// <param name="dbContext">The FitnessDbContext used for database operations.</param>
        /// <returns>
        /// A Person object representing the logged-in or registered user if successful; otherwise, returns null.
        /// </returns>
        private static Person? LoginOrRegister(FitnessDbContext dbContext)
        {
            while (true)
            {
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                string? loginOrRegisterChoice = Console.ReadLine();

                switch (loginOrRegisterChoice)
                {
                    case "1":
                        Person? loggedInUser = Login(dbContext);
                        if (loggedInUser != null)
                        {
                            Console.WriteLine("Login successful!");
                            return loggedInUser;
                        }
                        else
                        {
                            Console.WriteLine("Invalid username or password. Please try again.");
                            break;
                        }

                    case "2":
                        Person? newUser = Register(dbContext);
                        if (newUser != null)
                        {
                            Console.WriteLine("Registration successful! You are now the current user.");
                            return newUser;
                        }
                        else
                        {
                            Console.WriteLine("Registration failed. Please try again.");
                            break;
                        }

                    case "3":
                        Console.WriteLine("Exiting the application.");
                        return null;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option (1, 2, or 3).");
                        break;
                }
            }

        }






        ///// <summary>
        ///// Validates the provided user's password.
        ///// </summary>
        ///// <param name="user">The Person object representing the user.</param>
        ///// <param name="password">The password to validate against the user's stored password.</param>
        ///// <returns>
        /////   <c>true</c> if the provided password matches the user's stored password; otherwise, <c>false</c>.
        ///// </returns>
        //private static bool ValidateUser(Person user, string password)
        //{
        //    if (user.Password == password)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        Console.WriteLine("Incorrect password. Please try again.");
        //        return false;
        //    }
        //}





        /// <summary>
        /// Creates a new Person object with the specified information.
        /// </summary>
        /// <param name="name">The name of the person.</param>
        /// <param name="age">The age of the person.</param>
        /// <param name="username">The username for the person.</param>
        /// <param name="password">The password for the person.</param>
        /// <returns>A new Person object initialized with the provided information.</returns>
        private static Person CreateUser(string name, int age, string username, string password)
        {
            return new Person { PersonName = name, PersonAge = age, Username = username, Password = password };
        }






        /// <summary>
        /// Logs in a user based on provided username and password.
        /// </summary>
        /// <param name="dbContext">The FitnessDbContext used for database operations.</param>
        /// <returns>
        /// A Person object representing the logged-in user if successful; otherwise, returns null.
        /// </returns>
        private static Person? Login(FitnessDbContext dbContext)
        {
            Console.Write("Enter your username: ");
            string? username = Console.ReadLine();

            Console.Write("Enter your password: ");
            string? password = Console.ReadLine();

            Person? user = dbContext.Persons.FirstOrDefault(u => u.Username == username && u.Password == password);

            return user;
        }








        /// <summary>
        /// Registers a new user with the provided information.
        /// </summary>
        /// <param name="dbContext">The FitnessDbContext used for database operations.</param>
        /// <returns>
        /// A Person object representing the registered user if successful; otherwise, returns null.
        /// </returns>
        private static Person? Register(FitnessDbContext dbContext)
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

                return newUser;
            }
            else
            {
                Console.WriteLine("Invalid age. Registration failed.");
                return null;
            }
        }


        #endregion





    }
}
#endregion
