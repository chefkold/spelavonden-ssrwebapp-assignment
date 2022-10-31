namespace dutchonboard.Core.Domain.Models;

#nullable disable
public class Player
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateOnly BirthDate { get; set;}
    public ICollection<GameNight> JoinedNights = new List<GameNight>();
    public bool IsAdult()
    {
        return BirthDate <= DateOnly.FromDateTime(DateTime.Now.AddYears(-18));
    }
}