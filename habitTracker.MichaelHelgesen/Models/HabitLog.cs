namespace habitTracker.MichaelHelgesen.Models;

internal class HabitLog
{
    internal string DateTimeNow { get; set; } = DateTimeOffset.Now.ToString();
    internal required int HabitID { get; set; }
    internal int ID { get; set; }
}