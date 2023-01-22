using System.ComponentModel.DataAnnotations;

namespace dutchonboard.ViewModels;

/// <summary>
/// ViewModel to represent game nights in situations where one is created or edited. In case of editing,
/// this model keeps track of the game night ID as a best practice solution for supplying a POST form with multiple parameters
/// </summary>
public class GameNightFormViewModel
{
    [Required(ErrorMessage = "Geef de avond alstublieft een titel"), StringLength(30, ErrorMessage = "Houd de titel beknopt (maximaal 30 karakters")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Geef alstublieft aan of de avond 18+ is")]
    public bool IsAdultsOnly { get; set; }
    [Required(ErrorMessage = "Geef alstublieft aan of snacks moeten worden meegenomen")]
    public bool Potluck { get; set; }

    [Required(ErrorMessage = "Geef de avond alstublieft een beschrijving"), StringLength(400, ErrorMessage = "Maak uw beschrijving korter (maximaal 400 karakters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Geef alstublieft de datum en tijd van de avond")]
    public DateTime? DateAndTime { get; set; }

    [Required(ErrorMessage = "Geef alstublieft een waarde op"), Range(1, int.MaxValue, ErrorMessage = "Aantal spelers is minimaal 1")]
    public int? MaxPlayerAmount { get; set; }

    [Required(ErrorMessage = "Geef alstublieft een waarde op")]
    public string? Street { get; set; }

    [Required(ErrorMessage = "Geef alstublieft een waarde op")]
    public int? HouseNumber { get; set; }

    [Required(ErrorMessage = "Geef alstublieft een waarde op")]
    public string? City { get; set; }

    public BoardGamesDropdown GamesDropdown { get; set; } = new BoardGamesDropdown();

    public int UpdatedGameNightId { get; set; }

    public void FillGameNightData(GameNight data)
    {
        UpdatedGameNightId = data.Id;

        Title = data.Title;
        IsAdultsOnly = data.IsForAdults;
        Description = data.Description;
        DateAndTime = data.DateAndTime;
        MaxPlayerAmount = data.MaxPlayerAmount;
        Street = data.Location.Street;
        HouseNumber = data.Location.Number;
        City = data.Location.City;
    }

    public Address CreateAddress()
    {
        return new Address(Street!, HouseNumber!.Value, City!);
    }
    public class BoardGamesDropdown
    {
        public ICollection<BoardGame>? ChoosableBoardGames { get; set; } = new List<BoardGame>();

        public List<string> ChosenBoardGames { get; set; } = new List<string>();
    }
}

