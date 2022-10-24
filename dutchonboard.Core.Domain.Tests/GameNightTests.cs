namespace dutchonboard.Core.Domain.Tests;

public class GameNightTests
{
    [Fact]
    public void GameNightCreation_SettingAnOrganizer_OrganizerShouldBeAddedToPlayersList()
    {
        var organizer = new Organizer(); 
        
        var gameNight = new GameNight(){Organizer = organizer};

        Assert.Contains(organizer, gameNight.Players); 
    }
}