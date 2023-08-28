using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BasketballLeagueTracker.ViewModels
{
    public class PlayerViewModel
    {
        public Player Player { get; set; }

        [ValidateNever]
        public IFormFile? Image { get; set; }
        [Display(Name = "Pozycje")]
        public List<int> SelectedPositions { get; set; }

    }
}
