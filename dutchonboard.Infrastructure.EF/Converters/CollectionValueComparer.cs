using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace dutchonboard.Infrastructure.EF.Converters;

#nullable disable

/// <summary>
/// Support collections of enumerator values to be stored with EF Core 
/// </summary>
public class CollectionValueComparer<T> : ValueComparer<ICollection<T>>
{
    public CollectionValueComparer() : base((c1, c2) => c1.SequenceEqual(c2),
        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), c => (ICollection<T>)c.ToHashSet())
    {
    }
}