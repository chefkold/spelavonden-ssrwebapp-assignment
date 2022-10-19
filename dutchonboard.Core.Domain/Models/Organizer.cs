namespace dutchonboard.Core.Domain.Models;

public class Organizer : Person
{
    public Organizer(int id, string name) : base(id, name)
    {
    }

    public ICollection<GameNight> HostedNights = new List<GameNight>();
}