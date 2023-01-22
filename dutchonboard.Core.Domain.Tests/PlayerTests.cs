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

    [Fact]
    public void IsAdult_PlayerIs18YearsOld_ShouldReturnTrue()
    {
        var player = new Player(DateOnly.FromDateTime(DateTime.Now).AddYears(-18));
        
        Assert.True(player.IsAdult());
    }

    [Fact]
    public void IsAdult_PlayerIs17YearsOld_ShouldReturnFalse()
    {
        var player = new Player(DateOnly.FromDateTime(DateTime.Now).AddYears(-17));
        
        Assert.False(player.IsAdult());
    }

    [Fact]
    public void GetConflictingDietRestrictions_PlayerHasNoConflictingDietRestrictions_ShouldReturnEmptyList()
    {
        var player = new Player(DateOnly.FromDateTime(DateTime.Now).AddYears(-18));
        var gameNight = new GameNight();
        var consumption = new Consumption("");
        var dietRestriction = new DietRestriction();
        consumption.DietRestrictions.Add(dietRestriction);
        gameNight.Consumptions.Add(consumption);

        var conflictingDietRestrictions = player.GetConflictingDietRestrictions(gameNight);

        Assert.Empty(conflictingDietRestrictions);
    }

    [Fact]
    public void GetConflictingDietRestrictions_PlayerHasConflictingDietRestrictions_ShouldReturnListWithConflictingDietRestrictions()
    {
        var player = new Player(DateOnly.FromDateTime(DateTime.Now).AddYears(-18));
        var gameNight = new GameNight();
        var consumption = new Consumption("");
        consumption.DietRestrictions.Add(DietRestriction.Vegetarian);
        gameNight.Consumptions.Add(consumption);
        player.DietRestrictions.Add(DietRestriction.Vegetarian);

        var conflictingDietRestrictions = player.GetConflictingDietRestrictions(gameNight);

        Assert.Contains(DietRestriction.Vegetarian, conflictingDietRestrictions);
    }
}