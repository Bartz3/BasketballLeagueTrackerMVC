using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasketballLeagueTracker.Models
{
    public class SeasonStatistics
    {

        public int? Wins { get; set; } = 0;
        public int? Losses { get; set; } = 0;
        public int? LeaguePoints { get; set; } = 0;
        public int? GamesPlayed { get; set; } = 0;

        public double? TeamPoints { get; set; } = 0;
        public double? OpponentPoints { get; set; } = 0;


        public int? TeamId { get; set; }
        public Team? Team { get; set; }

        public int? LeagueId { get; set; }
        public League? League { get; set; }
    }

}