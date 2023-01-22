using dutchonboard.Infrastructure.EF.Data;
using Microsoft.EntityFrameworkCore;

namespace dutchonboard.Infrastructure.EF.Repositories;

public class PlayerRepo : IPlayerRepo
{
    private readonly DutchOnBoardDbContext _dbContext;

    public PlayerRepo(DutchOnBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Player GetPlayerByEmail(string email)
    {
        return _dbContext.Players
            .Include(p => p.JoinedNights)
            .First(p => p.Email.ToLower().Equals(email.ToLower()));
    }

    public Player GetPlayerById(int id)
    {
        return _dbContext.Players
            .Include(p => p.JoinedNights)
            .First(p => p.Id == id);
    }
}