using System.Globalization;

namespace EconomyPlanner.Web.Helpers;

public static class NumberCultureHelper
{
    public static NumberFormatInfo GetNumberCulture()
    {
        var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
        nfi.NumberGroupSeparator = " ";
        return nfi;
    }
}