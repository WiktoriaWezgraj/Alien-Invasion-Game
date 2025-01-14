using System;

namespace Simulator;

public static class Validator
{
    public static int Limiter(int value, int min, int max)
    {
        return Math.Max(min, Math.Min(max, value));
    }

    public static string Shortener(string value, int min, int max, char placeholder)
    {
        value ??= string.Empty;

        value = value.Trim();

        if (value.Length < min)
            value = value.PadRight(min, placeholder);

        if (value.Length > max)
        {
            value = value.Substring(0, max).TrimEnd();
            if (value.Length < min)
                value = value.PadRight(min, placeholder);
        }

        if (value.Length > 0)
            value = value[0].ToString().ToUpper() + value[1..];
        else
            return new string(placeholder, min); 

        return value;
    }
}

