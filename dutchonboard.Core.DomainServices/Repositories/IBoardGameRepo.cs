namespace dutchonboard.Core.DomainServices.Repositories;

public interface IBoardGameRepo
{
    public ICollection<BoardGame> GetAllBoardGames();
    public BoardGame GetBoardGameById(int id);
}