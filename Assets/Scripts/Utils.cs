public static class Utils
{
    public static string FormatTimeLapseFromSeconds(long seconds)
    {
        int SECONDS_IN_A_MINUTE = 60;
        int SECONDS_IN_AN_HOUR = 60 * 60;

        if (seconds < SECONDS_IN_A_MINUTE)
        {
            return seconds + " seconds";
        }
        if (seconds < SECONDS_IN_AN_HOUR)
        {
            return seconds / SECONDS_IN_A_MINUTE + " minutes";
        }
        return seconds / SECONDS_IN_AN_HOUR + " hours";
    }
}
