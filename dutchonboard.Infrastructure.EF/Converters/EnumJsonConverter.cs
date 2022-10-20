using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace dutchonboard.Infrastructure.EF.Converters;
#nullable disable
public class EnumJsonConverter<T> : ValueConverter<ICollection<T>, string> where T : Enum
{
    public EnumJsonConverter() : base(
        v => JsonConvert
            .SerializeObject(v.Select(e => e.ToString()).ToList()),
        v => JsonConvert
            .DeserializeObject<ICollection<string>>(v)
            .Select(e => (T)Enum.Parse(typeof(T), e)).ToList())
    {
    }
}