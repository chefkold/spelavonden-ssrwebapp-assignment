using dutchonboard.Core.DomainServices.Repositories;
using dutchonboard.Core.DomainServices.Services;

namespace dutchonboard.Core.DomainServices.Tests;

// GameService is responsible for business logic. Based on requirements, tests the business logic for GameNights
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
        const int maxPlayerAmount = 0;

        Result<GameNight> newGameNightResult = gameNightService.NewGameNight(organizer, "title", "description", false, maxPlayerAmount,
            new Address("street", 7, "city"), DateTime.Now);

        Assert.True(newGameNightResult.HasError);
    }

    [Fact]
    public void EditGameNight_MaxPlayerAmountLessThanOne_ShouldGiveErrorResult()
    {
        // Arrange block
        var mockGameNightRepo = new Mock<IGameNightRepo>();
        var mockBoardGameRepo = new Mock<IBoardGameRepo>();
        var mockOrganizerRepo = new Mock<IOrganizerRepo>();
        var mockPlayerRepo = new Mock<IPlayerRepo>();

        var gameNightService = new GameNightService(mockGameNightRepo.Object, mockBoardGameRepo.Object, mockOrganizerRepo.Object, mockPlayerRepo.Object);

        var gameNight = new GameNight()
        {
            Id = 1
        };
        mockGameNightRepo.Setup(r => r.GetGameNightById(1)).Returns(gameNight);
        const int maxPlayerAmount = 0;
        // End of arrange block

        Result editGameNightResult = gameNightService.EditGameNight(gameNight.Id, "title", "description", false, maxPlayerAmount,
            new Address("street", 7, "city"), DateTime.Now, new List<FoodAndDrinkType>(), new List<BoardGame>());

        Assert.True(editGameNightResult.HasError);
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
        const int maxPlayerAmount = 1;

        Result<GameNight> newGameNightResult = gameNightService.NewGameNight(organizer, "title", "description", false, maxPlayerAmount,
            new Address("street", 7, "city"), DateTime.Now);

        Assert.False(newGameNightResult.HasError);
    }

    [Fact]
    public void EditGameNight_MaxPlayerAmountIsOne_ShouldBeUpdated()
    {
        // Arrange block
        var mockGameNightRepo = new Mock<IGameNightRepo>();
        var mockBoardGameRepo = new Mock<IBoardGameRepo>();
        var mockOrganizerRepo = new Mock<IOrganizerRepo>();
        var mockPlayerRepo = new Mock<IPlayerRepo>();

        var gameNightService = new GameNightService(mockGameNightRepo.Object, mockBoardGameRepo.Object, mockOrganizerRepo.Object, mockPlayerRepo.Object);

        var gameNight = new GameNight()
        {
            Id = 1
        };
        mockGameNightRepo.Setup(r => r.GetGameNightById(1)).Returns(gameNight);
        const int maxPlayerAmount = 1;
        // End of arrange block

        Result editGameNightResult = gameNightService.EditGameNight(gameNight.Id, "title", "description", false, maxPlayerAmount,
            new Address("street", 7, "city"), DateTime.Now, new List<FoodAndDrinkType>(), new List<BoardGame>());

        Assert.False(editGameNightResult.HasError);
    }

    [Fact]
    public void DeleteGameNight_APlayerIsEnrolled_ShouldNotBeDeleted()
    {
        // Arrange block
        var mockGameNightRepo = new Mock<IGameNightRepo>();
        var mockBoardGameRepo = new Mock<IBoardGameRepo>();
        var mockOrganizerRepo = new Mock<IOrganizerRepo>();
        var mockPlayerRepo = new Mock<IPlayerRepo>();
        
        var gameNightService = new GameNightService(mockGameNightRepo.Object, mockBoardGameRepo.Object, mockOrganizerRepo.Object, mockPlayerRepo.Object);
        
        var gameNight = new GameNight()
        {
            Id = 1,
            Players = new List<Player>()
            {
                new Organizer(DateOnly.FromDateTime(DateTime.Now.AddYears(-18))),
                new Player(DateOnly.FromDateTime(DateTime.Now.AddYears(-16)))
            }
        };
        
        mockGameNightRepo.Setup(r => r.GetGameNightById(1)).Returns(gameNight);
        // End of arrange block

        Result deleteResult = gameNightService.DeleteGameNight(1);
        
        Assert.True(deleteResult.HasError);
    }
    
    [Fact]
    public void EditGameNight_APlayerIsEnrolled_ShouldNotBeEdited()
    {
        // Arrange block
        var mockGameNightRepo = new Mock<IGameNightRepo>();
        var mockBoardGameRepo = new Mock<IBoardGameRepo>();
        var mockOrganizerRepo = new Mock<IOrganizerRepo>();
        var mockPlayerRepo = new Mock<IPlayerRepo>();
        
        var gameNightService = new GameNightService(mockGameNightRepo.Object, mockBoardGameRepo.Object, mockOrganizerRepo.Object, mockPlayerRepo.Object);
        
        var gameNight = new GameNight()
        {
            Id = 1,
            Players = new List<Player>()
            {
                new Organizer(DateOnly.FromDateTime(DateTime.Now.AddYears(-18))),
                new Player(DateOnly.FromDateTime(DateTime.Now.AddYears(-16)))
            }
        };
        
        mockGameNightRepo.Setup(r => r.GetGameNightById(1)).Returns(gameNight);
        // End of arrange block

        Result editGameNightResult = gameNightService.EditGameNight(gameNight.Id, "title", "description", false, 2,
            new Address("street", 7, "city"), DateTime.Now, new List<FoodAndDrinkType>(), new List<BoardGame>());

        
        Assert.True(editGameNightResult.HasError);
    }


    [Fact]
    public void DeleteGameNight_NoPlayersEnrolled_ShouldBeDeleted()
    {
        // Arrange block
        var mockGameNightRepo = new Mock<IGameNightRepo>();
        var mockBoardGameRepo = new Mock<IBoardGameRepo>();
        var mockOrganizerRepo = new Mock<IOrganizerRepo>();
        var mockPlayerRepo = new Mock<IPlayerRepo>();
        
        var gameNightService = new GameNightService(mockGameNightRepo.Object, mockBoardGameRepo.Object, mockOrganizerRepo.Object, mockPlayerRepo.Object);
        
        var gameNight = new GameNight()
        {
            Id = 1,
            Players = new List<Player>()
            {
                new Organizer(DateOnly.FromDateTime(DateTime.Now.AddYears(-18))),
            }
        };
        
        mockGameNightRepo.Setup(r => r.GetGameNightById(1)).Returns(gameNight);
        // End of arrange block

        Result deleteResult = gameNightService.DeleteGameNight(1);
        
        Assert.False(deleteResult.HasError);
    }
    
    [Fact]
    public void EditGameNight_NoPlayersEnrolled_ShouldBeEdited()
    {
        // Arrange block
        var mockGameNightRepo = new Mock<IGameNightRepo>();
        var mockBoardGameRepo = new Mock<IBoardGameRepo>();
        var mockOrganizerRepo = new Mock<IOrganizerRepo>();
        var mockPlayerRepo = new Mock<IPlayerRepo>();
        
        var gameNightService = new GameNightService(mockGameNightRepo.Object, mockBoardGameRepo.Object, mockOrganizerRepo.Object, mockPlayerRepo.Object);
        
        var gameNight = new GameNight()
        {
            Id = 1,
            Players = new List<Player>()
            {
                new Organizer(DateOnly.FromDateTime(DateTime.Now.AddYears(-18))),
            }
        };
        
        mockGameNightRepo.Setup(r => r.GetGameNightById(1)).Returns(gameNight);
        // End of arrange block
        
        Result editGameNightResult = gameNightService.EditGameNight(gameNight.Id, "title", "description", false, 2,
            new Address("street", 7, "city"), DateTime.Now, new List<FoodAndDrinkType>(), new List<BoardGame>());
        
        Assert.False(editGameNightResult.HasError);
    }


    [Fact]
    public void AddBoardGames_SettingBoardGamesIncludingAnAdultBoardGame_ShouldMakeGameNightForAdults()
    {
        // Arrange block
        var mockGameNightRepo = new Mock<IGameNightRepo>();
        var mockBoardGameRepo = new Mock<IBoardGameRepo>();
        var mockOrganizerRepo = new Mock<IOrganizerRepo>();
        var mockPlayerRepo = new Mock<IPlayerRepo>();

        var gameNightService = new GameNightService(mockGameNightRepo.Object, mockBoardGameRepo.Object, mockOrganizerRepo.Object, mockPlayerRepo.Object);

        var gameNight = new GameNight()
        {
            MaxPlayerAmount = 1,
            IsForAdults = false,
        };
        var boardGames = new[]
        {
            new BoardGame { IsForAdults = false },
            new BoardGame { IsForAdults = true },
            new BoardGame { IsForAdults = false }
        };
        // End of arrange block

        gameNightService.AddBoardGames(gameNight, boardGames);

        Assert.True(gameNight.IsForAdults);
    }

    [Fact]
    public void AddBoardGames_SettingBoardGamesIncludingNoAdultBoardGame_ShouldMakeGameNightForAdults()
    {
        // Arrange block
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
        // End of arrange block

        gameNightService.AddBoardGames(gameNight, boardGames);

        Assert.False(gameNight.IsForAdults);
    }
}