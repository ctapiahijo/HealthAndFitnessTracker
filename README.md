# HealthAndFitnessTracker

Fitness Tracker Console App

Overview
The Fitness Tracker Console App is a health management system designed to help users monitor and maintain their fitness journey. 
The application allows users to log and track their workouts, food consumption, and body measurements. With a user-friendly console interface, 
individuals can easily input and manage their health-related data, enabling them to make informed decisions about their fitness and nutrition.

Key Features
1. Workout Tracking:
Log details of each workout, including type, date, duration, and calories burned.
Calculate and display total calories burned and average workout time.

2. Food Log:
Record food intake, specifying meal types and associated calories.
Provide insights into total calories consumed and distribution among different food items.

3. Body Measurements:
Track body measurements, including weight, height, and body fat percentage.
Support updates and deletions for accurate and up-to-date information.

4. User Management:
Secure user authentication and logout functionality.
View detailed information about all users, adhering to the Dependency Inversion Principle.

Required Features:

1.Create 3 or more unit tests for your application. (xUnit Test class)
2.Create a dictionary or list, populate it with several values, retrieve at least one value, and use it in your program ( the function: InsertDataIntoDatabase() uses the lists)
3.Implement a log that records errors, invalid inputs, or other important events and writes them to a text file. (Logger class is in charge of creating logs to a text file)
4.Add comments to your code explaining how you are using at least 2 of the solid principles. (Person class: Line 1053) && (HealthTracker: Line 137)

#---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

How to Use

.NET SDK: Ensure that you have the .NET SDK installed to build and run the application. 
Note that the database will contain mock data by default.

Files:
"Fitness.db" will be created when the app is run.
"log.txt" will be generated when users log in, log out, create records, update records, or delete records.

Log In:

When prompted, log in using your credentials. If you don't have an account, follow the on-screen instructions to create one.

Main Menu:

Navigate through the main menu options using the provided numeric choices.

Track Workouts:

Choose option 1 to log and manage your workouts. Follow the prompts to add, update, or delete workout entries.
Log Food Intake:

Choose option 2 to record your food logs. Input meal details, food items, and calories as prompted.
Monitor Body Measurements:

Select option 3 to track your body measurements. Input weight, height, and body fat percentage when prompted.
View Entries:

Use option 4 to view your workouts, food logs, and body measurements. Follow on-screen instructions to navigate through the entries.
Log Out:

Option 5 allows you to log out securely.

#------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Database Diagram

Persons Table:
+-------------+--------------+
| Column      | Type         |
+-------------+--------------+
| PersonId    | int (PK)     |
| PersonName  | varchar      |
| PersonAge   | int          |



BodyMeasurements Table:
+---------------------+--------------+
| Column              | Type         |
+---------------------+--------------+
| BodyMeasurementId   | int          |
| PersonId            | int  (FK)    |
| BodymeasurementDate | datetime     |
| Weight              | double       |
| BodyfatPercentage   | double       |



FoodLogs Table:
+-------------+--------------+
| Column      | Type         |
+-------------+--------------+
| FoodLogId   | int          |
| PersonId    | int  (FK)    |
| FoodlogDate | datetime     |
| MealType    | varchar      |
| FoodCalories| double       |



Workouts Table:
+---------------+--------------+
| Column        | Type         |
+---------------+--------------+
| WorkoutId     | int          |
| PersonId      | int  (FK)    |
| WorkoutDate   | datetime     |
| WorkoutType   | varchar      |
| WorkoutStart  | time         |
| CaloriesBurned| double       |


#-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
Technologies Used
C#
Entity Framework Core for database interactions
Console-based user interface
Project Structure
The project follows a structured architecture, promoting modularity and adherence to SOLID principles. It employs a database-first approach using Entity Framework Core for efficient data management.

Future Enhancements
Graphical User Interface (GUI): Expand the application by incorporating a graphical interface for a more user-friendly experience.

If you find any issues or have suggestions for improvements, feel free to open an issue or submit a pull request. We welcome contributions from the community!