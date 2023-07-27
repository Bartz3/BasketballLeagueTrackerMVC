using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.Models
{
    public class FavouritePlayer
    {
        public int FavouritePlayerId { get; set; }

        public ApplicationUser User { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
    //public int UserId { get; set; }
}
