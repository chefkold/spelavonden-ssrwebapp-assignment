using dutchonboard.Core.DomainServices.Repositories;
using dutchonboard.Core.DomainServices.Services;

namespace dutchonboard.Core.DomainServices.Tests;

public class GameServiceTests
{

    [Fact]
    public void NewGameNight_MaxPlayerAmountLessThanOne_ShouldGiveErrorResult()
    {
        var mockGameNightRepo = new Mock<IGameNightRepo>();
        var mockBoardGameRepo = new Mock<IBoardGameRepo>();
        var mockOrganizerRepo = new Mock<IOrganizerRepo>();
        var mockPlayerRepo = new Mock<IPlayerRepo>();
        var gameNightService = new GameNightService(mockGameNightRepo.Object, mockBoardGameRepo.Object, mockOrganizerRepo.Object, mockPlayerRepo.Object);
        var organizer = new Organizer(DateOnly.FromDateTime(DateTime.Now).AddYears(-18));

        Result<GameNight> newGameNightResult = gameNightService.NewGameNight(organizer, "title", "description", false, 0,
            new Address("street", 7, "city"), DateTime.Now);

        Assert.True(newGameNightResult.HasError);
    }

    [Fact]
    public void NewGameNight_MaxPlayerAmountIsOne_ShouldBeCreated()
    {
        var mockGameNightRepo = new Mock<IGameNightRepo>();
        var mockBoardGameRepo = new Mock<IBoardGameRepo>();
        var mockOrganizerRepo = new Mock<IOrganizerRepo>();
        var mockPlayerRepo = new Mock<IPlayerRepo>();
        var gameNightService = new GameNightService(mockGameNightRepo.Object, mockBoardGameRepo.Object, mockOrganizerRepo.Object, mockPlayerRepo.Object);
        var organizer = new Organizer(DateOnly.FromDateTime(DateTime.Now).AddYears(-18));

        Result<GameNight> newGameNightResult = gameNightService.NewGameNight(organizer, "title", "description", false, 1,
            new Address("street", 7, "city"), DateTime.Now);

        Assert.False(newGameNightResult.HasError);
    }
    [Fact]
    public void AddBoardGames_SettingBoardGamesIncludingAnAdultBoardGame_ShouldMakeGameNightForAdults()
    {
        var mockGameNightRepo = new Mock<IGameNightRepo>();
        var mockBoardGameRepo = new Mock<IBoardGameRepo>();
        var mockOrganizerRepo = new Mock<IOrganizerRepo>();
        var mockPlayerRepo = new Mock<IPlayerRepo>();
        var gameNightService = new GameNightService(mockGameNightRepo.Object, mockBoardGameRepo.Object, mockOrganizerRepo.Object, mockPlayerRepo.Object);
        var organizer = new Organizer(DateOnly.FromDateTime(DateTime.Now).AddYears(-18));
        var gameNight = new GameNight()
        {
            MaxPlayerAmount = 1,
            Organizer = organizer,
            Title = "title",
            Description = "description",
            IsForAdults = false,
            Location = new Address("street", 7, "city"),
            DateAndTime = DateTime.Now
        };
        var boardGames = new[]
        {
            new BoardGame { IsForAdults = false },
            new BoardGame { IsForAdults = true },
            new BoardGame { IsForAdults = false }
        };

        gameNightService.AddBoardGames(gameNight, boardGames);

        Assert.True(gameNight.IsForAdults);
    }

    [Fact]
    public void AddBoardGames_SettingBoardGamesIncludingNoAdultBoardGame_ShouldMakeGameNightForAdults()
    {
        var mockGameNightRepo = new Mock<IGameNightRepo>();
        var mockBoardGameRepo = new Mock<IBoardGameRepo>();
        var mockOrganizerRepo = new Mock<IOrganizerRepo>();
        var mockPlayerRepo = new Mock<IPlayerRepo>();
        var gameNightService = new GameNightService(mockGameNightRepo.Object, mockBoardGameRepo.Object, mockOrganizerRepo.Object, mockPlayerRepo.Object);
        var organizer = new Organizer(DateOnly.FromDateTime(DateTime.Now).AddYears(-18));
        var gameNight = new GameNight()
        {
            MaxPlayerAmount = 1,
            Organizer = organizer,
            Title = "title",
            Description = "description",
            IsForAdults = false,
            Location = new Address("street", 7, "city"),
            DateAndTime = DateTime.Now
        };
        var boardGames = new[]
        {
            new BoardGame { IsForAdults = false },
            new BoardGame { IsForAdults = false },
            new BoardGame { IsForAdults = false }
        };

        gameNightService.AddBoardGames(gameNight, boardGames);

        Assert.False(gameNight.IsForAdults);
    }
}