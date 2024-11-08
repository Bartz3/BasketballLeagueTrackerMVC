﻿using BasketballLeagueTracker.DataAccess.Repository;
using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.Models;
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

        public PlayerController(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,IFavouritePlayerRepository favouritePlayerRepository)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _favouritePlayerRepository = favouritePlayerRepository;
        }

        public IActionResult Details(int? playerId)
        {
            PlayerViewModel playerVM = new PlayerViewModel();
            var player = _unitOfWork.Player.Get(t => t.PlayerId == playerId, "Team");
            playerVM.Player = player;
            var allStats = _unitOfWork.GamePlayerStats.GetAll(null);
            List<int?> gameIds =new List<int?>();
            List<Game> playerGames = new List<Game>();
            foreach (var stat in allStats)
            {
                if (stat.PlayerId == playerId) gameIds.Add(stat.GameId);
            }
            foreach (var id in gameIds)
            {
                playerGames.Add(_unitOfWork.Game.Get(g => g.GameId == id, "AwayTeam,HomeTeam"));
            }
            playerVM.PlayerGames=playerGames;
            ViewBag.IsFavourite = IsFavourite(playerId);

            return View(playerVM);
        }

        public IActionResult Index()
        {
            var playersList = _unitOfWork.Player.GetAll(includeProp: "Team");

            return View(playersList);
        }

        [Authorize(Roles = Utility.RoleNames.Role_Admin)]
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
                Player? player = _unitOfWork.Player.Get(p => p.PlayerId == id, "Team");
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

        [Authorize(Roles = Utility.RoleNames.Role_Admin)]
        [HttpPost]
        public IActionResult Upsert(PlayerViewModel playerVM, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                playerVM.Player.Team = null;playerVM.Player.TeamId = null;
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
                    int playerId = playerVM.Player.PlayerId;
                    _unitOfWork.Player.Update(playerId ,playerVM.Player);
                    TempData["success"] = "Zawodnik został zmodyfikowany";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            playerVM.Countries = Utility.StaticDetails.countries.Select(c => new SelectListItem
            {
                Text = c,
                Value = c,
                Selected = c == "Polska"
            }).ToList();

            return View(playerVM);
        }
        [Authorize(Roles = Utility.RoleNames.Role_Admin)]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Player? player = _unitOfWork.Player.Get(p => p.PlayerId == id, "Team");

            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }
        [Authorize(Roles = Utility.RoleNames.Role_Admin)]
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

        [Authorize(Roles = Utility.RoleNames.Role_Admin)]
        public IActionResult AddPlayerToTeam(int? teamId)
        {
            var team = _unitOfWork.Team.Get(t => t.TeamId == teamId, "Players");
            ViewBag.TeamId = teamId;
            ViewBag.TeamName = team.Name;
            return View();
        }
        [Authorize(Roles = Utility.RoleNames.Role_Admin)]
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
                team.Players.Add(player);
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
        [Authorize(Roles = Utility.RoleNames.Role_Admin)]
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
        [Authorize(Roles =Utility.RoleNames.Role_Admin)]
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

        private bool IsFavourite(int? playerId)
        {
            if (playerId == null)
                return false;

            var userId = _userManager.GetUserId(User);
            var followedPlayer = _favouritePlayerRepository.Get(fp => fp.UserId == userId && fp.PlayerId == playerId, null);

            if (followedPlayer == null)
                return false;
            else return true;

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
