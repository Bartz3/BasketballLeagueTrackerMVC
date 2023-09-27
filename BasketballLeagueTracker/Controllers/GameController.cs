using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.Models;
using BasketballLeagueTracker.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BasketballLeagueTracker.Controllers
{
    public class GameController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GameController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var gameList = _unitOfWork.Game.GetAll(null);

            return View(gameList);
        }
        public IActionResult Details(int gameId)
        {
            var game = _unitOfWork.Game.Get(t => t.GameId == gameId, null);

            //TempData["SelectedTeam"] = team;

            return View(game);
        }

        public IActionResult Upsert(int? id,int? leagueId)
        {

            //ViewData["Teams"] = new SelectList(_unitOfWork.Team.GetAll(null).ToList(), "TeamId", "Name");
            //ViewData["AwayTeamId"] = new SelectList(_unitOfWork.Team.GetAll(null), "TeamId", "Name");
            //ViewData["Leagues"] = new SelectList(_unitOfWork.League.GetAll(null), "LeagueId", "Name");


            var gameVM = new GameViewModel();

            if (leagueId.HasValue)
            {
                gameVM.LeagueId = leagueId.Value;
            }
            PopulateTeamSL(ref gameVM);

            if (id == null || id == 0)
            {
                gameVM.Game = new Game();
                return View(gameVM);
            }
            else
            {
                Game? game = _unitOfWork.Game.Get(p => p.GameId == id, null);
                gameVM.Game = game;

                return View(gameVM);
            }
        }




        [HttpPost]
        public IActionResult Upsert(GameViewModel gameVM)
        {

            if (ModelState.IsValid)
            {
                if (gameVM.Game.HomeTeamId == gameVM.Game.AwayTeamId)
                {
                    ModelState.AddModelError("", "Drużyny muszą być różne.");
                    PopulateTeamSL(ref gameVM);
                    return View(gameVM);
                }
                int _leagueId = 0;
                if (TempData["LeagueId"] != null)
                {
                    _leagueId = Convert.ToInt32(TempData["LeagueId"]);
                    gameVM.Game.LeagueId = _leagueId;
                }

                if (gameVM.Game.GameId == 0)
                {

                    _unitOfWork.Game.Add(gameVM.Game);
                    TempData["success"] = "Mecz został utworzony.";
                }
                else
                {
                    _unitOfWork.Game.Update(gameVM.Game);
                    TempData["success"] = "Mecz został zmodyfikowany";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            PopulateTeamSL(ref gameVM);

            return View(gameVM);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var game = _unitOfWork.Game.Get(t => t.GameId == id, null);

            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Game? game = _unitOfWork.Game.Get(p => p.GameId == id, null);
            if (game == null)
            {
                return NotFound();
            }

            _unitOfWork.Game.Delete(game);
            _unitOfWork.Save();

            TempData["success"] = "Spotkanie zostało usunięte";

            return RedirectToAction("Index");
        }

        public void PopulateTeamSL(ref GameViewModel gameVM)
        {
            var teams = _unitOfWork.Team.GetAll(null);

            gameVM.HomeTeamSL = new List<SelectListItemWithImage>();
            gameVM.AwayTeamSL = new List<SelectListItemWithImage>();
            foreach (var team in teams)
            {
                gameVM.HomeTeamSL.Add(new SelectListItemWithImage
                {
                    Text = team.Name,
                    Value = team.TeamId.ToString(),
                    Logo = team.TeamLogo


                });
                gameVM.AwayTeamSL.Add(new SelectListItemWithImage
                {
                    Text = team.Name,
                    Value = team.TeamId.ToString(),
                    Logo = team.TeamLogo
                });
            }
        }

    }
}
