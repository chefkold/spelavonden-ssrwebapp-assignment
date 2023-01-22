
namespace dutchonboard.Core.DomainServices.Services;

public interface IGameNightService
{
    public Result<GameNight> NewGameNight(Organizer organizer, string title, string description, bool isForAdults, bool potluck, int maxPlayerAmount, Address location, DateTime dateAndTime);

    public Result EditGameNight(int id, string title, string description, bool isForAdults, int maxPlayerAmount,
        Address location, DateTime dateAndTime,
        ICollection<BoardGame> boardGames);
    public Result DeleteGameNight(int id);
    public Result AddBoardGames(GameNight gameNight, ICollection<BoardGame> boardGames);
    public Result AddPlayerToGameNight(int gameNightId, Player player);
    public Result AddConsumptionsToGameNight(int gameNightId, ICollection<Consumption> consumptions);
    public Result CommitEnrollmentOfPlayer(GameNight gameNight, Player player);
    public Result VerifyAllowedToUpdateOrDelete(GameNight gameNight);
    public void SaveNewGameNight(GameNight gameNight);
    public ICollection<GameNight> GetAllGameNights();
    public GameNight GetGameNightById(int id);
}