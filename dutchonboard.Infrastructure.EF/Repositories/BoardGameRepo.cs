using dutchonboard.Infrastructure.EF.Data;

namespace dutchonboard.Infrastructure.EF.Repositories;

public class BoardGameRepo : IBoardGameRepo
{
    private readonly DutchOnBoardDbContext _dbContext;

    public BoardGameRepo(DutchOnBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ICollection<BoardGame> GetAllBoardGames() => _dbContext.BoardGames.Include(b => b.GameNightsWhereFeatured).ToList();

    public BoardGame GetBoardGameById(int id) => _dbContext.BoardGames.First(p => p.Id == id);
    public void UpdateBoardgame(BoardGame boardGame)
    {
        var currBoardGame = _dbContext.BoardGames.First(p => p.Id == boardGame.Id);
        currBoardGame.GameNightsWhereFeatured = boardGame.GameNightsWhereFeatured;
        _dbContext.SaveChanges();
    }
}