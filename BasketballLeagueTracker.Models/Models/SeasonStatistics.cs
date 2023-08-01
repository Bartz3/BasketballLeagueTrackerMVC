using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasketballLeagueTracker.Models
{
    public class SeasonStatistics
    {
        public int SeasonStatisticsId { get; set; }

        public int Wins { get; set; }
        public int Losses { get; set; }
        public int GamesPlayed { get; set; }

        public int LeaguePoints { get; set; }

        public double PointsPerGame { get; set; }
        public double OpponentPointsPerGame { get; set; }


        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int LeagueId { get; set; }
        public League League { get; set; }
    }

}