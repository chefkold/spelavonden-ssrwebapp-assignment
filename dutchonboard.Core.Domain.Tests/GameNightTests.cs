namespace dutchonboard.Core.Domain.Tests;

public class GameNightTests
{

    [Fact]
    public void AddBoardGame_AddOneBoardGameForAdults_AdultBoardGameShouldMakeGameNightForAdults()
    {
        var gameNight = new GameNight();
        var boardGame1 = new BoardGame { IsForAdults = false };
        var boardGame2 = new BoardGame { IsForAdults = true };
        var boardGame3 = new BoardGame { IsForAdults = false };
        
        gameNight.AddBoardGame(boardGame1);
        gameNight.AddBoardGame(boardGame2);
        gameNight.AddBoardGame(boardGame3);

        Assert.True(gameNight.IsForAdults);
    }

    
    [Fact]
    public void SetBoardGames_SettingBoardGamesIncludingAnAdultBoardGame_AdultBoardGameShouldMakeGameNightForAdults()
    {
        var gameNight = new GameNight();
        var boardGames = new[]
        {
            new BoardGame { IsForAdults = false },
            new BoardGame { IsForAdults = true },
            new BoardGame { IsForAdults = false }
        };

        gameNight.Games = boardGames;

        Assert.True(gameNight.IsForAdults);
    }

    [Fact]
    public void SetBoardGames_SettingBoardGamesIncludingNoAdultBoardGame_NonAdultBoardGamesDoesNotMakeGameNightAdultOnly()
    {
        var gameNight = new GameNight();
        gameNight.IsForAdults = false;
        var boardGames = new[]
        {
            new BoardGame { IsForAdults = false },
            new BoardGame { IsForAdults = false },
            new BoardGame { IsForAdults = false }
        };

        gameNight.Games = boardGames;

        Assert.False(gameNight.IsForAdults);
    }

    [Fact]
    public void AddPlayer_AddingAnAdultPlayerToGameNightForAdults_ShouldNotThrowException()
    {
        var gameNight = new GameNight { IsForAdults = true};
        var player = new Player { BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-18)) };

        gameNight.AddPlayer(player);

        Assert.Contains(gameNight.Players, p => p == player);
    }

    [Fact]
    public void AddPlayer_AddingNonAdultPlayerToGameNightForAdults_ShouldThrowException()
    {
        var gameNight = new GameNight { IsForAdults = true };
        var player = new Player { BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-17)) };

        Assert.Throws<Exception>(() => gameNight.AddPlayer(player));
    }

    [Fact]
    public void AddPlayer_AddingPlayerToGameNightWhileMaxPlayerAmountLimitIsReached_ShouldThrowException()
    {
        var gameNight = new GameNight { MaxPlayerAmount = 1 };
        var player1 = new Player { BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-18)) };
        var player2 = new Player { BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-18)) };

        gameNight.AddPlayer(player1);

        Assert.Throws<Exception>(() => gameNight.AddPlayer(player2));
    }

    [Fact]
    public void GameNightCreation_SettingAnOrganizer_OrganizerShouldBeAddedToPlayersList()
    {
        var organizer = new Organizer(); 
        
        var gameNight = new GameNight(){Organizer = organizer};

        Assert.Contains(organizer, gameNight.Players); 
    }
}