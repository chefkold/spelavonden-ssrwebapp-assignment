using dutchonboard.Core.Domain.Models;
using dutchonboard.Core.DomainServices.Repositories;
using Microsoft.VisualBasic;
using System;
using System.Linq.Expressions;

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
    public Result<GameNight> NewGameNight(Organizer organizer, string title, string description, bool isForAdults, bool potluck,
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
            Potluck = potluck,
            MaxPlayerAmount = maxPlayerAmount,
            Location = location,
            DateAndTime = dateAndTime,
            Organizer = organizer,
            // An organizer is automatically a player
            Players = new List<Player> { organizer }
        });
    }

    public Result EditGameNight(int id, string title, string description, bool isForAdults, int maxPlayerAmount, Address location,
        DateTime dateAndTime, ICollection<BoardGame> boardGames)
    {
        var gN = _iGameNightRepo.GetGameNightById(id);
        var checkAllowedToUpdateResult = VerifyAllowedToUpdateOrDelete(gN); 
        if (checkAllowedToUpdateResult.HasError)
        {
            return checkAllowedToUpdateResult;
        }

        if (maxPlayerAmount < 1)
        {
            return new Result("Minimaal een maximaal spelerslimiet van 1, let op u doet mee.");
        }

        gN.Title = title;
        gN.Description = description;
        gN.IsForAdults = isForAdults;
        gN.MaxPlayerAmount = maxPlayerAmount;
        gN.Location = location;
        gN.DateAndTime = dateAndTime;
        AddBoardGames(gN, boardGames);

        _iGameNightRepo.UpdateGameNight(gN);
        return new Result();
    }

    public Result DeleteGameNight(int id)
    {
        Result result;
        var gn = _iGameNightRepo.GetGameNightById(id);
        if ((result = VerifyAllowedToUpdateOrDelete(gn)).HasError)
        {
            return result;
        }

        _iGameNightRepo.DeleteGameNight(gn);
        return result;
    }


    // Business rule: Game night only allowed to update if no players have joined (excluding the organizer himself, so count is at least 1)
    public Result VerifyAllowedToUpdateOrDelete(GameNight gameNight)
    {
        if (gameNight.Players.Count > 1)
        {
            return new Result(
                "U kunt deze avond niet wijzigen of verwijderen, een andere speler naast uzelf heeft zich al ingeschreven");
        }

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
            game.GameNightsWhereFeatured.Add(gameNight);
            _iBoardGameRepo.UpdateBoardgame(game);
        }

        return new Result();
    }
    // Business rule (A): A player can only join a game night if he is not already joined
    // Business rule (B): if a game night maximum player count is met, a player cannot join
    // Business rule (C): if a game night is for adults only, a non-adult player cannot join
    // Business rule (D): if a player is already enrolled to a game night for this game night's date, a player cannot join
    public Result AddPlayerToGameNight(int gameNightId, Player player)
    {
        var gN = GetGameNightById(gameNightId);

        // A
        if (gN.Players.Contains(player))
        {
            return new Result("U bent al ingeschreven voor deze avond");
        }

        // B
        if (gN.MaxPlayerAmount <= gN.Players.Count)
        {
            return new Result("Het limiet van maximaal aantal spelers is al bereikt.");
        }

        // C
        if (gN.IsForAdults && !player.IsAdult())
        {
            return new Result("Deze avond is voor alleen voor volwassenen!");
        }

        // D
        if (player.JoinedNights.Any(g =>
                DateOnly.FromDateTime(g.DateAndTime).Equals(DateOnly.FromDateTime(gN.DateAndTime))))
        {
            return new Result("U bent al ingeschreven op een bordspellenavond vandaag!");
        }

        return new Result();

    }
    public Result CommitEnrollmentOfPlayer(GameNight gameNight, Player player)
    {
        gameNight.Players.Add(player);
        _iGameNightRepo.UpdateGameNight(gameNight);
        return new Result();
    }

    // Business rule: a player must at least supply one consumption in order to be enrolled (if the game has potluck)
    public Result AddConsumptionsToGameNight(int gameNightId, ICollection<Consumption> consumptions)
    {
        if (consumptions.Count == 0)
        {
            return new Result("Voeg minimaal één consumptie toe.");
        }

        var gN = GetGameNightById(gameNightId);
        foreach (var consumption in consumptions)
        {
            gN.Consumptions.Add(consumption);
        }

        _iGameNightRepo.UpdateGameNight(gN);
        return new Result();

    }

    public void SaveNewGameNight(GameNight gameNight)
    {
        _iGameNightRepo.AddGameNight(gameNight);
    }

    public ICollection<GameNight> GetAllGameNights() => _iGameNightRepo.GetAllGameNights();

    public GameNight GetGameNightById(int id)
    {
        return _iGameNightRepo.GetGameNightById(id);
    }
}