using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BasketballLeagueTracker.Models
{
    [Flags]
    public enum PlayerPosition
    {
        [Display(Name = "Trener")]
        Coach = 1,

        [Display(Name = "Rozgrywający")]
        PointGuard = 2,

        [Display(Name = "Rzucający obrońca")]
        ShootingGuard = 4,

        [Display(Name = "Niski skrzydłowy")]
        SmallForward = 8,

        [Display(Name = "Silny skrzydłowy")]
        PowerForward = 16,

        [Display(Name = "Środkowy")]
        Center = 32    
    }

    public class Player
    {
        public int PlayerId { get; set; }
        [Display(Name = "Imię")]
        [MinLength(2, ErrorMessage = "Imie jest zbyt krótkie")]
        [MaxLength(40, ErrorMessage = "Imie jest zbyt długie")]
        public string Name { get; set; }
        [MinLength(2, ErrorMessage = "Nazwisko jest zbyt krótkie")]
        [MaxLength(50, ErrorMessage = "Nazwisko jest zbyt długie")]
        [Display(Name = "Nazwisko")]
        public string? Surname { get; set; }

        [Display(Name = "Zdjęcie")]
        public byte[]? Photo { get; set; }

        public DateTime? Birthday { get; set; }

        public int? UniformNumber { get; set; }


        public int? Height { get; set; }
        public double? Weight { get; set; }
        public string? Country { get; set; }

        public bool IsInTeam { get; set; } = false;

        [Display(Name = "Pozycje")]
        public PlayerPosition Positions { get; set; }
        // Jeden zawodnik należy do jednej drużyny Player ∞----1 Team
        public int? TeamId { get; set; }
        public Team? Team { get; set; }

        public int? GamesPlayed { get; set; }

        public ICollection<FavouritePlayer>? PlayerFollowers { get; set; }

        public string FullName => $"{Name} {Surname}";
        public string? FormattedBirthday => Birthday?.ToString("yyyy-MM-dd");


        public string GetPositionsString(PlayerPosition positions)
        {
            var positionNames = Enum.GetValues(typeof(PlayerPosition))
                .Cast<PlayerPosition>()
                .Where(p => positions.HasFlag(p))
                .Select(p => GetDisplayAttributeValue(p));

            return string.Join(", ", positionNames);
        }

        public static string GetDisplayAttributeValue(Enum? value)
        {
            //var displayAttribute = value.GetType().GetField(value.ToString())
            //    .GetCustomAttributes(typeof(DisplayAttribute), false)
            //    .OfType<DisplayAttribute>()
            //    .FirstOrDefault();

            //return displayAttribute != null ? displayAttribute.GetName() : value.ToString();
            var fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo != null)
            {
                var displayAttributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false).OfType<DisplayAttribute>().ToList();

                if (displayAttributes.Any())
                {
                    return displayAttributes.First().GetName();
                }
            }

            return value.ToString();
        }
    }

}
