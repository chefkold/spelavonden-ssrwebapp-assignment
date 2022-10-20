namespace dutchonboard.Core.Domain.Models;

public class Organizer : Person
{

    public ICollection<GameNight> HostedNights = new List<GameNight>();
}