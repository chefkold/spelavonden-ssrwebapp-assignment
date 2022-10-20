namespace dutchonboard.Core.Domain.Tests;

public class GameNightTests
{
    [Fact]
    public void GameNightCreation_SettingAHost_HostShouldBeAddedToPlayersList()
    {
        var host = new Organizer(); 
        
        var gameNight = new GameNight(){Host = host};

        Assert.Contains(host, gameNight.Players); 
    }
}