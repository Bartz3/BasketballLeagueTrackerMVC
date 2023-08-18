using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.Models
{
    public class League
    {
        public int LeagueId { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Opis")]
        public string? Description { get; set; }
        [Display(Name = "Logo")]
        public byte[]? Logo { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Player? LeaugeMVP { get; set; }
        public string? Season { get; set; }

        public ICollection<SeasonStatistics>? SeasonStatistics { get; set; }
        public ICollection<Team>? Teams { get; set; }

        public ICollection<Game>? Games { get; set; }

        public ICollection<FavouriteLeague>? LeagueFollowers { get; set; }
        //public ApplicationUser UserId { get; set; }
    }
}
