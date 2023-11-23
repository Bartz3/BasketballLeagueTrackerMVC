using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BasketballLeagueTracker.ViewModels
{
    public class TeamViewModel
    {
        public Team Team { get; set; }
        [ValidateNever]
        public IFormFile? Image { get; set; }

        public IEnumerable<Player>? AvailablePlayers { get; set; }

        public Stadium? TeamStadium { get; set; }
    }
}
