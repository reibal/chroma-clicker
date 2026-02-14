using System;
using UnityEngine;

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

    private static readonly string[] SUFFIXES = new[]
    {
        "", "K", "M", "B", "T", "Qa", "Qt", "Sx", "Sp", "Oc", "No", "Dc"
    };

    public static string FormatBigNumber(float value, bool showDecimalsOnSmallNumbers = false)
    {
        if (value < 0)
        {
            throw new Exception("The method FormatBigNumber must receive a positive value. Got: " + value);
        }
        if (value < 1000)
        {
            return showDecimalsOnSmallNumbers ? value.ToString("0.##") : $"{value:F0}";
        }
        // For larger numbers, calculate suffix
        int powExp = Mathf.FloorToInt(Mathf.Log10(value));
        int suffixIndex = Mathf.FloorToInt(powExp / 3);
        float powBase = value / Mathf.Pow(10, suffixIndex * 3);
        string suffix = suffixIndex < SUFFIXES.Length ? SUFFIXES[suffixIndex] : $"e{powExp}";
        return $"{powBase:F2}{suffix}";
    }
}