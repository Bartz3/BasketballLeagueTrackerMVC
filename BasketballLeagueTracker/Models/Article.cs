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
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public int LeagueId { get; set; }
        public League League { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<ArticleImage>? Images { get; set; }
    }
}
