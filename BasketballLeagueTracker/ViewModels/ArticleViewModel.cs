using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BasketballLeagueTracker.ViewModels
{
    public class ArticleViewModel
    {
        public Article Article { get; set; }
        public int LeagueId { get; set; }
    }
}
