namespace dutchonboard.Core.Domain.Models;

public class GamesNight
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Address Location { get; set; }
    public DateTime DateAndTime { get; set; }
    public User Host { get; set; }
    public ICollection<BoardGame> Games { get; set; } = new List<BoardGame>();
}