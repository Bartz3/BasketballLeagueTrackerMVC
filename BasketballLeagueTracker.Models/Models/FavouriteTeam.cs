﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.Models
{
    public class FavouriteTeam
    {
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public DateTime DateAdded { get; set; }
    }
 
}
