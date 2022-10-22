namespace dutchonboard.Core.Domain.Models;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<GameNight> JoinedNights = new List<GameNight>(); 

}