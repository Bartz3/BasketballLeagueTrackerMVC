﻿using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.ViewModels;
using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using static Microsoft.VisualStudio.Services.Notifications.VssNotificationEvent;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        //public IActionResult Index()
        //{
        //    var articleList = _unitOfWork.Article.GetAll(null);

        //    return View(articleList);
        //}

        public IActionResult Details(int articleId)
        {
            var article = _unitOfWork.Article.Get(t => t.ArticleId == articleId, "League,Comments.User"); // Players
            
            //TempData["SelectedTeam"] = team;

            return View(article);
        }


        // id - ArticleId
        [Authorize(Roles = Utility.RoleNames.Role_Admin + "," + Utility.RoleNames.Role_Moderator)]
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


        [Authorize(Roles = Utility.RoleNames.Role_Admin + "," + Utility.RoleNames.Role_Moderator)]
        [HttpPost]
        public IActionResult Upsert(ArticleViewModel articleVM, IFormFile? file)
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

                if (file != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        articleVM.Article.MainPhoto = memoryStream.ToArray();
                    }
                }
                if (articleVM.Article.ArticleId == 0)
                {
                    var user =  _userManager.GetUserId(User);
                    articleVM.Article.UserId = user;
                    //Ataki XSS polegają na wstrzykiwaniu złośliwych skryptów do zawartości,
                    //które następnie są wykonywane w przeglądarce innych użytkowników.
                    //Aby zapobiec takim atakom, należy filtrować i
                    //sanatoryzować dane wejściowe przed ich zapisaniem lub wyświetleniem.
                    var sanitizer = new HtmlSanitizer();
                    articleVM.Article.Content = sanitizer.Sanitize(articleVM.Article.Content);

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
        [Authorize(Roles = Utility.RoleNames.Role_Admin + "," + Utility.RoleNames.Role_Moderator)]
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

        [Authorize(Roles = Utility.RoleNames.Role_Admin + "," + Utility.RoleNames.Role_Moderator)]
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
