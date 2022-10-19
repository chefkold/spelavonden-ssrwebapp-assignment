namespace dutchonboard.Core.Domain.Models;

public class Person
{
    public Person(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<GameNight> JoinedNights = new List<GameNight>(); 

}