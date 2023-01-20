namespace dutchonboard.Core.DomainServices.Repositories;

public interface IGameNightRepo
{
    public GameNight AddGameNight(GameNight gameNight);
    public ICollection<GameNight> GetAllGameNights();
    public ICollection<GameNight> GetGameNightsJoinedBy(Player player);
    public GameNight GetGameNightById(int id);

    public void EnrollPlayer(GameNight gameNight, Player player);
    public void UpdateGameNight(GameNight gameNight);
    public void DeleteGameNight(GameNight gameNight);

}