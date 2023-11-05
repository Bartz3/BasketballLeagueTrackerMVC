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
        
        [Display(Name ="Punkty")]
        public int? Points { get; set; } = 0;

        //[Display(Name = "Czas gry")]
        //public int? TimeSpend { get; set; } = 0;
        [Display(Name = "Czas gry")]
        [Column(TypeName = "bigint")]
        [DisplayFormat(DataFormatString = "{0:mm\\:ss}", ApplyFormatInEditMode = true)]
        public TimeSpan? TimeSpend { get; set; } = new TimeSpan(0, 0, 0);

        [Display(Name = "Zbiórki")]
        public int? Rebounds { get; set; } = 0;

        [Display(Name ="Zbiórki ofensywne")]
        public int? OffensiveRebounds { get; set; } = 0;

        [Display(Name ="Zbiórki defensywne")]
        public int? DefensiveRebounds { get; set; } = 0;

        [Display(Name ="Asysty")]
        public int? Assists { get; set; } = 0;

        [Display(Name ="Przechwyty")]
        public int? Steals { get; set; } = 0;

        [Display(Name ="Bloki")]
        public int? Blocks { get; set; } = 0;

        [Display(Name ="Straty")]
        public int? Turnovers { get; set; } = 0;

        [Display(Name ="Faule"),
            Range(0, 5, ErrorMessage = "Ilość fauli musi być od 0 do 5.")]
        public int? Fouls { get; set; }

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
