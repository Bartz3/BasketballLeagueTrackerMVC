using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Data założenia")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Display(Name = "Powiadomienia")]
        public bool? NotificationBool { get; set; }

        public ICollection<Article>? Articles { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<UserCommentRating>? UserCommentRaitings { get; set; }

        [Display(Name = "Ulubioni zawodnicy ")]
        public ICollection<FavouritePlayer>? FavouritePlayers { get; set; }
        [Display(Name = "Ulubione drużyny ")]
        public ICollection<FavouriteTeam>? FavouriteTeams { get; set; }
        [Display(Name = "Ulubione ligi ")]
        public ICollection<FavouriteLeague>? FavouriteLeagues { get; set; }
    }

    //[Key]
    //public Guid ApplicationUserId { get; set; }

    //public string Username { get; set; } 
    //public string Password { get; set; }

}
