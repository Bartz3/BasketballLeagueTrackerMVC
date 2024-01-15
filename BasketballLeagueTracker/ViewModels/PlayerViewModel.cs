using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BasketballLeagueTracker.ViewModels
{
    public class PlayerViewModel
    {
        public Player? Player { get; set; }

        [ValidateNever]
        public IFormFile? Image { get; set; }
        [Display(Name = "Pozycje")]
        public List<int>? SelectedPositions { get; set; }
        public List<SelectListItem>? Countries { get; set; }     
        public List<Game>? PlayerGames { get; set; }     

    }
}
