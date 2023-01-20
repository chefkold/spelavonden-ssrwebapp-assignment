
namespace dutchonboard.Core.DomainServices.Services;

public interface IGameNightService
{
    public Result<GameNight> NewGameNight(Organizer organizer, string title, string description, bool isForAdults, int maxPlayerAmount, Address location, DateTime dateAndTime);

    public Result editGameNight(int id, string title, string description, bool isForAdults, int maxPlayerAmount,
        Address location, DateTime dateAndTime, ICollection<FoodAndDrinkType> dietAndAllergyInfo,
        ICollection<BoardGame> boardGames); 
    public Result AddBoardGames(GameNight gameNight, ICollection<BoardGame> boardGames);

    public void SaveNewGameNight(GameNight gameNight);
    public ICollection<GameNight> GetAllGameNights();
}