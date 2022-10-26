using System.Globalization;
using dutchonboard.Core.Domain.Models;

namespace dutchonboard.Displays;

public static class GameNightDisplays
{
    public static string GetPlayerCountDisplay(this GameNight gameNight) =>
        $"{gameNight.Players.Count} / {gameNight.MaxPlayerAmount}";

    public static string GetDietInfoDisplay(this GameNight gameNight) => string.Join(" , ", gameNight.DietAndAllergyInfo);
}