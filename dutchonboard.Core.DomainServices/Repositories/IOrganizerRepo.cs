namespace dutchonboard.Core.DomainServices.Repositories;

public interface IOrganizerRepo
{
    public abstract Organizer GetOrganizerByEmail(string email);
}