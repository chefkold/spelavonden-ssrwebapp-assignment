namespace dutchonboard.Core.Domain.Models;

public class Organizer : Player
{
    public ICollection<GameNight> HostedNights = new List<GameNight>();

    // Business rule: someone with a birthday less than 18 years ago cannot be on organizer
    public Organizer(DateOnly birthDate) : base(birthDate)
    {
        if (birthDate.AddYears(18) > DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ArgumentException("Organizer must be at least 18 years old");
        }

    }
}