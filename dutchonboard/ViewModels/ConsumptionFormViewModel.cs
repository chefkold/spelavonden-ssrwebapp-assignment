using System.ComponentModel.DataAnnotations;

namespace dutchonboard.Models;

public class ConsumptionFormViewModel
{
    [Required(ErrorMessage = "Geef alstublieft de naam van het eten of drinken op")]
    public string? Name { get; set; }

    public ICollection<DietRestriction> DietRestrictions { get; set; } =  Enum.GetValues(typeof(DietRestriction)).Cast<DietRestriction>().ToList();
    public int GameNightId { get; set; }
}