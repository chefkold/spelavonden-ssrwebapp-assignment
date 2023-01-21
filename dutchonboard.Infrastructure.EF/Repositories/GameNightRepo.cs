using dutchonboard.Infrastructure.EF.Data;

namespace dutchonboard.Infrastructure.EF.Repositories;

public class GameNightRepo : IGameNightRepo
{
    private readonly DutchOnBoardDbContext _dbContext;

    public GameNightRepo(DutchOnBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public GameNight AddGameNight(GameNight gameNight)
    {
        var createGameNight = _dbContext.GameNights.Add(gameNight).Entity;
        _dbContext.SaveChanges();
        return createGameNight;
    }

    public ICollection<GameNight> GetAllGameNights() => _dbContext.GameNights.ToList();

    public ICollection<GameNight> GetGameNightsJoinedBy(Player player) => _dbContext.GameNights.Where(g => g.Players.Contains(player)).ToList();

    public GameNight GetGameNightById(int id)
    {

        return _dbContext.GameNights
            .Include(p => p.Players)
            .Include(p => p.Games)
            .First(p => p.Id == id);
    }

    public void EnrollPlayer(GameNight gameNight, Player player)
    {
        var currGn = _dbContext.GameNights.First(g => g.Id == gameNight.Id);
        currGn.Players.Add(player);
        _dbContext.SaveChanges();
    }

    public void UpdateGameNight(GameNight updatedGameNight)
    {
        _dbContext.Update(updatedGameNight);
        _dbContext.SaveChanges();

    }

    public void DeleteGameNight(GameNight gameNight)
    {
        _dbContext.GameNights.Remove(gameNight);
        _dbContext.SaveChanges();
    }
}