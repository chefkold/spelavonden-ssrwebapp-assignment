namespace dutchonboard.Core.Domain.Tests;

public class GameNightTests
{

    
    [Fact]
    public void AddPlayer_AddingAnAdultPlayerToGameNightForAdults_ShouldNotThrowException()
    {
        var gameNight = new GameNight { IsForAdults = true };
        var player = new Player(DateOnly.FromDateTime(DateTime.Now.AddYears(-18)));

        gameNight.AddPlayer(player);

        Assert.Contains(gameNight.Players, p => p == player);
    }

    [Fact]
    public void AddPlayer_AddingNonAdultPlayerToGameNightForAdults_ShouldThrowException()
    {
        var gameNight = new GameNight { IsForAdults = true };
        var player = new Player(DateOnly.FromDateTime(DateTime.Now.AddYears(-17))); 
        Assert.Throws<Exception>(() => gameNight.AddPlayer(player));
    }

    [Fact]
    public void AddPlayer_AddingPlayerToGameNightWhileMaxPlayerAmountLimitIsReached_ShouldThrowException()
    {
        var gameNight = new GameNight { MaxPlayerAmount = 1 };
        var player1 = new Player(DateOnly.FromDateTime(DateTime.Now.AddYears(-18)));
        var player2 = new Player(DateOnly.FromDateTime(DateTime.Now.AddYears(-18)));

        gameNight.AddPlayer(player1);

        Assert.Throws<Exception>(() => gameNight.AddPlayer(player2));
    }

    [Fact]
    public void AddPlayer_NewPlayerToNightWhileAlreadyEnrolledToOtherNightToday_ShouldThrowException()
    {
        var gameNight1 = new GameNight { MaxPlayerAmount = 1 };
        var gameNight2 = new GameNight { MaxPlayerAmount = 1 };
        var player = new Player(DateOnly.FromDateTime(DateTime.Now.AddYears(-18)));
        player.JoinedNights.Add(gameNight1);
        
        Assert.Throws<Exception>(() => gameNight2.AddPlayer(player));
    }

    [Fact]
    public void GameNightCreation_SettingAnOrganizer_ShouldBeAddedToPlayersList()
    {
        var organizer = new Organizer(DateOnly.FromDateTime(DateTime.Now.AddYears(-20)));

        var gameNight = new GameNight() { Organizer = organizer };

        Assert.Contains(organizer, gameNight.Players);
    }
}