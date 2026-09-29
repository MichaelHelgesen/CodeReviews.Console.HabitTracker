namespace habitTracker.MichaelHelgesen.Controllers;
using habitTracker.MichaelHelgesen.Models;

internal class AppController()
{
    internal static void RunApp()
    {
        HabitRepository.CreateTable();
        CreateTestData();
        /*int uniqueHabits = HabitRepository.GetUniqueHabits().Count();*/ // Vise beskjed dersom det ikke er noen vaner.
        MenuController.RenderMainMenu();
    }

    static void CreateTestData()
    {
        var testHabits = new[] { "trening", "lesing", "meditasjon" };
        foreach (var name in testHabits)
        {
            var habitID = HabitRepository.CreateHabit(name, name);
            int logDataEntries = 3;
            for (int i = 0; i < logDataEntries; i++)
            {
                HabitRepository.CreateHabitLog(DateTimeOffset.Now.ToString(), habitID);
            }
        }
    }
}