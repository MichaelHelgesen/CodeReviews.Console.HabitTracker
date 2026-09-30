namespace habitTracker.MichaelHelgesen.Models;

internal class HabitLog
{
    internal string DateTimeNow { get; set; } = DateTimeOffset.Now.ToString();
    internal required long HabitID { get; set; }
    internal required int Ocurrence { get; set; }
    internal long ID { get; set; }
}