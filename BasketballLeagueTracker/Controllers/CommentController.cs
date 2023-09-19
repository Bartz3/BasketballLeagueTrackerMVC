using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.ViewModels;
using Ganss.Xss;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BasketballLeagueTracker.Controllers
{
    public class CommentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        public IActionResult Details(int commentId)
        {
            var comment = _unitOfWork.Comment.Get(t => t.CommentId == commentId,"User"); // Players
            //TempData["SelectedTeam"] = team;

            return View(comment);
        }

        public IActionResult Upsert(int? id,int? articleId)
        {
            var commentVM = new CommentViewModel();

            if (articleId.HasValue)
            {
                commentVM.ArticleId= articleId.Value;
            }

            if (id == null || id == 0)
            {
                commentVM.Comment = new Comment();
                return View(commentVM);
            }
            else
            {
                Comment? comment = _unitOfWork.Comment.Get(p => p.CommentId == id, null);
                commentVM.Comment= comment;

                return View(commentVM);
            }
        }



        [HttpPost]
        public IActionResult Upsert(CommentViewModel commentVM)
        {

            if (ModelState.IsValid)
            {
                int _articleId = 0;
                if (TempData["ArticleId"] != null)
                {
                    _articleId = Convert.ToInt32(TempData["ArticleId"]);

                    //var league=_unitOfWork.League.Get(l => l.LeagueId == leagueId,null);
                    //commentVM.Article.League = league;

                    commentVM.Comment.ArticleId = _articleId;
                }
                if (commentVM.Comment.CommentId == 0)
                {
                    var userId =  _userManager.GetUserId(User);
                    commentVM.Comment.UserId = userId;
                    commentVM.Comment.ArticleId= commentVM.ArticleId;

                    _unitOfWork.Comment.Add(commentVM.Comment);
                    TempData["success"] = "Komentarz został dodany";
                }
                else
                {
                    _unitOfWork.Comment.Update(commentVM.Comment);
                    TempData["success"] = "Komentarz został zmodyfikowany";
                }
                _unitOfWork.Save();

                return RedirectToAction("Details", "Article", new {articleId=commentVM.ArticleId });
                
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            Comment? comment = _unitOfWork.Comment.Get(p => p.CommentId == id, null);

            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        public IActionResult DeletePOST(int? commentId, int? articleId)
        {
            Comment? comment = _unitOfWork.Comment.Get(p => p.CommentId == commentId, null);
            if (comment == null)
            {
                return NotFound();
            }

            _unitOfWork.Comment.Delete(comment);
            _unitOfWork.Save();

            TempData["success"] = "Komentarz został usunięty";
            return RedirectToAction("Details", "Article", new { articleId = articleId }); //?
        }


    }
}
