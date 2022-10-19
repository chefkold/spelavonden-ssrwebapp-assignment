namespace dutchonboard.Core.Domain.Models;

public class GameNight
{
    public GameNight(int id, string title, string description, bool adultOnly, int maxPlayerAmount, Address location, DateTime dateAndTime, Organizer host)
    {
        Id = id;
        Title = title;
        Description = description;
        AdultOnly = adultOnly;
        MaxPlayerAmount = maxPlayerAmount;
        Location = location;
        DateAndTime = dateAndTime;
        Host = host;
    }

    public int Id { get; set; } 
    public string Title { get; set; }
    public string Description { get; set; }
    public bool AdultOnly { get; set; }
    public int MaxPlayerAmount { get; set; }
    public Address Location { get; set; }
    public DateTime DateAndTime { get; set; }
    public Organizer Host { get; set; }
    public ICollection<Person> Players { get; set; } = new List<Person>(); 
    public ICollection<BoardGame> Games { get; set; } = new List<BoardGame>();
    public ICollection<FoodAndDrinkTypes> DietAndAllergyInfo = new List<FoodAndDrinkTypes>(); 
}