using BasketballLeagueTracker.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BasketballLeagueTracker.ViewModels
{
    public class LeagueViewModel
    {
        public League League { get; set; }
        [ValidateNever]
        public IFormFile? Image { get; set; }

    }
}
