using dutchonboard.Core.Domain.Models;
using System.Globalization;

namespace dutchonboard.Displays;

public static class GeneralDisplay
{
    public static string GetDateDisplay(this DateTime date) =>
        date.ToString(new CultureInfo("nl-NL"));
}