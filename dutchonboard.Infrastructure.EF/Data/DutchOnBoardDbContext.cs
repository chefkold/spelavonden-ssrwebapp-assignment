using dutchonboard.Infrastructure.EF.Converters;
using Microsoft.EntityFrameworkCore;

namespace dutchonboard.Infrastructure.EF.Data;

#nullable disable
public class DutchOnBoardDbContext : DbContext
{
    public DbSet<GameNight> GameNights { get; set; }
    public DbSet<Organizer> Organizers { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DutchOnBoardDbContext(DbContextOptions<DutchOnBoardDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<GameNight>()
            .Property(e => e.DietAndAllergyInfo)
            .HasConversion(new EnumJsonConverter<FoodAndDrinkType>())
            .Metadata.SetValueComparer(new CollectionValueComparer<FoodAndDrinkType>());


        modelBuilder.Entity<GameNight>().OwnsOne<Address>(p => p.Location);

        modelBuilder.Entity<GameNight>()
            .HasMany<BoardGame>(p => p.Games)
            .WithMany(p => p.GameNightsWhereFeatured);
        modelBuilder.Entity<GameNight>()
            .HasMany<Person>(p => p.Players)
            .WithMany(p => p.JoinedNights);
        modelBuilder.Entity<GameNight>()
            .HasOne<Organizer>(p => p.Host)
            .WithMany(p => p.HostedNights);
        base.OnModelCreating(modelBuilder);
    }
}