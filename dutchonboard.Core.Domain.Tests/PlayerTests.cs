namespace dutchonboard.Core.Domain.Tests;

public class PlayerTests
{
    [Fact]
    public void NewPlayer_15YearsOld_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() => new Player(DateOnly.FromDateTime(DateTime.Now).AddYears(-15)));
    }

    [Fact]
    public void NewPlayer_16YearsOld_ShouldBeCreated()
    {
        try
        {
            var player = new Player(DateOnly.FromDateTime(DateTime.Now).AddYears(-16));
        }
        catch (ArgumentException)
        {
            Assert.True(false, "Player not created");
        }
    }

    [Fact]
    public void NewPlayer_BirthdayInFuture_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() => new Player(DateOnly.FromDateTime(DateTime.Now).AddYears(1)));
    }
}