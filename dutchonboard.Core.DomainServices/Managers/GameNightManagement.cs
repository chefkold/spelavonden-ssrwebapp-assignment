namespace dutchonboard.Core.DomainServices.Managers;

/// <summary>
/// Business rule revolving around GameNight
/// </summary>
public static class GameNightManagement
{
    // Business rule: Game night only allowed to update if no players have joined (excluding the organizer himself, so count is at least 1)
    public static void PreProcessGameNightModification(GameNight gameNight)
    { 
        if (gameNight.Players.Count > 1)
        {
            throw new GameNightCrudException(
                "U kunt deze avond niet wijzigen, een andere speler naast uzelf heeft zich al ingeschreven");
        }
    }

    // Copy properties of another GameNight instance 
    public static void UpdateGameNightProperties(GameNight current, GameNight updated)
    {
        PreProcessGameNightModification(current);

        current.Title = updated.Title;
        current.DateAndTime = updated.DateAndTime;
        current.IsForAdults = updated.IsForAdults;
        current.Description = updated.Description;
        current.Location = updated.Location;
        current.MaxPlayerAmount = updated.MaxPlayerAmount;
        current.DietAndAllergyInfo = updated.DietAndAllergyInfo;
        current.Games = updated.Games;
    }

    public class GameNightCrudException : Exception
    {
        public GameNightCrudException(string message)
            : base(message)
        {
        }
    }
}