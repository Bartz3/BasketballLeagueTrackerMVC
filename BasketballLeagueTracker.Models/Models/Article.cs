using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.Models
{
    public class Article
    {
        public int ArticleId { get; set; }
        [MaxLength(255)]
        [Display(Name ="Tytuł")]
        public string Title { get; set; }
        [Display(Name ="Treść")]
        public string Content { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        
        [Display(Name ="Główne zdjęcie")]
        public byte[]? MainPhoto { get; set; }

        public int? LeagueId { get; set; }
        public League? League { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public ICollection<Comment>? Comments { get; set; }

    }
}
