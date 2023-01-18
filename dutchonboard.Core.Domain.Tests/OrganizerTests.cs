namespace dutchonboard.Core.Domain.Tests;

public class OrganizerTests
{

    [Fact]
    public void NewOrganizer_17YearsOld_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() => new Organizer(DateOnly.FromDateTime(DateTime.Now).AddYears(-17)));
    }

    [Fact]
    public void NewOrganizer_18YearsOld_ShouldBeCreated()
    {
        try
        {
            var player = new Organizer(DateOnly.FromDateTime(DateTime.Now).AddYears(-18));
        }
        catch (ArgumentException)
        {
            Assert.True(false, "Organizer not created");
        }
    }
}