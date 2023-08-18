using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.Models
{
    public class FavouriteLeague
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int LeagueId { get; set; }
        public League League { get; set; }

        public DateTime DateAdded { get; set; }
    }
 
}
