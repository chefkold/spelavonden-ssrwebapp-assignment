using dutchonboard.Core.DomainServices.Repositories;

namespace dutchonboard.Core.DomainServices.Services;

public interface IGameNightService
{
    public GameNight NewGameNight();
    public ICollection<GameNight> GetAllGameNights();
}