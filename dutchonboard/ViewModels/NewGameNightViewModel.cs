using System.ComponentModel.DataAnnotations;

namespace dutchonboard.Models;

public class NewGameNightViewModel 
{
    [Required(ErrorMessage = "Geef de avond alstublieft een titel"), StringLength(30, ErrorMessage = "Houd de titel beknopt (maximaal 30 karakters")]
    public string? Title { get; set; }
    
    [Required(ErrorMessage = "Geef alstublieft aan ofe de avond 18+ is")]
    public bool? IsAdultsOnly { get; set;}

    [Required(ErrorMessage = "Geef de avond alstublieft een beschrijving"), StringLength(400, ErrorMessage = "Maak uw beschrijving korter (maximaal 400 karakters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Geef alstublieft de datum en tijd van de avond")]
    public DateTime? DateAndTime { get; set; }

    [Required(ErrorMessage = "Geef alstublieft aan hoeveel spelers maximaal kunnen meedoen (met uzelf inbegrepen)"), Range(1, int.MaxValue, ErrorMessage = "Aantal spelers is minimaal 1")]
    public int? MaxPlayerAmount { get; set; }

    [Required(ErrorMessage = "Straat ontbreekt van adresgegevens")]
    public string? Street { get; set; }

    [Required(ErrorMessage = "Huisnummer ontbreekt van adresgegevens")]
    public int? HouseNumber { get; set; }

    [Required(ErrorMessage = "Stad ontbreekt van adresgegevens")]
    public string? City { get; set; }

    public GameNight ConvertToGameNight()
    {
        return new GameNight()
        {
            Title = this.Title,
            Description = this.Description,
            AdultOnly = IsAdultsOnly,
            DateAndTime = this.DateAndTime!.Value,
            MaxPlayerAmount = this.MaxPlayerAmount!.Value, 
            Location = new Address(Street!, HouseNumber!.Value, City!)
        };
    }
}