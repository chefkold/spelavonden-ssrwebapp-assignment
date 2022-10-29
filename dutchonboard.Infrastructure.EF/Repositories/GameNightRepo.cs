using dutchonboard.Infrastructure.EF.Data;

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
        _dbContext.GameNights.Add(gameNight);
        _dbContext.SaveChanges();
    }

    public ICollection<GameNight> GetAllGameNights() => _dbContext.GameNights.ToList();

    public ICollection<GameNight> GetGameNightsJoinedBy(Player player)
    {
        throw new NotImplementedException();
    }

    public GameNight GetGameNightById(int id)
    {

        return _dbContext.GameNights
            .Include(p => p.Players)
            .Include(p => p.Games)
            .First(p => p.Id == id);
    }

    public void UpdateGameNight(GameNight updatedGameNight)
    {
        var currentGameNight = GetGameNightById(updatedGameNight.Id);
        GameNightManagement.UpdateGameNightProperties(currentGameNight, updatedGameNight);
        _dbContext.SaveChanges();

    }

    public void DeleteGameNight(GameNight gameNight)
    {
        GameNightManagement.PreProcessGameNightModification(gameNight);
        _dbContext.GameNights.Remove(gameNight);
        _dbContext.SaveChanges();
    }
}