using dutchonboard.Core.DomainServices.Repositories;

namespace dutchonboard.Core.DomainServices.Managers;

public class GameNightManager : IGameNightManager
{
    private readonly IGameNightRepo _iGameNightRepo;
    private readonly IBoardGameRepo _iBoardGameRepo;
    private readonly IOrganizerRepo _iOrganizerRepo;
    private readonly IPlayerRepo _iPlayerRepo;

    public GameNightManager(IGameNightRepo iGameNightRepo, IBoardGameRepo iBoardGameRepo, IOrganizerRepo iOrganizerRepo, IPlayerRepo iPlayerRepo)
    {
        _iGameNightRepo = iGameNightRepo;
        _iBoardGameRepo = iBoardGameRepo;
        _iOrganizerRepo = iOrganizerRepo;
        _iPlayerRepo = iPlayerRepo;
    }

    public ICollection<GameNight> GetAllGameNights() => _iGameNightRepo.GetAllGameNights();
    
}