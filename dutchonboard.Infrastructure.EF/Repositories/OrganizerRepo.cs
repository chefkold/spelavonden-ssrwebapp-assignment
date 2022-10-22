using dutchonboard.Infrastructure.EF.Data;
using Microsoft.EntityFrameworkCore;

namespace dutchonboard.Infrastructure.EF.Repositories;

public class OrganizerRepo : IOrganizerRepo
{
    private readonly DutchOnBoardDbContext _dbContext;

    public OrganizerRepo(DutchOnBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Organizer GetOrganizerByEmail(string email)
    {
        return _dbContext.Organizers
            .Include(p => p.HostedNights)
            .First(p => p.Email.ToLower().Equals(email.ToLower()));
    }
}