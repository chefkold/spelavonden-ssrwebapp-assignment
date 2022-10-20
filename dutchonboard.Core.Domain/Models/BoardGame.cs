namespace dutchonboard.Core.Domain.Models;

public class BoardGame
{
    
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<GameNight> GameNightsWhereFeatured { get; set; } = new List<GameNight>();
}