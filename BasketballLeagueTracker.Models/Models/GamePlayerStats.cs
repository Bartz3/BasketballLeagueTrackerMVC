using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.Models
{
    public class GamePlayerStats
    {

        public int GamePlayerStatsId { get; set; } // ? Klucz złożony -> GameId + PlayerId

        public int? Points { get; set; }
        public int? TimeSpend { get; set; }
        public int? Rebounds { get; set; }
        public int? OffensiveRebounds { get; set; }
        public int? DefensiveRebounds { get; set; }
        public int? Assists { get; set; }
        public int? Steals { get; set; }
        public int? Blocks { get; set; }
        public int? Turnovers { get; set; }
        public int? Fouls { get; set; }

        public bool? IsOnBench { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }

    }
}
