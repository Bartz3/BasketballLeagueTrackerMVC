using Bogus.DataSets;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BasketballLeagueTracker.ViewModels
{
    public class UpdateGameViewModel
    {
        public Game Game { get; set; }
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

}
