namespace dutchonboard.Core.DomainServices.Repositories;

public interface IPlayerRepo
{
    public abstract Player GetPlayerByEmail (string email);
}