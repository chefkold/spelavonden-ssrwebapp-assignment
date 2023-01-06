namespace dutchonboard.Core.Domain.Models;

public class Organizer : Player
{
    public ICollection<GameNight> HostedNights = new List<GameNight>();

    public Organizer(DateOnly birthDate) : base(birthDate)
    {
    }
}