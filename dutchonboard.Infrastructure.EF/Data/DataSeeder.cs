using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace dutchonboard.Infrastructure.EF.Data;

public class DataSeeder
{
    private readonly DutchOnBoardDbContext _dutchOnBoardDbContext;

    public DataSeeder(DutchOnBoardDbContext dutchOnBoardDbContext)
    {
        _dutchOnBoardDbContext = dutchOnBoardDbContext;
    }

    public async Task Seed()
    {

        if (_dutchOnBoardDbContext.GameNights.Any()) return;

        // Users
        var organizer1 = new Organizer(DateOnly.FromDateTime(DateTime.Now.AddYears(-50)))
        {
            FirstName = UserSeedData.Organizer1FirstName,
            LastName = UserSeedData.Organizer1LastName,
            Email = UserSeedData.Organizer1Email,
            Gender = Gender.V,
            Address = new Address("Professor Cobbenhagenlaan", 11, "Tilburg"),
        };

        var player1 = new Player(DateOnly.FromDateTime(DateTime.Now.AddYears(-18)))
        {
            FirstName = UserSeedData.Player1FirstName,
            LastName = UserSeedData.Player1LastName,
            Email = UserSeedData.Player1Email,
            Gender = Gender.M,
            Address = new Address("Lovensdijkstraat", 63, "Breda"),
            DietRestrictions = new List<DietRestriction> { DietRestriction.Lactose }

        };

        var player2 = new Player(DateOnly.FromDateTime(DateTime.Now.AddYears(-17)))
        {
            FirstName = UserSeedData.Player2FirstName,
            LastName = UserSeedData.Player2LastName,
            Email = UserSeedData.Player2Email,
            Gender = Gender.M,
            Address = new Address("Lovensdijkstraat", 63, "Breda"),
            DietRestrictions = new List<DietRestriction>() { DietRestriction.Alcohol }
        };

        byte[] monopolyImg;
        var someUrl = "https://media.s-bol.com/39Q7EWnMMX3M/550x508.jpg";

        using (var webClient = new HttpClient())
        {

            monopolyImg = await webClient.GetAsync(someUrl).Result.Content.ReadAsByteArrayAsync();
        }
        // Board games
        var gameMonopoly = new BoardGame()
        {
            Name = "Monopoly",
            Description = "Er kan maar één winnaar zijn bij Monopoly Classic. Ben jij degene die straks alles bezit? Het enige echte Monopoly bordspel staat al jaren garant voor veel speelplezier met familie en vrienden en is de klassieker onder de spellen. Bij veel mensen zal dit spel nostalgie opwekken, maar het is ook nog steeds een fantastisch spel om voor de eerste keer te spelen.\r\n",
            Type = "Een bord met een roadmap, dobbelstenen en items",
            Genre = Genre.Strategie,
            IsForAdults = false,
            Image = monopolyImg,
            ImageFormat = "image/jpeg"

        };

        // Locations
        var locationAvansExplora = new Address("Lovensdijkstraat", 63, "Breda");

        _dutchOnBoardDbContext.Organizers.Add(organizer1);
        _dutchOnBoardDbContext.Players.Add(player1);
        _dutchOnBoardDbContext.Players.Add(player2);
        _dutchOnBoardDbContext.BoardGames.Add(gameMonopoly);

        // Game nights
        var gameNight1 = new GameNight()
        {
            Title = "Sessie 1 Avans",
            Description =
                "We gaan met een kickoff beginnen op de locatie van Avans Hogeschool. Hierbij zal ook de burgermeester aanwezig zijn om het nieuwe initiatief te vieren",
            IsForAdults = false,
            MaxPlayerAmount = 12,
            Location = locationAvansExplora,
            DateAndTime = new DateTime(2022, 10, 31, 23, 59, 00),
            Organizer = organizer1,
        };

        gameNight1.Players.Add(player1);
        gameNight1.Players.Add(organizer1);

        gameNight1.Games.Add(gameMonopoly);
        gameNight1.SupportedDietRestrictions.Add(DietRestriction.Alcohol);
        gameNight1.SupportedDietRestrictions.Add(DietRestriction.Nuts);

        _dutchOnBoardDbContext.GameNights.Add(gameNight1);
        _dutchOnBoardDbContext.SaveChanges();
    }
}