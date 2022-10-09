namespace dutchonboard.Core.Domain.Models;

public class Address
{
    public int Id { get; set; }
    public string Street { get; set; }
    public int HouseNumber { get; set; }
    public string PostalCode { get; set; }
}