namespace dutchonboard.Core.Domain.Models;

#nullable disable
public class Player
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateOnly BirthDate { get; }
    public ICollection<GameNight> JoinedNights = new List<GameNight>();

    public Player(DateOnly birthDate)
    {
        // Check if the person is at least 16 years old
        if (birthDate.AddYears(16) >  DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ArgumentException("Player must be at least 16 years old");
        }

        // Check if the birthday is not in the future
        if (birthDate > DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ArgumentException("Birthday cannot be in the future");
        }

        BirthDate = birthDate;
    }
    public bool IsAdult()
    {
        return BirthDate <= DateOnly.FromDateTime(DateTime.Now.AddYears(-18));
    }
}