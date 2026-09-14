/*
REQUIREMENTS
This is an application where you'll log occurrences of a habit.

[ ] This habit can't be tracked by time (ex. hours of sleep), only by quantity (ex. number of water glasses a day)
[ ] Users need to be able to input the date of the occurrence of the habit
[ ] The application should store and retrieve data from a real database
[ ] When the application starts, it should create a sqlite database, if one isn't present.
[ ] It should also create a table in the database, where the habit will be logged.
[ ] The users should be able to insert, delete, update and view their logged habit.
[ ] You should handle all possible errors so that the application never crashes.
[ ] You can only interact with the database using ADO.NET. You can't use mappers such as Entity Framework or Dapper.
[ ] Follow the DRY Principle, and avoid code repetition.
[ ] Your project needs to contain a Read Me file where you'll explain how your app works and tell a little bit about your thought progress. What was hard? What was easy? What have you learned? Here's a nice example:
*/

/*
CHALLENGES
- If you already have a bit of experience with programming, we highly recommend you get into the habit of writing unit tests for a few methods in your project. Any method that outputs data and doesn't talk to a database (those are tested in integration tests) can be unit tested. A good example is any method that deals with validation. Here's a quick tutorial.
- If you haven't, try using parameterized queries to make your application more secure.
- Let the users create their own habits to track. That will require that you let them choose the unit of measurement of each habit. Hot tip: You should not create a table for each habit.
- Seed Data into the database automatically when the database gets created for the first time, generating a few habits and inserting a hundred records with randomly generated values. This is specially helpful during development so you don't have to reinsert data every time you create the database.
*/

/*
TIPS
- Read this article about the KISS principle and try to apply it to this project.
- Test your SQL commands on DB Browser before using them in your program.
- To improve the user's experience, when asking for a date input, give the option to type a simple command to add today's date
- You can keep all of the code in one single class if you wish. We'll deal with Object Oriented Programming in the next project
- Don't forget the user input's validation: Check for incorrect dates. What happens if a menu option is chosen that's not available? What happens if the users input a string instead of a number?
*/

using habitTracker.MichaelHelgesen.Models;


HabitRepository.CreateTable();
try
{
    HabitRepository.CreateUser("petett", 7);
}
catch (System.Exception ex)
{
    Console.WriteLine("User og ID aldready exist");
    Console.WriteLine(ex.Message);
}
HabitRepository.CheckForUsers(5);
