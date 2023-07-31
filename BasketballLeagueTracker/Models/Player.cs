using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BasketballLeagueTracker.Models
{
    [Flags]
    public enum PlayerPosition
    {
        [Display(Name = "Trener")]
        Coach = 0,

        [Display(Name = "Rozgrywający")]
        PointGuard = 1,

        [Display(Name = "Rzucający obrońca")]
        ShootingGuard = 2,

        [Display(Name = "Niski skrzydłowy")]
        SmallForward = 4,

        [Display(Name = "Silny skrzydłowy")]
        PowerForward = 8,

        [Display(Name = "Środkowy")]
        Center = 16    
    }


    public class Player
    {
        public int PlayerId { get; set; }


        [Display(Name = "Imię")]
        [MinLength(2, ErrorMessage = "Imie jest zbyt krótkie")]
        [MaxLength(40, ErrorMessage ="Imie jest zbyt długie")]
        public string Name { get; set; }
        [MinLength(2, ErrorMessage = "Nazwisko jest zbyt krótkie")]
        [MaxLength(50, ErrorMessage = "Nazwisko jest zbyt długie")]
        [Display(Name = "Nazwisko")]
        public string? Surname { get; set; }
        public DateTime? Birthday { get; set; }

        public int? UniformNumber { get; set; }

        [Display(Name = "Pozycje")]
        public PlayerPosition Positions{ get; set; }

        public int? Height { get; set; }
        public double? Weight { get; set; }
        public string? Country { get; set; }

        public int? GamesPlayed { get; set; }

        public bool? IsInTeam { get; set; }

        // Jeden zawodnik należy do jednej drużyny Player ∞----1 Team
        public int? TeamId { get; set; }
        public Team? Team { get; set; }

        public ICollection<FavouritePlayer>? FavouritePlayers { get; set; }
    }

}
