using BasketballLeagueTracker.DataAccess.Repository;
using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.Models;
using BasketballLeagueTracker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace BasketballLeagueTracker.Controllers
{
    public class LeagueController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFavouriteLeagueRepository _favouriteLeagueRepository;
        public LeagueController(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            IFavouriteLeagueRepository favouriteLeagueRepository)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _favouriteLeagueRepository = favouriteLeagueRepository;
        }

        //public IActionResult Index(int? page)
        //{
        //    var leagues = _unitOfWork.League.GetAll(null);

        //    int pageSize = 10; 
        //    int pageNumber = (page ?? 1);
        //    var model = leagues.ToPagedList(pageNumber, pageSize);

        //    return View(model);
        //}

        public IActionResult Index()
        {
            List<League> leagueList = _unitOfWork.League.GetAll(null).ToList();

            return View(leagueList);
        }
        [Authorize(Roles = Utility.RoleNames.Role_Admin)]
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                var leagueVM = new LeagueViewModel()
                {
                    League = new League(),
                    Image = null,
                };
                return View(leagueVM);
            }
            else
            {
                League? league = _unitOfWork.League.Get(p => p.LeagueId == id, null);
                var leagueVM = new LeagueViewModel()
                {
                    League = league
                };

                return View(leagueVM);
            }
        }
        private bool IsFavourite(int? leagueId)
        {
            if (leagueId == null)
                return false;

            var userId = _userManager.GetUserId(User);
            var followedLeague = _favouriteLeagueRepository.Get(ft => ft.UserId == userId && ft.LeagueId ==leagueId ,null);

            if (followedLeague == null)
                return false;
            else return true;

        }
        [Authorize]
        public async Task<IActionResult> AddLeagueToFavourites(int leagueId)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _userManager.GetUserAsync(User);
            var followedLeague = _favouriteLeagueRepository.Get(ft => ft.UserId == user.Id && ft.LeagueId == leagueId, null);
            var league = _unitOfWork.League.Get(t => t.LeagueId == leagueId, null);

            if (followedLeague == null)
            {
                var favouriteLeague = new FavouriteLeague
                {
                    UserId = user.Id,
                    LeagueId = leagueId,
                    DateAdded = DateTime.UtcNow
                };
                _favouriteLeagueRepository.Add(favouriteLeague);
                _favouriteLeagueRepository.Save();
          
                TempData["success"] = "Liga została dodana do ulubionych";
            }
            else
            {
                _favouriteLeagueRepository.Delete(followedLeague);
                _favouriteLeagueRepository.Save();
                TempData["success"] = "Liga została usunięta z ulubionych";
            }


            return RedirectToAction("Details", new { leagueId = leagueId});
        }
        public IActionResult Details(int leagueId)
        {
            var league = _unitOfWork.League.Get(t => t.LeagueId == leagueId, "Teams,Articles,Games.HomeTeam,Games.AwayTeam,SeasonStatistics");
            LeagueViewModel leagueVM = new LeagueViewModel();
            leagueVM.League = league;
            ViewBag.IsFavourite = IsFavourite(leagueId);
            //List<SeasonStatistics> seasonStatistic= new List<SeasonStatistics>();
            //var allStats= _unitOfWork.SeasonStatistics.GetAll("Teams");
            //foreach (var stat in allStats)
            //{
            //    if(stat.LeagueId==leagueId)seasonStatistic.Add(stat);
            //}


            //TempData["SelectedTeam"] = team;

            return View(leagueVM);
        }
        [Authorize(Roles = Utility.RoleNames.Role_Admin)]
        [HttpPost]
        public IActionResult Upsert(LeagueViewModel leagueVM, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        leagueVM.League.Logo = memoryStream.ToArray();
                    }
                }


                if (leagueVM.League.LeagueId == 0)
                {

                    _unitOfWork.League.Add(leagueVM.League);
                    TempData["success"] = "Liga została dodana";
                }
                else
                {
                    _unitOfWork.League.Update(leagueVM.League);
                    TempData["success"] = "Liga została zmodyfikowana";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
        [Authorize(Roles = Utility.RoleNames.Role_Admin)]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            League? league = _unitOfWork.League.Get(p => p.LeagueId == id, null);

            if (league == null)
            {
                return NotFound();
            }
            return View(league);
        }
        [Authorize(Roles = Utility.RoleNames.Role_Admin )]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            League? league = _unitOfWork.League.Get(p => p.LeagueId == id, null);
            if (league == null)
            {
                return NotFound();
            }

            _unitOfWork.League.Delete(league);
            _unitOfWork.Save();

            TempData["success"] = "Liga została usunięta";

            return RedirectToAction("Index");
        }


    }
}
