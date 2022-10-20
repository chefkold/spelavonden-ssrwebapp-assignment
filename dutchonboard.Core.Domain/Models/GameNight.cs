namespace dutchonboard.Core.Domain.Models;

public class GameNight
{

    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool AdultOnly { get; set; }
    public int MaxPlayerAmount { get; set; }
    public Address Location { get; set; }
    public DateTime DateAndTime { get; set; }
    private Organizer _host;

    public Organizer Host
    {
        get => _host;
        init
        {
            _host = value;
            Players.Add(_host);
        }
    }

    public ICollection<Person> Players { get; set; } = new List<Person>();
    public ICollection<BoardGame> Games { get; set; } = new List<BoardGame>();
    public ICollection<FoodAndDrinkType> DietAndAllergyInfo = new List<FoodAndDrinkType>();

}