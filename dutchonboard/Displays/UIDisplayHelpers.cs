using dutchonboard.Core.Domain.Models;
using System.Globalization;

namespace dutchonboard.Displays;

public static class UIDisplayHelpers
{
    public static string GetDietRestrictionDisplay(this DietRestriction restriction)
    {
        return restriction switch
        {
            DietRestriction.Alcohol => "Geen Alcohol",
            DietRestriction.Vegetarian => "Vegetarisch",
            DietRestriction.Nuts => "Notenallergie",
            DietRestriction.Lactose => "Lactose-intolerantie",
            _ => restriction.ToString()
        };
    }

    public static string GetSupportDietRestrictionsAsArray(this ICollection<DietRestriction> dietRestrictions) => string.Join(" , ", dietRestrictions.Select(x => x.GetDietRestrictionDisplay()));
    
    public static string GetDateDisplay(this DateTime date) =>
        date.ToString("dd-MM-yyyy HH:mm", new CultureInfo("nl-NL"));

    public static string GetPlayerCountDisplay(this GameNight gameNight) =>
        $"{gameNight.Players.Count} / {gameNight.MaxPlayerAmount}";
}