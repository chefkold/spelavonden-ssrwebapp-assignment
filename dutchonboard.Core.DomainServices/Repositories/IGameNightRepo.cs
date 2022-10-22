using dutchonboard.Core.Domain.Models;

namespace dutchonboard.Core.DomainServices.Repositories;

public interface IGameNightRepo
{
    public abstract void AddGameNight(GameNight gameNight); 
    public abstract ICollection<GameNight> GetAllGameNights();
    public abstract ICollection<GameNight> GetGameNightsHostedBy(Organizer host);
    public abstract ICollection<GameNight> GetGameNightsJoinedBy(Player player);
    public abstract GameNight GetGameNightById(int id);
    public abstract void UpdateGameNight(GameNight gameNight); 
    public abstract void DeleteGameNight(GameNight gameNight);
}