using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.Models;
using BasketballLeagueTracker.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                var gameVM = new GameViewModel()
                {
                    Game = new Game(),

                };
                Game game= new Game();
                return View(game);
            }
            else
            {
                Game? game = _unitOfWork.Game.Get(p => p.GameId == id, null);
                var gameVM = new GameViewModel()
                {
                   Game=game
                };

                return View(gameVM);
            }
        }

        public IActionResult Details(int gameId)
        {
            var game = _unitOfWork.Game.Get(t => t.GameId == gameId,null); 

            //TempData["SelectedTeam"] = team;

            return View(game);
        }

        [HttpPost]
        public IActionResult Upsert(GameViewModel gameVM)
        {

            if (ModelState.IsValid)
            {


                if (gameVM.Game.GameId == 0)
                {

                    _unitOfWork.Game.Add(gameVM.Game);
                    //TempData["success"] = "Liga została dodana";
                }
                else
                {
                    _unitOfWork.Game.Update(gameVM.Game);
                    //TempData["success"] = "Liga została zmodyfikowana";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
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


    }
}
