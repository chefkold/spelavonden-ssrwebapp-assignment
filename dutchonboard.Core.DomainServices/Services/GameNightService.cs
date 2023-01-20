using dutchonboard.Core.Domain.Models;
using dutchonboard.Core.DomainServices.Repositories;
using Microsoft.VisualBasic;

namespace dutchonboard.Core.DomainServices.Services;

public class GameNightService : IGameNightService
{
    private readonly IGameNightRepo _iGameNightRepo;
    private readonly IBoardGameRepo _iBoardGameRepo;
    private readonly IOrganizerRepo _iOrganizerRepo;
    private readonly IPlayerRepo _iPlayerRepo;

    public GameNightService(IGameNightRepo iGameNightRepo, IBoardGameRepo iBoardGameRepo, IOrganizerRepo iOrganizerRepo, IPlayerRepo iPlayerRepo)
    {
        _iGameNightRepo = iGameNightRepo;
        _iBoardGameRepo = iBoardGameRepo;
        _iOrganizerRepo = iOrganizerRepo;
        _iPlayerRepo = iPlayerRepo;
    }

    // Already covered business rule: organizer is at least 18 years old, see class definition of Organizer
    // Business rule: GameNight always has one player (the organizer himself)
    public Result<GameNight>NewGameNight(Organizer organizer,string title, string description, bool isForAdults,
        int maxPlayerAmount, Address location, DateTime dateAndTime)
    {
        if (maxPlayerAmount < 1)
        {
            return new Result<GameNight>("Minimaal een maximaal spelerslimiet van 1, let op u doet mee.");
        }
        return new Result<GameNight>(_iGameNightRepo.AddGameNight(new GameNight
        {
            Title = title,
            Description = description,
            IsForAdults = isForAdults,
            MaxPlayerAmount = maxPlayerAmount,
            Location = location,
            DateAndTime = dateAndTime,
            Organizer = organizer
        }));
    }

     // Business rule: When a game is added, it must be checked if its adults only. If  true, the game night should also become adults only
    public Result AddBoardGame(GameNight gameNight, BoardGame boardGame)
    {
        if (boardGame.IsForAdults == true)
        {
            gameNight.IsForAdults = true;
        }
        gameNight.Games.Add(boardGame);
        _iGameNightRepo.UpdateGameNight(gameNight);

        return new Result(); 
    }

    public Result AddBoardGames(GameNight gameNight, ICollection<BoardGame> boardGames)
    {
        var result = new Result();
        foreach (var game in boardGames)
        {
            result = AddBoardGame(gameNight, game);
            if (result.HasError)
            {
                return result;
            }
        }

        return result;
    }

    public ICollection<GameNight> GetAllGameNights() => _iGameNightRepo.GetAllGameNights();
    
}