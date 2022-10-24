namespace dutchonboard.Core.Domain.Models;

public class Address
{
    public Address(string street, int number, string city)
    {
        Street = street;
        Number = number;
        City = city;
    }

    public string Street { get; set; }
    public int Number { get; set; }
    public string City { get; set; }
    
    public override string ToString() => $"{Street} {Number} te {City}"; 
}