﻿using System.ComponentModel.DataAnnotations;

namespace dutchonboard.Models;

/// <summary>
/// ViewModel to represent game nights in situations where one is created or edited. In case of editing,
/// this model keeps track of the game night ID as a best practice solution for supplying a POST form with multiple parameters
/// </summary>
public class GameNightViewModel
{
    [Required(ErrorMessage = "Geef de avond alstublieft een titel"), StringLength(30, ErrorMessage = "Houd de titel beknopt (maximaal 30 karakters")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Geef alstublieft aan ofe de avond 18+ is")]
    public bool? IsAdultsOnly { get; set; }

    [Required(ErrorMessage = "Geef de avond alstublieft een beschrijving"), StringLength(400, ErrorMessage = "Maak uw beschrijving korter (maximaal 400 karakters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Geef alstublieft de datum en tijd van de avond")]
    public DateTime? DateAndTime { get; set; }

    [Required(ErrorMessage = "Geef alstublieft een waarde op"), Range(1, int.MaxValue, ErrorMessage = "Aantal spelers is minimaal 1")]
    public int? MaxPlayerAmount { get; set; }

    [Required(ErrorMessage = "Geef alstublieft een waarde op")]
    public string? Street { get; set; }

    [Required(ErrorMessage = "Geef alstublieft een waarde op")]
    public int? HouseNumber { get; set; }

    [Required(ErrorMessage = "Geef alstublieft een waarde op")]
    public string? City { get; set; }
    public int UpdatedGameNightId { get; set; }
    public void FillGameNightData(GameNight data)
    {
        UpdatedGameNightId = data.Id;

        this.Title = data.Title;
        this.IsAdultsOnly = data.AdultOnly;
        this.Description = data.Description;
        this.DateAndTime = data.DateAndTime;
        this.MaxPlayerAmount = data.MaxPlayerAmount;
        this.Street = data.Location.Street;
        this.HouseNumber = data.Location.Number;
        this.City = data.Location.City;
    }
    public GameNight ConvertToGameNight()
    {
        return new GameNight()
        {
            Title = this.Title,
            Description = this.Description,
            AdultOnly = this.IsAdultsOnly,
            DateAndTime = this.DateAndTime!.Value,
            MaxPlayerAmount = this.MaxPlayerAmount!.Value,
            Location = new Address(Street!, HouseNumber!.Value, City!)
        };
    }
}