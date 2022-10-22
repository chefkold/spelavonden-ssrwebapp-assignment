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

        var organizerHenk = new Organizer() { Name = "Henk" };
        var playerDonat = new Player() {Name = "Donat"};
        var gameMonopoly = new BoardGame() {Name = "Monopoly"};
        var locationAvansExplora = new Address("Lovensdijkstraat", 63);

        _dutchOnBoardDbContext.Organizers.Add(organizerHenk);
        _dutchOnBoardDbContext.Players.Add(playerDonat);
        _dutchOnBoardDbContext.BoardGames.Add(gameMonopoly); 

        var gameNight1 = new GameNight()
        {
            Title = "Sessie 1 Avans",
            Description =
                "We gaan met een kickoff beginnen op de locatie van Avans Hogeschool. Hierbij zal ook de burgermeester aanwezig zijn om het nieuwe initiatief te vieren",
            AdultOnly = false,
            MaxPlayerAmount = 12,
            Location = locationAvansExplora,
            DateAndTime = new DateTime(2022, 10, 31, 23, 59, 00),
            Host = organizerHenk,
        };

        gameNight1.Players.Add(playerDonat);
        gameNight1.Games.Add(gameMonopoly);
        gameNight1.DietAndAllergyInfo.Add(FoodAndDrinkType.TreeNuts);
        gameNight1.DietAndAllergyInfo.Add(FoodAndDrinkType.Lactose);

        _dutchOnBoardDbContext.GameNights.Add(gameNight1);

        _dutchOnBoardDbContext.SaveChanges();
    }
}