namespace habitTracker.MichaelHelgesen.Models;

internal class Habit
{
    internal required string ID { get; set; }

    internal DateTimeOffset DateTimeNow { get; } = DateTimeOffset.Now;

    internal required string Title { get; set; }

     
}