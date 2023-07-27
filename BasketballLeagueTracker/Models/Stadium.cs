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

        public string Name { get; set; }

        public int Capacity { get; set; }

        public byte[] Image { get; set; }

        //public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
