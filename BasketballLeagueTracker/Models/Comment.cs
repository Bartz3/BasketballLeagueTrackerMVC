using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        [Display(Name = "Treść komentarza")]
        public string Content { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

        public ApplicationUser User { get; set; }

        public ICollection<UserCommentRating> Ratings { get; set; }
    }
}
