using System.ComponentModel.DataAnnotations;

namespace dutchonboard.Core.Domain.Models;

#nullable disable
public class BoardGame
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } 
    public string Type { get; set; } 
    public Genre Genre { get; set; }
    public bool? IsForAdults { get; set; }
    public byte[] Image { get; set; }
    public string ImageFormat { get; set; }
    public ICollection<GameNight> GameNightsWhereFeatured { get; set; } = new List<GameNight>();
}