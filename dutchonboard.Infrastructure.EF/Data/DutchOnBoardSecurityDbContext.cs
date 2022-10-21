using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dutchonboard.Infrastructure.EF.Data;

public class DutchOnBoardSecurityDbContext : IdentityDbContext
{
    public DutchOnBoardSecurityDbContext(DbContextOptions<DutchOnBoardSecurityDbContext> options) : base(options)
    {


    }
}
