namespace habitTracker.MichaelHelgesen.Models;

internal class HabitLog
{
    internal DateTime DateTimeNow { get; set; } = DateTime.Now;
    internal required int HabitID { get; set; }
    internal int ID { get; set; }
}