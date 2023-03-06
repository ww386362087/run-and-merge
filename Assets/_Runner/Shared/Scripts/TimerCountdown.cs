using System;

public static class TimerCountdown
{
    static DateTime TimeStarted;
    static TimeSpan TotalTime;

    public static void StartCountDown(TimeSpan _countdownTime)
    {
        TimeStarted = DateTime.Now;
        TotalTime = _countdownTime;
    }

    public static TimeSpan TimeLeft
    {
        get
        {
            var result = TotalTime - (DateTime.Now - TimeStarted);
            if (result.TotalSeconds <= 0)
                return TimeSpan.Zero;
            return result;
        }

        //set { }
    }
}
