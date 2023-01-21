using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dutchonboard.Models;

/// <summary>
/// ViewModel to represent game nights in situations where one is created or edited. In case of editing,
/// this model keeps track of the game night ID as a best practice solution for supplying a POST form with multiple parameters
/// </summary>
public class GameNightViewModel
{
    [Required(ErrorMessage = "Geef de avond alstublieft een titel"), StringLength(30, ErrorMessage = "Houd de titel beknopt (maximaal 30 karakters")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Geef alstublieft aan ofe de avond 18+ is")]
    public bool IsAdultsOnly { get; set; }

    [Required(ErrorMessage = "Geef de avond alstublieft een beschrijving"), StringLength(150, ErrorMessage = "Maak uw beschrijving korter (maximaal 400 karakters")]
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

    [Required(ErrorMessage = "Geef alstublieft een waarde op")]
    public ICollection<DietRestriction> SupportedDietRestrictions { get; set; } =
        Enum.GetValues(typeof(DietRestriction)).Cast<DietRestriction>().ToList();

    // Holding diet restrictions of current game night if viewmodel is used for updating
    public ICollection<DietRestriction> SupportedDietRestrictionsCurrent { get; set; } = new List<DietRestriction>();


    public BoardGamesDropdown GamesDropdown { get; set; } = new BoardGamesDropdown();
    
    public int UpdatedGameNightId { get; set; }
    
    public void FillGameNightData(GameNight data)
    {
        UpdatedGameNightId = data.Id;

        this.Title = data.Title;
        this.IsAdultsOnly = data.IsForAdults;
        this.Description = data.Description;
        this.DateAndTime = data.DateAndTime;
        this.MaxPlayerAmount = data.MaxPlayerAmount;
        this.Street = data.Location.Street;
        this.HouseNumber = data.Location.Number;
        this.City = data.Location.City;
        this.SupportedDietRestrictionsCurrent = data.SupportedDietRestrictions;
    }

    public Address CreateAddress()
    {
        return new Address(this.Street!, this.HouseNumber!.Value, this.City!);
    }    
    public class BoardGamesDropdown
    {
        public ICollection<BoardGame>? ChoosableBoardGames { get; set; } = new List<BoardGame>();

        public List<string> ChosenBoardGames { get; set; } = new List<string>();
    }
}

