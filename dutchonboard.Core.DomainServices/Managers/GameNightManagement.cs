namespace dutchonboard.Core.DomainServices.Managers;

public static class GameNightManagement
{
    public static void UpdateGameNightProperties(GameNight current, GameNight updated)
    {
        // BUSINESS LOGIC: Game night only allowed to update if no players have joined (excluding the organizer himself, so count is at least 1)
        if (current.Players.Count > 1)
        {
            throw new GameNightCrudException(
                "U kunt deze avond niet wijzigen, een andere speler naast uzelf heeft zich al ingeschreven");
        }

        current.Title = updated.Title;
        current.DateAndTime = updated.DateAndTime;
        current.AdultOnly = updated.AdultOnly;
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