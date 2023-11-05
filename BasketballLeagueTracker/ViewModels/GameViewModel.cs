using Bogus.DataSets;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BasketballLeagueTracker.ViewModels
{
    public class GameViewModel
    {
        public Game? Game { get; set; }
        [Display(Name = "Goście")]
        [ValidateNever]
        public List<SelectListItemWithImage> HomeTeamSL{ get; set; }
        [Display(Name = "Gospodarze")]
        [ValidateNever]
        public List<SelectListItemWithImage> AwayTeamSL{ get; set; }
        public int LeagueId { get; set; }

        public List<GamePlayerStats>? HomeTeamGPS { get; set; }
        public List<GamePlayerStats>? AwayTeamGPS { get; set; }

    }
    public class SelectListItemWithImage : SelectListItem
    {
        public byte[] Logo { get; set; }
    }
    public class GameValidator : AbstractValidator<GameViewModel>
    {
        public GameValidator()
        {
            //RuleFor(x => x.Game.AwayTeamScore)
            //    .GreaterThan(-1).WithMessage("Wynik nie może być mniejszy niż 0")
            //    .LessThan(256).WithMessage("Wynik jest zbyt duży");

            //RuleFor(x => x.Game.AwayTeamScore)
            //    .LessThan(654).WithErrorCode("zle");

            //RuleFor(x => x.Game.HomeTeamScore)
            //    .GreaterThan(-1).WithMessage("Wynik nie może być mniejszy niż 0")
            //    .LessThan(256).WithMessage("Wynik jest zbyt duży");
            //RuleFor(x => x.Game.AwayTeamId)
            //    .NotEqual(x => x.Game.HomeTeamId)
            //    .WithMessage("Dodaj dwie różne drużyny.");
            //RuleFor(x => x.Game.AwayTeamId)
            //    .NotEqual(x => x.Game.HomeTeamId)
            //    .WithMessage("Dodaj dwie różne drużyny.");

        }
    }
}
