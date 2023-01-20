
namespace dutchonboard.Core.DomainServices.Services;

public interface IGameNightService
{
    public Result<GameNight> NewGameNight(Organizer organizer, string title, string description, bool isForAdults, int maxPlayerAmount, Address location, DateTime dateAndTime);
    public Result AddBoardGame(GameNight gameNight, BoardGame boardGame);
    public Result AddBoardGames(GameNight gameNight, ICollection<BoardGame> boardGame);

    public ICollection<GameNight> GetAllGameNights();
}