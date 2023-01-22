using dutchonboard.Infrastructure.EF.Converters;
using Microsoft.EntityFrameworkCore;

namespace dutchonboard.Infrastructure.EF.Data;

#nullable disable
public class DutchOnBoardDbContext : DbContext
{
    public DbSet<GameNight> GameNights { get; set; }
    public DbSet<BoardGame> BoardGames { get; set; }
    public DbSet<Organizer> Organizers { get; set; }
    public DbSet<Player> Players { get; set; }

    public DutchOnBoardDbContext(DbContextOptions<DutchOnBoardDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //DDL
        modelBuilder
            .Entity<Consumption>()
            .Property(e => e.DietRestrictions)
            .HasConversion(new EnumJsonConverter<DietRestriction>())
            .Metadata.SetValueComparer(new CollectionValueComparer<DietRestriction>());

        modelBuilder
            .Entity<Player>()
            .Property(p => p.DietRestrictions)
            .HasConversion(new EnumJsonConverter<DietRestriction>())
            .Metadata.SetValueComparer(new CollectionValueComparer<DietRestriction>());
        
        modelBuilder
            .Entity<Player>()
            .Property(p => p.BirthDate)
            .HasConversion<DateOnlyConverter, DateOnlyComparer>();

        modelBuilder.Entity<GameNight>().OwnsOne<Address>(p => p.Location);
        modelBuilder.Entity<Player>().OwnsOne<Address>(p => p.Address);


        modelBuilder.Entity<GameNight>()
            .HasMany<BoardGame>(p => p.Games)
            .WithMany(p => p.GameNightsWhereFeatured);

        modelBuilder.Entity<GameNight>()
            .HasMany<Consumption>(p => p.Consumptions)
            .WithMany(p => p.GameNightsWhereConsumed);

        modelBuilder.Entity<GameNight>()
            .HasMany<Player>(p => p.Players)
            .WithMany(p => p.JoinedNights);

        modelBuilder.Entity<GameNight>()
            .HasOne<Organizer>(p => p.Organizer)
            .WithMany(p => p.HostedNights);
        

    }
}