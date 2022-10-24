namespace dutchonboard.Infrastructure.EF.Data;

public class DataSeeder
{
    private readonly DutchOnBoardDbContext _dutchOnBoardDbContext;

    public DataSeeder(DutchOnBoardDbContext dutchOnBoardDbContext)
    {
        _dutchOnBoardDbContext = dutchOnBoardDbContext;
    }

    public void Seed()
    {
        if (_dutchOnBoardDbContext.GameNights.Any()) return;

        // Users
        var organizer1 = new Organizer()
        {
            FirstName = UserSeedData.Organizer1FirstName,
            LastName = UserSeedData.Organizer1LastName,
            Email = UserSeedData.Organizer1Email
        };

        var player1 = new Player()
        {
            FirstName = UserSeedData.Player1FirstName,
            LastName = UserSeedData.Player1LastName,
            Email = UserSeedData.Player1Email
        };

        // Board games
        var gameMonopoly = new BoardGame() { Name = "Monopoly" };

        // Locations
        var locationAvansExplora = new Address("Lovensdijkstraat", 63, "Breda");

        _dutchOnBoardDbContext.Organizers.Add(organizer1);
        _dutchOnBoardDbContext.Players.Add(player1);
        _dutchOnBoardDbContext.BoardGames.Add(gameMonopoly);

        // Game nights
        var gameNight1 = new GameNight()
        {
            Title = "Sessie 1 Avans",
            Description =
                "We gaan met een kickoff beginnen op de locatie van Avans Hogeschool. Hierbij zal ook de burgermeester aanwezig zijn om het nieuwe initiatief te vieren",
            AdultOnly = false,
            MaxPlayerAmount = 12,
            Location = locationAvansExplora,
            DateAndTime = new DateTime(2022, 10, 31, 23, 59, 00),
            Organizer = organizer1,
        };

        gameNight1.Players.Add(player1);
        gameNight1.Games.Add(gameMonopoly);
        gameNight1.DietAndAllergyInfo.Add(FoodAndDrinkType.TreeNuts);
        gameNight1.DietAndAllergyInfo.Add(FoodAndDrinkType.Lactose);

        _dutchOnBoardDbContext.GameNights.Add(gameNight1);
        _dutchOnBoardDbContext.SaveChanges();
    }
}