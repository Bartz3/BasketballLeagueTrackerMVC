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

        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }

        public int? HomeTeamId { get; set; }
        public int? AwayTeamId { get; set; }

        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }

        public int LeagueId { get; set; }
        public League League { get; set; }

        public ICollection<GamePlayerStats> GamePlayerStats { get; set; }
    }
}
