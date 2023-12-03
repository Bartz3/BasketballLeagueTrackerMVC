using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BasketballLeagueTracker.Models
{
    public class GamePlayerStats
    {
        private const string errorMessage0 = "Wartość nie może być mniejsza niż 0.";

        [Display(Name = "Punkty")]
        [Range(0, int.MaxValue, ErrorMessage = errorMessage0)]
        public short? Points { get; set; } = 0;

        [Display(Name = "2PA")]
        [Range(0, int.MaxValue, ErrorMessage = errorMessage0)]
        public short? FGA { get; set; }= 0;
        [Display(Name = "2PM")]
        [Range(0, int.MaxValue, ErrorMessage = errorMessage0)]
        public short? FGM { get; set; } = 0;

        [Display(Name = "3PA")]
        [Range(0, int.MaxValue, ErrorMessage = errorMessage0)]
        public short? PM3 { get; set; } = 0;
        [Display(Name = "3PM")]
        [Range(0, int.MaxValue, ErrorMessage = errorMessage0)]
        public short? PA3 { get; set; } = 0;

        [Display(Name = "FTA")]
        [Range(0, int.MaxValue, ErrorMessage = errorMessage0)]
        public short? FTA { get; set; } = 0;
        [Display(Name = "FTM")]
        [Range(0, int.MaxValue, ErrorMessage = errorMessage0)]
        public short? FTM { get; set; } = 0;

        [Display(Name = "Czas gry")]
        [Column(TypeName = "bigint")]
        [DisplayFormat(DataFormatString = "{0:mm\\:ss}", ApplyFormatInEditMode = true)]
        public TimeSpan? TimeSpend { get; set; } = new TimeSpan(0, 0, 0);

        [Display(Name = "Zbiórki")]
        [Range(0, int.MaxValue, ErrorMessage = errorMessage0)]
        public short? Rebounds { get; set; } = 0;

        [Display(Name = "Zbiórki ofensywne")]
        [Range(0, int.MaxValue, ErrorMessage = errorMessage0)]
        public short? OffensiveRebounds { get; set; } = 0;

        [Display(Name = "Zbiórki defensywne")]
        [Range(0, int.MaxValue, ErrorMessage = errorMessage0)]
        public short? DefensiveRebounds { get; set; } = 0;

        [Display(Name = "Asysty")]
        [Range(0, int.MaxValue, ErrorMessage = errorMessage0)]
        public short? Assists { get; set; } = 0;

        [Display(Name = "Przechwyty")]
        [Range(0, int.MaxValue, ErrorMessage = errorMessage0)]
        public short? Steals { get; set; } = 0;

        [Display(Name = "Bloki")]
        [Range(0, int.MaxValue, ErrorMessage = errorMessage0)]
        public short? Blocks { get; set; } = 0;

        [Display(Name = "Straty")]
        [Range(0, int.MaxValue, ErrorMessage = errorMessage0)]
        public short? Turnovers { get; set; } = 0;

        [Display(Name = "Faule")]
        [Range(0, 5, ErrorMessage = "Błędna ilość fauli (0-5).")]
        public short? Fouls { get; set; }


        [Display(Name = "Starter")]
        public bool? IsOnBench { get; set; } = true;
        
        // ? Composite key -> GameId + PlayerId
        public int? GameId { get; set; }
        public Game? Game { get; set; }

        public int? PlayerId { get; set; }
        public Player? Player { get; set; }

        public string TimeSpendFixedFormat(int time)
        {
            int minutes = time / 60;
            int seconds = time % 60;

            if (minutes == 0)
            {
                return $"{minutes}:{"00" + seconds.ToString()}";
            }
            else
            {
                return $"{minutes:00}:{seconds:00}";
            }
        }

    }
}
