using dutchonboard.Infrastructure.EF.Data;
using Microsoft.EntityFrameworkCore;

namespace dutchonboard.Infrastructure.EF.Repositories;

public class GameNightRepo : IGameNightRepo
{
    private readonly DutchOnBoardDbContext _dbContext;

    public GameNightRepo(DutchOnBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddGameNight(GameNight gameNight)
    {
        throw new NotImplementedException();
    }

    public ICollection<GameNight> GetAllGameNights() => _dbContext.GameNights.ToList();

    public ICollection<GameNight> GetGameNightsJoinedBy(Player player)
    {
        throw new NotImplementedException();
    }

    public GameNight GetGameNightById(int id) => _dbContext.GameNights.Include(p => p.Players).First(p => p.Id == id);
    public void UpdateGameNight(GameNight gameNight)
    {
        throw new NotImplementedException();
    }

    public void DeleteGameNight(GameNight gameNight)
    {
        throw new NotImplementedException();
    }
}