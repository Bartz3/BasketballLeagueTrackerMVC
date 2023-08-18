using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Opis")]
        public string? Description { get; set; }

        [Display(Name = "Logo")]

        public byte[]? TeamLogo { get; set; }

        public bool? IsCurrentlyPlaying { get; set; }

        public int? LeagueId { get; set; }
        public League? League { get; set; }

        public ICollection<Player>? Players { get; set; }

        public int? StadiumId { get; set; }
        public Stadium? Stadium { get; set; }

        public ICollection<FavouriteTeam>? FavouriteTeams { get; set; }
        public ICollection<SeasonStatistics>? SeasonStatistics { get; set; }


        public ICollection<Game>? HomeGames { get; set; }
        public ICollection<Game>? AwayGames { get; set; }

        public ICollection<FavouriteTeam>? TeamFollowers { get; set; }

    }
}
