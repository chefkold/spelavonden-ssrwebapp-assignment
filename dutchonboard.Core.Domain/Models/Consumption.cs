namespace dutchonboard.Core.Domain.Models;

public class Consumption
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public ICollection<DietRestriction> DietRestrictions { get; set; } = new List<DietRestriction>();
    public IEnumerable<GameNight> GameNightsWhereConsumed { get; set; } = new List<GameNight>();

    public Consumption(string name)
    {
        Name = name;
    }
}