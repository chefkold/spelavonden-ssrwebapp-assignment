namespace dutchonboard.Core.Domain.Models;

#nullable disable
public class GameNight
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int MaxPlayerAmount { get; set; }
    public bool Potluck { get; set; }
    public bool IsForAdults { get; set; }
    public Organizer Organizer { get; set; }
    public Address Location { get; set; }
    public DateTime DateAndTime { get; set; }
    public ICollection<BoardGame> Games { get; set; } = new List<BoardGame>();
    public ICollection<Player> Players { get; set; } = new List<Player>();
    public ICollection<Consumption> Consumptions { get; set; } = new List<Consumption>();
}