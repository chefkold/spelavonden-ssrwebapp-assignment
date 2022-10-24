namespace dutchonboard.Core.Domain.Models;

#nullable disable
public class GameNight
{

    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool? AdultOnly { get; set; }
    public int MaxPlayerAmount { get; set; }
    public Address Location { get; set; }
    public DateTime DateAndTime { get; set; }
    private Organizer _organizer;

    public Organizer Organizer
    {
        get => _organizer;
        init
        {
            _organizer = value;
            Players.Add(_organizer);
        }
    }

    public ICollection<Player> Players { get; set; } = new List<Player>();
    public ICollection<BoardGame> Games { get; set; } = new List<BoardGame>();
    public ICollection<FoodAndDrinkType> DietAndAllergyInfo = new List<FoodAndDrinkType>();

}