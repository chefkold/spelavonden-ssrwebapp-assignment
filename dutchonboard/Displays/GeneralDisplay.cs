using dutchonboard.Core.Domain.Models;
using System.Globalization;

namespace dutchonboard.Displays;

public static class GeneralDisplay
{

    public static string GetDateDisplay(this DateTime date) =>
        date.ToString("dd-MM-yyyy HH:mm", new CultureInfo("nl-NL"));
}