using BasketballLeagueTracker.DataAccess.Repository;
using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BasketballLeagueTracker.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFavouritePlayerRepository _favouritePlayerRepository;

        public PlayerController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Details(int? playerId)
        {
            var player = _unitOfWork.Player.Get(t => t.PlayerId == playerId, "Team");
            //TempData["SelectedTeam"] = team;

            return View(player);
        }

        public IActionResult Index()
        {
            var playersList = _unitOfWork.Player.GetAll(includeProp: "Team");

            return View(playersList);
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0) // Adding player
            {
                PlayerViewModel playerVM = new PlayerViewModel()
                {
                    Player = new Player(),
                    SelectedPositions = new List<int>() { },
                    Countries = Utility.StaticDetails.countries.Select(c => new SelectListItem
                    {
                        Text = c,
                        Value = c,
                        Selected =c=="Polska"
                    }).ToList()
                };

                return View(playerVM);
            }
            else // Editing player
            {
                Player? player = _unitOfWork.Player.Get(p => p.PlayerId == id, null);
                var pos = player.Positions;
                PlayerViewModel playerVM = new PlayerViewModel()
                {
                    Player = player,
                    //SelectedPositions = new List<int>() { },
                    Countries = Utility.StaticDetails.countries.Select(c => new SelectListItem
                    {
                        Text = c,
                        Value = c,
                        Selected = c == "Polska"
                    }).ToList()
                };
                //playerVM.SelectedPositions = player.Positions;

                return View(playerVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(PlayerViewModel playerVM, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                // Adding/ editing player poisitions
                if (playerVM.SelectedPositions != null)
                {
                    foreach (var pos in playerVM.SelectedPositions)
                    {
                        playerVM.Player.Positions += pos;
                    }
                }
                // Adding/ editing player Photo
                if (file != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        playerVM.Player.Photo = memoryStream.ToArray();
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


        public IActionResult AddPlayerToTeam(int? teamId)
        {
            var team = _unitOfWork.Team.Get(t => t.TeamId == teamId, "Players");
            ViewBag.TeamId = teamId;
            ViewBag.TeamName = team.Name;
            return View();
        }

        [HttpPost]
        public IActionResult AddPlayerToTeamPOST(int playerId, int teamId)
        {
            var player = _unitOfWork.Player.Get(p => p.PlayerId == playerId, null);
            var team = _unitOfWork.Team.Get(t => t.TeamId == teamId, "Players");
            string messageToDisplay = "";
            if (team == null)
            {
                messageToDisplay = "Brak drużyny";
                return Json(new
                {
                    success = false,
                    message = messageToDisplay
                });
            }
            if (team.Players.Count > 13)
            {
                messageToDisplay = "Przekroczono limit zawodników w drużynie." +
                    "\n Maksmalna liczba wynosi 13";

                return Json(new
                {
                    success = false,
                    message = messageToDisplay
                });
            }
            else if (player.TeamId <= 0 || player.TeamId == null)
            {
                player.TeamId = teamId;
                player.IsInTeam = true;
                _unitOfWork.Save();
                messageToDisplay = $"Zawodnik {player.FullName} został dodany do drużyny";
            }

            return Json(new
            {
                success = true,
                message = messageToDisplay
            });
        }

        /// <summary>
        /// Returns all players in Json format
        /// </summary>
        /// <returns></returns>
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
            List<Player> availablePlayers = new List<Player>();
            foreach (var player in players)
            {
                if (player.IsInTeam == false)
                    availablePlayers.Add(player);
            }

            var ignoreCyclesInJSON = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            return Json(new { data = availablePlayers.ToList() }, ignoreCyclesInJSON);
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

        [Authorize]
        public async Task<IActionResult> AddPlayerToFavourites(int playerId)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _userManager.GetUserAsync(User);
            var followedPlayer = _favouritePlayerRepository.Get(ft => ft.UserId == user.Id && ft.PlayerId == playerId, null);
            var player = _unitOfWork.Player.Get(t => t.PlayerId == playerId, null);

            if (followedPlayer == null)
            {
                var favouritePlayer = new FavouritePlayer
                {
                    UserId = user.Id,
                    PlayerId = playerId,
                    DateAdded = DateTime.UtcNow
                };
                _favouritePlayerRepository.Add(favouritePlayer);
                _favouritePlayerRepository.Save();
                TempData["success"] = "Zawodnik został dodany do ulubionych";
            }
            else
            {
                _favouritePlayerRepository.Delete(followedPlayer);
                _favouritePlayerRepository.Save();
                TempData["success"] = "Zawodnik został usunięty z ulubionych";
            }


            return RedirectToAction("Details", new { playerId = playerId });
        }

    }
}
