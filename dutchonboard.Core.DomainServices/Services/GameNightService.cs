using dutchonboard.Core.DomainServices.Repositories;

namespace dutchonboard.Core.DomainServices.Services;

public class GameNightService : IGameNightService
{
    private readonly IGameNightRepo _iGameNightRepo;
    private readonly IBoardGameRepo _iBoardGameRepo;
    private readonly IOrganizerRepo _iOrganizerRepo;
    private readonly IPlayerRepo _iPlayerRepo;

    public GameNightService(IGameNightRepo iGameNightRepo, IBoardGameRepo iBoardGameRepo, IOrganizerRepo iOrganizerRepo, IPlayerRepo iPlayerRepo)
    {
        _iGameNightRepo = iGameNightRepo;
        _iBoardGameRepo = iBoardGameRepo;
        _iOrganizerRepo = iOrganizerRepo;
        _iPlayerRepo = iPlayerRepo;
    }

    public ICollection<GameNight> GetAllGameNights() => _iGameNightRepo.GetAllGameNights();
    
}