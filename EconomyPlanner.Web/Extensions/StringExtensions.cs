using EconomyPlanner.Web.Helpers;

namespace EconomyPlanner.Web.Extensions;

public static class StringExtensions
{
    public static string FirstCharToUpper(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($@"{nameof(input)} cannot be empty", nameof(input)),
            _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
        };

    public static string ToRoundedNumberFormatString(this decimal input)
    {
        var numberCulture = NumberCultureHelper.GetNumberCulture();
        return Math.Round(input, 2).ToString("#,0.00", numberCulture).Replace($"{numberCulture.CurrencyDecimalSeparator}00", "");
    }
    
    public static string ToNumberFormatString(this decimal input)
    {
        var numberCulture = NumberCultureHelper.GetNumberCulture();
        return input.ToString("#,0.00", numberCulture).Replace($"{numberCulture.CurrencyDecimalSeparator}00", "");
    }
}