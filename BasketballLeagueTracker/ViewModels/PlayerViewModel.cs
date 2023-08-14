using BasketballLeagueTracker.Models;
using System.ComponentModel.DataAnnotations;

namespace BasketballLeagueTracker.ViewModels
{
    public class PlayerViewModel
    {
        public Player Player { get; set; }
        [Display(Name ="Pozycje")]
        public List<int> SelectedPositions { get; set; }
    }
}
