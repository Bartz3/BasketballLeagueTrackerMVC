using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BasketballLeagueTracker.ViewModels
{
    public class CommentViewModel
    {
        public Comment Comment { get; set; }
        public int ArticleId { get; set; }
    }
}
