using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.Models
{
    public enum GameStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Postponed,
        Cancelled
    }

    public class Game
    {
        public int GameId { get; set; }

        public DateTime GameDate { get; set; }
        public GameStatus Status { get; set; } = GameStatus.NotStarted;


        [Display(Name ="Wynik gosp")]
        [Range(0,256,ErrorMessage ="Wynik jest niepoprawny.")]
        public int HomeTeamScore { get; set; }
        [Display(Name ="Wynik gość")]
        [Range(0,256,ErrorMessage ="Wynik jest niepoprawny.")]
        public int AwayTeamScore { get; set; }

        public Team? HomeTeam { get; set; }
        [Display(Name ="Gospodarze")]
        public int? HomeTeamId { get; set; }

        public Team? AwayTeam { get; set; }
        [Display(Name ="Goście")]
        public int? AwayTeamId { get; set; }

        public int? LeagueId { get; set; }
        [Display(Name ="Liga")]
        public League? League { get; set; }

        public ICollection<GamePlayerStats>? GamePlayerStats { get; set; }
    }
}
