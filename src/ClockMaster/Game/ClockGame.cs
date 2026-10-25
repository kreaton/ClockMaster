namespace ClockMaster.Game;

public sealed class ClockGame
{
    private readonly Random _random = new();

    /// <summary>The time to set, 0–23.</summary>
    public int Hour { get; private set; }

    /// <summary>The minutes to set, always a whole five minutes.</summary>
    public int Minute { get; private set; }

    public string Display => $"{Hour:00}:{Minute:00}";

    /// <summary>The time in words, either the everyday way or read off a digital clock; chosen at random each round.</summary>
    public string Text { get; private set; } = "";

    /// <summary>Picks a new time, never the same as the last one.</summary>
    public void NewRound()
    {
        int hour, minute;
        do
        {
            hour = _random.Next(24);
            minute = _random.Next(12) * 5;
        } while ((hour, minute) == (Hour, Minute));

        (Hour, Minute) = (hour, minute);
        Text = _random.Next(2) == 0 ? TimeText.Spoken(hour, minute) : TimeText.Digital(hour, minute);
    }

    /// <summary>
    /// Whether the hands show the time. The clock face only has 12 hours, so 15:00 and 3:00
    /// are set the same way, and the 12 dot means both 0:00 and 12:00.
    /// </summary>
    public bool IsCorrect(int clockHour, int clockMinute) =>
        clockHour % 12 == Hour % 12 && clockMinute == Minute;
}
