using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.Models
{
    [Flags]
    public enum PlayerPosition
    {
        None = 0,
        PointGuard = 1,
        ShootingForward = 2,
        SmallForward = 4,
        PowerForward = 8,
        Center = 16
    }

    public class Player
    {
        public int PlayerId { get; set; }

        public string Name { get; set; }
        public string? Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public int? UniformNumber { get; set; }

        public PlayerPosition Position { get; set; }

        public int? Height { get; set; }
        public double? Weight { get; set; }
        public string? Country { get; set; }

        public int? GamesPlayed { get; set; }

        public bool? IsInTeam { get; set; }

        // Jeden zawodnik należy do jednej drużyny Player ∞----1 Team
        public int? TeamId { get; set; }
        public Team? Team { get; set; }

        public ICollection<FavouritePlayer> FavouritePlayers { get; set; }
    }

}
