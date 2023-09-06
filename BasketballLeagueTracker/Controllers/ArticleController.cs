using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BasketballLeagueTracker.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public ArticleController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var articleList = _unitOfWork.Article.GetAll(null);

            return View(articleList);
        }

        public IActionResult Details(int articleId)
        {
            var article = _unitOfWork.Article.Get(t => t.ArticleId == articleId, "League"); // Players
            //TempData["SelectedTeam"] = team;

            return View(article);
        }

        // id - ArticleId
        public IActionResult Upsert(int? id,int? leagueId)
        {
            var articleVM = new ArticleViewModel();

            if (leagueId.HasValue)
            {
                articleVM.LeagueId= leagueId.Value;
            }

            if (id == null || id == 0)
            {
                articleVM.Article = new Article();
                return View(articleVM);
            }
            else
            {
                Article? article = _unitOfWork.Article.Get(p => p.ArticleId == id, null);
                articleVM.Article= article;

                return View(articleVM);
            }
        }



        [HttpPost]
        public IActionResult Upsert(ArticleViewModel articleVM/*, IFormFile? file*/)
        {

            if (ModelState.IsValid)
            {
                int _leagueId = 0;
                if (TempData["LeagueId"] != null)
                {
                     _leagueId= Convert.ToInt32(TempData["LeagueId"]);

                    //var league=_unitOfWork.League.Get(l => l.LeagueId == leagueId,null);
                    //articleVM.Article.League = league;

                    articleVM.Article.LeagueId = _leagueId;
                }
                if (articleVM.Article.ArticleId == 0)
                {

                    var user =  _userManager.GetUserId(User);
                    articleVM.Article.UserId = user;

                    _unitOfWork.Article.Add(articleVM.Article);
                    TempData["success"] = "Artykuł został dodany";
                }
                else
                {
                    _unitOfWork.Article.Update(articleVM.Article);
                    TempData["success"] = "Artykuł została zmodyfikowany";
                }
                _unitOfWork.Save();

                return RedirectToAction("Details", "League", new {leagueId=_leagueId });
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Article? article = _unitOfWork.Article.Get(p => p.ArticleId == id, null);

            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        public IActionResult DeletePOST(int? articleId, int? leagueId)
        {
            Article? article = _unitOfWork.Article.Get(p => p.ArticleId == articleId, null);
            if (article == null)
            {
                return NotFound();
            }

            _unitOfWork.Article.Delete(article);
            _unitOfWork.Save();

            TempData["success"] = "Artykuł został usunięty";
            return RedirectToAction("Details", "League", new { leagueId = leagueId});
        }


    }
}
