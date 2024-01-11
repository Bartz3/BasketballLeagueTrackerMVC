using BasketballLeagueTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.Models
{
    public class UserCommentRating
    {
        public int UserCommentRatingId { get; set; }

        public bool Rating { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

        //public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}

//public class Player
//{
//    public int PlayerId { get; set; }

//    public int? TeamId { get; set; }
//    public Team? Team { get; set; }

//    ...
//}

//public class Team
//{
//    public int? TeamId { get; set; }
//    public ICollection<Player>? Players { get; set; }

//    ...
//}
