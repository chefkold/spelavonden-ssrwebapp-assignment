using dutchonboard.Core.DomainServices.Repositories;

namespace dutchonboard.Core.DomainServices.Managers;

public interface IGameNightManager
{
    public ICollection<GameNight> GetAllGameNights();
}