namespace ClockMaster.Game;

/// <summary>Writes a time the way it is said in Danish.</summary>
public static class TimeText
{
    private static readonly string[] Numbers =
    [
        "nul", "et", "to", "tre", "fire", "fem", "seks", "syv", "otte", "ni",
        "ti", "elleve", "tolv", "tretten", "fjorten", "femten", "seksten", "sytten", "atten", "nitten",
        "tyve", "enogtyve", "toogtyve", "treogtyve",
    ];

    private static readonly Dictionary<int, string> Minutes = new()
    {
        [0] = "nul nul", [5] = "nul fem", [10] = "ti", [15] = "femten", [20] = "tyve",
        [25] = "femogtyve", [30] = "tredive", [35] = "femogtredive", [40] = "fyrre",
        [45] = "femogfyrre", [50] = "halvtreds", [55] = "femoghalvtreds",
    };

    /// <summary>Like a digital clock read aloud: 15:30 is "femten tredive", 04:40 "nul fire fyrre".</summary>
    public static string Digital(int hour, int minute) =>
        $"{(hour < 10 ? "nul " : "")}{Numbers[hour]} {Minutes[minute]}";

    /// <summary>
    /// The everyday way, on a 12-hour clock: 15:30 is "halv fire", 15:45 "kvart i fire",
    /// 15:10 "10 minutter over tre". From 25 minutes on the time is counted towards the next hour.
    /// </summary>
    public static string Spoken(int hour, int minute)
    {
        var now = Numbers[TwelveHour(hour)];
        var next = Numbers[TwelveHour(hour + 1)];

        return minute switch
        {
            0 => $"klokken {now}",
            15 => $"kvart over {now}",
            30 => $"halv {next}",
            45 => $"kvart i {next}",
            25 => $"fem minutter i halv {next}",
            35 => $"fem minutter over halv {next}",
            < 30 => $"{Numbers[minute]} minutter over {now}",
            _ => $"{Numbers[60 - minute]} minutter i {next}",
        };
    }

    // 0 and 12 are both "tolv" on the clock face.
    private static int TwelveHour(int hour) => hour % 12 == 0 ? 12 : hour % 12;
}
