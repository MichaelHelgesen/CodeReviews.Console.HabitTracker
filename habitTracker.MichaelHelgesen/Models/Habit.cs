namespace habitTracker.MichaelHelgesen.Models;

internal class Habit
{
    internal int ID { get; set; }

    internal DateTimeOffset DateTime { get; set; } = DateTimeOffset.Now;

    internal required string Title { get; set; }
}