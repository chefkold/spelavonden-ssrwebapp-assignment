namespace dutchonboard.Core.Domain.Models;

#nullable disable
public class Player
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public Address Address { get; set; }
    public string Email { get; set; }
    public DateOnly BirthDate { get; }
    public ICollection<DietRestriction> DietRestrictions { get; set; } = new List<DietRestriction>();
    public ICollection<GameNight> JoinedNights { get; set; } = new List<GameNight>();

    // Business rules: someone with a birthday in the future or less than 16 years ago cannot be a player
    public Player(DateOnly birthDate)
    {
        if (birthDate.AddYears(16) > DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ArgumentException("Player must be at least 16 years old");
        }

        if (birthDate > DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ArgumentException("Birthday cannot be in the future");
        }

        BirthDate = birthDate;
    }
    public bool IsAdult()
    {
        return BirthDate <= DateOnly.FromDateTime(DateTime.Now.AddYears(-18));
    }

    public ICollection<DietRestriction> GetConflictingDietRestrictions(GameNight gN)
    {
        var conflictingDietRestrictions = new List<DietRestriction>();
        foreach (var c in gN.Consumptions)
        {
            foreach (var dR in c.DietRestrictions)
            {
                if (!DietRestrictions.Contains(dR)) continue;
                if (!conflictingDietRestrictions.Contains(dR))
                {
                    conflictingDietRestrictions.Add(dR);
                }
            }
        }
        return conflictingDietRestrictions;
    }
    
    public override string ToString()
    {
        var salutation = Gender switch
        {
            Gender.M => "dhr.",
            Gender.V => "mw.",
            Gender.X => "dhr/mw.",
            _ => "dhr./mw."
        };

        var age = (DateTime.Now).Year - BirthDate.Year;
        return $"{salutation} {FirstName} {LastName} ({age})";
    }

}

