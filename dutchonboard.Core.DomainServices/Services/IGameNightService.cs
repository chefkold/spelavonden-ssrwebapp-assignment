
namespace dutchonboard.Core.DomainServices.Services;

public interface IGameNightService
{
    public Result<GameNight> NewGameNight(Organizer organizer, string title, string description, bool isForAdults, int maxPlayerAmount, Address location, DateTime dateAndTime, ICollection<DietRestriction> supportedDietRestrictions);

    public Result EditGameNight(int id, string title, string description, bool isForAdults, int maxPlayerAmount,
        Address location, DateTime dateAndTime, ICollection<DietRestriction> supportedDietRestrictions,
        ICollection<BoardGame> boardGames);
    public Result DeleteGameNight(int id);
    public Result AddBoardGames(GameNight gameNight, ICollection<BoardGame> boardGames);
    public Result AddPlayerToGameNight(int gameNightId, Player player);
    public void SaveNewGameNight(GameNight gameNight);
    public ICollection<GameNight> GetAllGameNights();
    public GameNight GetGameNightById(int id);
}