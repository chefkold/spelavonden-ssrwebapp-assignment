using System.ComponentModel.DataAnnotations;

namespace dutchonboard.Core.Domain.Models;

#nullable disable
public class GameNight
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool? IsForAdults { get; set; }
    // Business rules: the organizer is automatically a player so the playermount limit should be at least 1
    [Range(1, int.MaxValue)]
    public int MaxPlayerAmount { get; set; } = 1;
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

    private readonly ICollection<BoardGame> _games = new List<BoardGame>();
    public ICollection<BoardGame> Games
    {
        get => _games.ToList().AsReadOnly();
        set
        {
            foreach (var game in value)
            {
                // Wrapper necessary for business rule
                AddBoardGame(game);
            }
        }
    }

    // Business rule: When a game is added, it must be checked if its for adults only and if the game night is for adults only 
    // then this must be reflected in the GameNight IsAdultsOnly property itself
    public void AddBoardGame(BoardGame game)
    {
        // If one game is for adults, the gamenight is forever for adults only
        if (game.IsForAdults == true)
        {
            IsForAdults = true;
        }
        _games.Add(game);
    }
    public ICollection<FoodAndDrinkType> DietAndAllergyInfo = new List<FoodAndDrinkType>();
}