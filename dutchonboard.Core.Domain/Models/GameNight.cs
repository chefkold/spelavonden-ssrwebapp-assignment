using System.ComponentModel.DataAnnotations;

namespace dutchonboard.Core.Domain.Models;

#nullable disable
public class GameNight
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsForAdults { get; set; }
    public int MaxPlayerAmount { get; set; }
    public Address Location { get; set; }
    public DateTime DateAndTime { get; set; }

    private Organizer _organizer;
    public Organizer Organizer
    {
        get => _organizer;
        set
        {
            _organizer = value;
            AddPlayer(_organizer);
        }
    }


    private readonly ICollection<Player> _players = new List<Player>();

    public ICollection<Player> Players
    {
        get => _players.ToList().AsReadOnly();
        set
        {
            foreach (var player in value)
            {
                // Wrapper necessary for business rule
                AddPlayer(player);
            }
        }
    }
    // Business rule: if a game night is for adults only, a player cannot join
    // Business rule: if a game night maximum player count is met, a player cannot join
    // Business rule: if a player is already enrolled to a game night for this game night's date, a player cannot join
    public void AddPlayer(Player player)
    {
        if (MaxPlayerAmount <= _players.Count)
        {
            throw new Exception("Het limiet van maximaal aantal spelers is al bereikt.");

        }

        if (IsForAdults == true && !player.IsAdult())
        {
            throw new Exception("Deze avond is voor alleen voor volwassenen!");
        }

        // A player cannot be added if he already is enrolled for a game night on this date
        if (player.JoinedNights.Any(g =>
                DateOnly.FromDateTime(g.DateAndTime).Equals(DateOnly.FromDateTime(this.DateAndTime))))
        {
            
            throw new Exception("U bent al ingeschreven op een bordspellenavond vandaag!");

        }

        _players.Add(player);
    }

   public ICollection<BoardGame> Games { get; set; } = new List<BoardGame>();

    public ICollection<FoodAndDrinkType> DietAndAllergyInfo = new List<FoodAndDrinkType>();
}