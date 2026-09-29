namespace habitTracker.MichaelHelgesen.Models;

internal class SeedTestData
{
    void CreateTestData()
    {
        var testHabits = new[] { "trening", "lesing", "meditasjon" };
        foreach (var name in testHabits) { 
            HabitRepository.CreateHabit(name, name); 
        }
    }
}

