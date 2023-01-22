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
            DietRestrictions = new List<DietRestriction> { DietRestriction.Nuts }

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
        byte[] gameThirtySecondsImg;
        byte[] gameCardsAgainstHumanityImg;

        var monopolyImgUrl = "https://media.s-bol.com/39Q7EWnMMX3M/550x508.jpg";
        var gameThirtySecondsUrl = "https://media.s-bol.com/JWXPMvvQXKy2/08XMNrX/1200x910.jpg";
        var gameCardsAgainstHumanityUrl = "https://media.s-bol.com/B95z8qmVOq5k/1A4JG/708x1200.jpg";

        using (var webClient = new HttpClient())
        {

            monopolyImg = await webClient.GetAsync(monopolyImgUrl).Result.Content.ReadAsByteArrayAsync();
            gameThirtySecondsImg = await webClient.GetAsync(gameThirtySecondsUrl).Result.Content.ReadAsByteArrayAsync();
            gameCardsAgainstHumanityImg = await webClient.GetAsync(gameCardsAgainstHumanityUrl).Result.Content.ReadAsByteArrayAsync();
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

        var gameThirtySeconds = new BoardGame()
        {
            Name = "30 seconds",
            Description =
                "Spectaculair partyspel voor teams. Fantastisch voor grote gezelschappen op feestjes en partijen. Je speelt in teams. Een van jullie probeert binnen 30 seconden zoveel mogelijk van de 5 begrippen op een kaartje te omschrijven. Hoe meer begrippen jullie raden, des te meer velden jullie op het speelbord vooruit mogen. Een dobbelsteenworp kan daar nog verandering in brengen. Welk team bereikt als eerste de finish? 30 SECONDS®, het uitdagende spel waarbij je snel moet denken en net zo snel moet praten, garandeert uren speelplezier met jouw familie en vrienden. Het wordt zeker supergezellig!",
            Type = "Een bord met een roadmap, dobbelstenen en een zandloper",
            Genre = Genre.Familie,
            IsForAdults = false,
            Image = gameThirtySecondsImg,
            ImageFormat = "image/jpeg"

        };

        var gameCardsAgainstHumanity = new BoardGame()
        {
            Name = "Cards Against Humanity",
            Description =
                "Cards Against Humanity is een hilarisch partyspel voor volwassenen. Het spel is gebaseerd op het populaire kaartspel Apples to Apples. Het doel van het spel is om de meest hilarische combinatie van witte en zwarte kaarten te verzinnen. De zwarte kaarten bevatten een vraag of een zin die je moet afmaken met een witte kaart. De witte kaarten bevatten een antwoord op de vraag of zin. De speler die de meest hilarische combinatie verzint, wint de ronde. Het spel is geschikt voor 4 tot 30 spelers en is geschikt voor volwassenen. Het spel is in het Engels.",
            Type = "Een doos met kaarten",
            Genre = Genre.Party,
            IsForAdults = true,
            Image = gameCardsAgainstHumanityImg,
            ImageFormat = "image/jpeg"

        };

        // Locations
        var locationAvansExplora = new Address("Lovensdijkstraat", 63, "Breda");
        var locationRandom1 = new Address("Ronde hengelstraat", 7, "Tilburg");
        var locationRandom2 = new Address("Vierkante stoelstraat", 57, "Den Bosch");

        // Consumptions
        var alcoholConsumption = new Consumption("Krat Grolsch")
        {
            DietRestrictions = new List<DietRestriction> { DietRestriction.Alcohol }
        };
        var nutsConsumption = new Consumption("Walnoten")
        {
            DietRestrictions = new List<DietRestriction> { DietRestriction.Nuts }
        };
        var lactoseConsumption = new Consumption("Melk")
        {
            DietRestrictions = new List<DietRestriction> { DietRestriction.Lactose }
        };
        var noVegetarianConsumption = new Consumption("Hamburgers")
        {
            DietRestrictions = new List<DietRestriction> { DietRestriction.Vegetarian }
        };

        _dutchOnBoardDbContext.Organizers.Add(organizer1);
        _dutchOnBoardDbContext.Players.Add(player1);
        _dutchOnBoardDbContext.Players.Add(player2);
        _dutchOnBoardDbContext.BoardGames.AddRange(gameMonopoly, gameThirtySeconds, gameCardsAgainstHumanity);

        // Game nights
        var gameNight1 = new GameNight()
        {
            Title = "Sessie 1 Avans",
            Description =
                "We gaan met een kickoff beginnen op de locatie van Avans Hogeschool. Hierbij zal ook de burgermeester aanwezig zijn om het nieuwe initiatief te vieren.",
            IsForAdults = false,
            Potluck = true,
            MaxPlayerAmount = 12,
            Location = locationAvansExplora,
            DateAndTime = new DateTime(2023, 2, 3, 19, 00, 00),
            Organizer = organizer1,
        };
        gameNight1.Players.Add(player1);
        gameNight1.Players.Add(organizer1);
        gameNight1.Games.Add(gameMonopoly);
        gameNight1.Consumptions.Add(alcoholConsumption);
        gameNight1.Consumptions.Add(nutsConsumption);
        _dutchOnBoardDbContext.GameNights.Add(gameNight1);

        var gameNight2 = new GameNight()
        {
            Title = "Avond voor fanatieken",
            Description = "Een avondje voor volwassennen met natuurlijk bier en hamburgers.",
            IsForAdults = true,
            Potluck = true,
            Location = locationRandom1,
            MaxPlayerAmount = 6,
            DateAndTime = new DateTime(2023, 2, 4, 22, 30, 00),
            Organizer = organizer1,
        };
        gameNight2.Players.Add(organizer1);
        gameNight2.Games.Add(gameCardsAgainstHumanity);
        gameNight2.Games.Add(gameThirtySeconds);
        gameNight2.Consumptions.Add(alcoholConsumption);
        gameNight2.Consumptions.Add(noVegetarianConsumption);
        _dutchOnBoardDbContext.GameNights.Add(gameNight2);

        var gameNight3 = new GameNight()
        {
            Title = "Rustig, maar gezellig",
            Description = "Gewoon een rustig, maar toch gezellige avond met elkaar.",
            IsForAdults = false,
            Potluck = true,
            Location = locationRandom2,
            MaxPlayerAmount = 2,
            DateAndTime = new DateTime(2023, 2, 3, 20, 30, 00),
            Organizer = organizer1
        };
        gameNight3.Players.Add(organizer1);
        gameNight3.Games.Add(gameMonopoly);
        gameNight3.Games.Add(gameThirtySeconds);
        gameNight3.Consumptions.Add(lactoseConsumption);
        _dutchOnBoardDbContext.GameNights.Add(gameNight3);

        await _dutchOnBoardDbContext.SaveChangesAsync();
    }
}
