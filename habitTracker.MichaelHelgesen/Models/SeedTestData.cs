namespace habitTracker.MichaelHelgesen.Models;

internal class SeedTestData
{
    static internal void CreateTestData()
    {
        var testHabits = new[] { "trening", "lesing", "meditasjon" };
        foreach (var name in testHabits) { 
            var habitID = HabitRepository.CreateHabit(name, name);
            int logDataEntries = 3;
            for (int i = 0; i < logDataEntries; i++)
            {
                HabitRepository.CreateHabitLog(DateTimeOffset.Now.ToString(), habitID);
            }
        }
    }
}

