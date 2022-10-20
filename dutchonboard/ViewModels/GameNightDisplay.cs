﻿using System.Globalization;
using dutchonboard.Core.Domain.Models;

namespace dutchonboard.Models;

public static class GameNightDisplay
{
    public static string GetPlayerCountDisplay(this GameNight gameNight) =>
        $"{gameNight.Players.Count} / {gameNight.MaxPlayerAmount}";

    public static string GetDateDisplay(this GameNight gameNight) =>
        gameNight.DateAndTime.ToString(new CultureInfo("nl-NL"));

    public static string GetDietInfoDisplay(this GameNight gameNight) => string.Join(" , ", gameNight.DietAndAllergyInfo); 
}