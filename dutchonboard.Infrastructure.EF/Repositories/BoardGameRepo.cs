using dutchonboard.Infrastructure.EF.Data;

namespace dutchonboard.Infrastructure.EF.Repositories;

public class BoardGameRepo : IBoardGameRepo
{
    private readonly DutchOnBoardDbContext _dbContext;

    public BoardGameRepo(DutchOnBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ICollection<BoardGame> GetAllBoardGames() => _dbContext.BoardGames.ToList();

    public BoardGame GetBoardGameById(int id) => _dbContext.BoardGames.First(p => p.Id == id); 
}