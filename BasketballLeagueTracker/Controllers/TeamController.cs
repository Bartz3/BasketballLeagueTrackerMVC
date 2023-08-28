﻿using BasketballLeagueTracker.DataAccess.Data;
using BasketballLeagueTracker.DataAccess.Repository;
using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.Models;
using BasketballLeagueTracker.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BasketballLeagueTracker.Controllers
{
    public class TeamController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFavouriteTeamRepository _favouriteTeamRepository;

        public TeamController(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            IFavouriteTeamRepository favouriteTeamRepository)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _favouriteTeamRepository = favouriteTeamRepository;
        }

        public async Task<IActionResult> AddToFavorites(int teamId)
        {
            var user = await _userManager.GetUserAsync(User); // Znajdź aktualnie zalogowanego użytkownika.

            if (user == null)
            {
                return Unauthorized();
            }

            // Sprawdzenie, czy drużyna jest już w ulubionych.
            if (user.FavouriteTeams == null)
            {
                user.FavouriteTeams = new List<FavouriteTeam>();
            }

            if (!user.FavouriteTeams.Any(t => t.TeamId == teamId))
            {
                var favouriteTeam = new FavouriteTeam
                {
                    UserId = user.Id,
                    TeamId = teamId,
                    DateAdded = DateTime.UtcNow
                };

                _favouriteTeamRepository.Add(favouriteTeam);
                _favouriteTeamRepository.Save();
            }

            return RedirectToAction("Index"); // Przekieruj użytkownika do odpowiedniego miejsca po zakończeniu operacji.
        }

        //[Authorize]
        //public async Task<IActionResult> AddToFavorites(int teamId)
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        return Unauthorized();
        //    }


        //    // Sprawdzenie czy drużyna jest już w ulubionych
        //    if (!user.FavouriteTeams.Any(t => t.TeamId == teamId))
        //    {
        //        user.FavouriteTeams.Add(new FavouriteTeam { TeamId = teamId, UserId = user.Id });
        //        await _userManager.UpdateAsync(user);
        //    }

        //    return RedirectToAction("Index");
        //}




        public IActionResult Index()
        {
            var teamsList = _unitOfWork.Team.GetAll(null);

            return View(teamsList);
        }

        public IActionResult Details(int teamId)
        {
            var team = _unitOfWork.Team.Get(t=>t.TeamId== teamId, "Players"); // Players
                                                                              //TempData["SelectedTeam"] = team;
            if (team == null)
            {
                return NotFound(); 
            }
            ViewBag.TeamName=team.Name;

            return View(team);
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                var teamVM = new TeamViewModel()
                {
                    Team = new Team(),
                    Image = null,
                    AvailablePlayers = null
                };
                return View(teamVM);
            }
            else
            {
                Team? team = _unitOfWork.Team.Get(p => p.TeamId == id,null);

                var availablePlayers = _unitOfWork.Player.GetAll(null)
                    .Where(p => p.IsInTeam == false);

                var teamVM = new TeamViewModel
                {
                    Team = team,
                    AvailablePlayers = availablePlayers
                };

                return View(teamVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(TeamViewModel teamVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                //Team team;

                if (file != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        teamVM.Team.TeamLogo = memoryStream.ToArray();
                    }
                }
                //team = teamVM.Team;

                if (teamVM.Team.TeamId == 0)
                {
                    _unitOfWork.Team.Add(teamVM.Team);
                    TempData["success"] = "Zespół został dodany";
                }
                else
                {
                    _unitOfWork.Team.Update(teamVM.Team);
                    TempData["success"] = "Zespół pomyślnie zmieniony";
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
            Team? team = _unitOfWork.Team.Get(p => p.TeamId == id, null);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Team? team = _unitOfWork.Team.Get(p => p.TeamId == id, null);
            //if (team == null)
            //{
            //    return NotFound();
            //}
            //var players = _unitOfWork.Player.GetAll(null).Where(p=>p.TeamId==id);
            //_unitOfWork.Team.RemovePlayerFromTeam();
            ////List<Player> playersInTeam=players.Where(p=>p.TeamId== id).ToList();

            //foreach (var player in players)
            //{
            //    player.TeamId = null;
            //    player.Team = null;

            //}

            _unitOfWork.Team.Delete(team);
            _unitOfWork.Save();

            TempData["success"] = "Zespół został usunięty";

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Returns all teams in Json format
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllTeams()
        {
            var teams = _unitOfWork.Team.GetAll(null).ToList();

            //var ignoreCyclesInJSON = new JsonSerializerOptions
            //{
            //    ReferenceHandler = ReferenceHandler.IgnoreCycles
            //};
            //, ignoreCyclesInJSON
            return Json(new { data = teams });
        }

        [HttpGet]
        public IActionResult GetAllAvailableTeams()
        {
            var teams = _unitOfWork.Team.GetAll(null).ToList();
            List<Team> availableTeams = new List<Team>();
            foreach (var team in teams)
            {
                if (team.IsInTheLeague == false || team.IsInTheLeague==null)
                    availableTeams.Add(team);
            }

            //var ignoreCyclesInJSON = new JsonSerializerOptions
            //{
            //    ReferenceHandler = ReferenceHandler.IgnoreCycles
            //};

            return Json(new { data = availableTeams.ToList() });
        }

        [HttpPost]
        public IActionResult RemovePlayerFromTeam(int teamId, int playerId)
        {
            try
            {
                _unitOfWork.Team.RemovePlayerFromTeam(teamId, playerId);
                TempData["success"] = $"Zawodnik został usunięty z drużyny.";
                return RedirectToAction("Details", "Team", new { teamId = teamId });
            }
            catch
            {
                TempData["Error"] = "Wystąpił błąd podczas usuwania zawodnika.";
                return RedirectToAction("Details", "Team", new { teamId = teamId });
            }
        }


        public IActionResult AddTeamToLeague(int? leagueId)
        {
            var league = _unitOfWork.League.Get(t => t.LeagueId == leagueId, "Teams");
            ViewBag.LeagueId = leagueId;
            ViewBag.LeagueName=league.Name;

            return View();
        }

        [HttpPost]
        public IActionResult AddTeamToLeaguePOST(int teamId, int leagueId)
        {
            var team = _unitOfWork.Team.Get(p => p.TeamId == teamId, null);
            var league = _unitOfWork.League.Get(t => t.LeagueId == leagueId,"Teams");
            string messageToDisplay = "";

            if (league == null)
            {
                messageToDisplay = "Brak ligi";
                return Json(new
                {
                    success = false,
                    message = messageToDisplay
                });
            }
            if (league.Teams.Count > 13)
            {
                messageToDisplay = $"Przekroczono liczbę drużyn w lidze ${league.Name}." +
                    "\n Maksmalna liczba wynosi 16";
        
                return Json(new
                {
                    success = false,
                    message = messageToDisplay
                });
            }
            else if (team.LeagueId <= 0 || team.LeagueId == null)
            {
                team.LeagueId = leagueId;
                team.IsInTheLeague = true;
                _unitOfWork.Save();
                messageToDisplay = $"Drużyna {team.Name} została dodana do ligi";
            }

            return Json(new
            {
                success = true,
                message = messageToDisplay
            });
        }
    }
}
