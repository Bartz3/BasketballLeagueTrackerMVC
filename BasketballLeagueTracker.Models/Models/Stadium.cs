using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.Models
{
    public class Stadium
    {
        public int StadiumId { get; set; }

        public string? Name { get; set; }

        public string? StadiumLatitude { get; set; }
        public string? StadiumLongitude { get; set; }
        public string? Address { get; set; }

        public int? TeamId { get; set; }
        public Team? Team { get; set; }
    }
}
