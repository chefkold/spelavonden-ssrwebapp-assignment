using dutchonboard.Core.Domain.Models;
using dutchonboard.Core.DomainServices.Repositories;
using HotChocolate;
using HotChocolate.Types;

namespace dutchonboardGraphQLApi.GraphQl;

public class GameNightQuery
{
    public IEnumerable<GameNight> GetAllGameNights([Service] IGameNightRepo iGameNightRepo) 
    {
        return iGameNightRepo.GetAllGameNights();
    }
    
}   