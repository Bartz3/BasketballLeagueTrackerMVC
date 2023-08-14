using BasketballLeagueTracker.DataAccess.Data;
using BasketballLeagueTracker.DataAccess.Repository;
using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;
using BasketballLeagueTracker.ViewModels;

namespace BasketballLeagueTracker.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlayerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult AddPlayerToTeam(int? teamId)
        {
            var team = _unitOfWork.Team.Get(t=>t.TeamId == teamId,"Players");
            ViewBag.TeamId = teamId;

            return View();
        }
        [HttpPost]
        public IActionResult AddPlayerToTeamPOST(int playerId,int teamId)
        {
            var player = _unitOfWork.Player.Get(p => p.PlayerId == playerId,null);

            player.TeamId = teamId;
            player.IsInTeam= true;

            _unitOfWork.Save();


            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult GetAllPlayers()
        {
            var players = _unitOfWork.Player.GetAll(includeProp: "Team").ToList();

            var ignoreCyclesInJSON = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles 
            };

            return Json(new { data = players }, ignoreCyclesInJSON);
        }

        [HttpGet]
        public IActionResult GetAllAvailablePlayers()
        {
            var players = _unitOfWork.Player.GetAll(includeProp: "Team").ToList();
            List<Player> availablePlayers=new List<Player>();
            foreach(var player in players)
            {
                if(player.IsInTeam==false)
                    availablePlayers.Add(player);
            }

            var ignoreCyclesInJSON = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            return Json(new { data = availablePlayers.ToList()}, ignoreCyclesInJSON);
        }

        [HttpPost]
        public IActionResult DeleteSelectedPlayers(List<int> selectedPlayers)
        {
            List<Player> playersToDelete = new List<Player>();
            if (selectedPlayers != null && selectedPlayers.Any())
            {
                foreach (int playerId in selectedPlayers)
                {
                    // Pobierz zawodnika z bazy danych i usuń go
                    var player = _unitOfWork.Player.Get(p => p.PlayerId == playerId, null);
                    playersToDelete.Add(player);
                }

                _unitOfWork.Player.DeleteRange(playersToDelete);
                _unitOfWork.Save();
                TempData["success"] = "Zawodnicy zostali usunięci";
            }
            else
            {
                TempData["error"] = "Nie wybrano żadnych zawodników do usunięcia";
            }

            return RedirectToAction("Index");
        }


        public IActionResult Index()
        {
            var playersList = _unitOfWork.Player.GetAll(includeProp: "Team");

            return View(playersList);
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return View();
            }
            else
            {
                Player? player = _unitOfWork.Player.Get(p => p.PlayerId == id, null);
                var playerVM = new PlayerViewModel();
                playerVM.Player = player;
                //playerVM.SelectedPositions = player.Positions;

                return View(playerVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(PlayerViewModel playerVM)
        {

            if (ModelState.IsValid)
            {
                if (playerVM.SelectedPositions != null)
                {
                    foreach (var pos in playerVM.SelectedPositions)
                    {
                        playerVM.Player.Positions += pos;
                    }
                }

                if (playerVM.Player.PlayerId == 0)
                {

                    _unitOfWork.Player.Add(playerVM.Player);
                    TempData["success"] = "Zawodnik został dodany";
                }
                else
                {
                    _unitOfWork.Player.Update(playerVM.Player);
                    TempData["success"] = "Zawodnik został zmodyfikowany";
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
            Player? player = _unitOfWork.Player.Get(p => p.PlayerId == id, null);

            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Player? player = _unitOfWork.Player.Get(p => p.PlayerId == id, null);
            if (player == null)
            {
                return NotFound();
            }

            _unitOfWork.Player.Delete(player);
            _unitOfWork.Save();

            TempData["success"] = "Zawodnik został usunięty";

            return RedirectToAction("Index");
        }


    }
}
