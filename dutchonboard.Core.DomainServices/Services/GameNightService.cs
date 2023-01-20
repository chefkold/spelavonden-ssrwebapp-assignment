using dutchonboard.Core.Domain.Models;
using dutchonboard.Core.DomainServices.Repositories;
using Microsoft.VisualBasic;
using System;

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

    // Already covered business rule: organizer is at least 18 years gn, see class definition of Organizer
    // Business rule: GameNight always has one player (the organizer himself)
    // TODO allergies
    public Result<GameNight> NewGameNight(Organizer organizer, string title, string description, bool isForAdults,
        int maxPlayerAmount, Address location, DateTime dateAndTime)
    {
        if (maxPlayerAmount < 1)
        {
            return new Result<GameNight>("Minimaal een maximaal spelerslimiet van 1, let op u doet mee.");
        }
        return new Result<GameNight>(new GameNight
        {
            Title = title,
            Description = description,
            IsForAdults = isForAdults,
            MaxPlayerAmount = maxPlayerAmount,
            Location = location,
            DateAndTime = dateAndTime,
            Organizer = organizer
        });
    }

    // Business rule: Game night only allowed to update if no players have joined (excluding the organizer himself, so count is at least 1)
    public Result editGameNight(int id, string title, string description, bool isForAdults, int maxPlayerAmount, Address location,
        DateTime dateAndTime, ICollection<FoodAndDrinkType> dietAndAllergyInfo, ICollection<BoardGame> boardGames)
    {
        var gn = _iGameNightRepo.GetGameNightById(id);
        if (gn.Players.Count > 1)
        {
            return new Result(
                "U kunt deze avond niet wijzigen, een andere speler naast uzelf heeft zich al ingeschreven");
        }

        if (maxPlayerAmount < 1)
        {
            return new Result("Minimaal een maximaal spelerslimiet van 1, let op u doet mee.");
        }

        gn.Title = title;
        gn.Description = description;
        gn.IsForAdults = isForAdults;
        gn.MaxPlayerAmount = maxPlayerAmount;
        gn.Location = location;
        gn.DateAndTime = dateAndTime;
        AddBoardGames(gn, boardGames);
        gn.DietAndAllergyInfo = dietAndAllergyInfo;

        _iGameNightRepo.UpdateGameNight(gn);
        return new Result();
    }

    // Business rule: When a game is added, it must be checked if its adults only. If  true, the game night should also become adults only
    public Result AddBoardGames(GameNight gameNight, ICollection<BoardGame> boardGames)
    {
        foreach (var game in boardGames)
        {
            if (game.IsForAdults == true)
            {
                gameNight.IsForAdults = true;
            }
            gameNight.Games.Add(game);
        }

        return new Result();
    }

    public void SaveNewGameNight(GameNight gameNight)
    {
        _iGameNightRepo.AddGameNight(gameNight);
    }


    public ICollection<GameNight> GetAllGameNights() => _iGameNightRepo.GetAllGameNights();

}