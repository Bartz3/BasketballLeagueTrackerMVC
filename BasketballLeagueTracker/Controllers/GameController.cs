﻿using BasketballLeagueTracker.DataAccess.Migrations;
using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.Models;
using BasketballLeagueTracker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using static Microsoft.VisualStudio.Services.Notifications.VssNotificationEvent;

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
            var gameVM = new GameViewModel();
            gameVM.AwayTeamGPS = new List<GamePlayerStats>();
            gameVM.HomeTeamGPS = new List<GamePlayerStats>();
            Game? game = _unitOfWork.Game.Get(p => p.GameId == gameId, "HomeTeam.Players.PlayerStats,AwayTeam.Players.PlayerStats,HomeTeam.Stadium");

            gameVM.Game = game;

            foreach (var homePlayerStats in gameVM.Game.HomeTeam.Players.
                    SelectMany(p => p.PlayerStats).
                         Where(x => x.GameId == game.GameId))
            {

                gameVM.HomeTeamGPS.Add(homePlayerStats);
            }

            foreach (var awayPlayerStats in gameVM.Game.AwayTeam.Players
                .SelectMany(p => p.PlayerStats).
                Where(x => x.GameId == game.GameId))
            {
                gameVM.AwayTeamGPS.Add(awayPlayerStats);
            }


            return View(gameVM);
        }

        [Authorize(Roles = Utility.RoleNames.Role_Admin + ","+Utility.RoleNames.Role_Moderator)]
        public IActionResult Create(int? id, int? leagueId)
        {

            var gameVM = new GameViewModel();

            if (leagueId.HasValue)
            {
                gameVM.LeagueId = leagueId.Value;
            }
            PopulateTeamSL(ref gameVM);
            gameVM.Game = new Game();
            gameVM.Game.GameDate = DateTime.Now;
            return View(gameVM);
        }
        [Authorize(Roles = Utility.RoleNames.Role_Admin + "," + Utility.RoleNames.Role_Moderator)]
        public IActionResult Update(int? id, int? leagueId)
        {

            var gameVM = new GameViewModel();
            gameVM.AwayTeamGPS = new List<GamePlayerStats>();
            gameVM.HomeTeamGPS = new List<GamePlayerStats>();

            if (leagueId.HasValue)
            {
                gameVM.LeagueId = leagueId.Value;
            }
            PopulateTeamSL(ref gameVM);


            Game? game = _unitOfWork.Game.Get(p => p.GameId == id, "HomeTeam.Players.PlayerStats,AwayTeam.Players.PlayerStats");


            gameVM.Game = game;
            gameVM.Game.HomeTeam.Name = game.HomeTeam.Name;
            gameVM.Game.AwayTeam.Name = game.AwayTeam.Name;



            foreach (var homePlayerStats in gameVM.Game.HomeTeam.Players.
                SelectMany(p => p.PlayerStats).
                Where(x => x.GameId == game.GameId))
            {
                gameVM.HomeTeamGPS.Add(homePlayerStats);
            }

            foreach (var awayPlayerStats in gameVM.Game.AwayTeam.Players
                .SelectMany(p => p.PlayerStats).
                Where(x => x.GameId == game.GameId))
            {
                gameVM.AwayTeamGPS.Add(awayPlayerStats);
            }

            return View(gameVM);

        }
        [Authorize(Roles = Utility.RoleNames.Role_Admin + "," + Utility.RoleNames.Role_Moderator)]
        [HttpPost]
        public IActionResult Update(GameViewModel gameVM, List<GamePlayerStats> gps)
        {

            if (ModelState.IsValid)
            {

                if (gameVM.HomeTeamGPS != null)
                    foreach (var homeStat in gameVM.HomeTeamGPS)
                    {
                        _unitOfWork.GamePlayerStats.Update(homeStat);
                    }

                if (gameVM.AwayTeamGPS != null)
                    foreach (var awayStat in gameVM.AwayTeamGPS)
                    {
                        _unitOfWork.GamePlayerStats.Update(awayStat);
                    }

                _unitOfWork.Save();

                var game = _unitOfWork.Game.Get(x => x.GameId == gameVM.Game.GameId, "HomeTeam.Players.PlayerStats,AwayTeam.Players.PlayerStats");
                //_unitOfWork.Game.Entry(gameVM.Game).State = EntityState.Detached;

                if (game.AwayTeam.TeamId == game.HomeTeam.TeamId)
                {
                    ModelState.AddModelError("", "Drużyny muszą być różne.");
                    PopulateTeamSL(ref gameVM);
                    return View(gameVM);
                }
                if (game.GameId == 0 || game.GameId == null)
                {

                    _unitOfWork.Game.Add(gameVM.Game);
                    TempData["success"] = "Mecz został utworzony.";
                }
                else
                {
                    game.GameDate = gameVM.Game.GameDate.Add(gameVM.GameDateHour);

                    // Updating info about season stats
                    if (gameVM.Game.Status == GameStatus.Completed)
                    {
                        var homeSeasonStats = _unitOfWork.SeasonStatistics.Get(s => s.TeamId == game.HomeTeamId
                        && s.LeagueId == game.LeagueId, null);

                        var awaySeasonStats = _unitOfWork.SeasonStatistics.Get(s => s.TeamId == game.AwayTeamId
                         && s.LeagueId == game.LeagueId, null);

                        if (gameVM.Game.HomeTeamScore > gameVM.Game.AwayTeamScore)// HomeTeam won
                        {
                            homeSeasonStats.LeaguePoints += 3;
                            homeSeasonStats.Wins += 1;

                            awaySeasonStats.Losses += 1;

                        }
                        else // AwayTeam won
                        {
                            awaySeasonStats.LeaguePoints += 3;
                            awaySeasonStats.Wins += 1;
                            homeSeasonStats.Losses += 1;

                        }
                        awaySeasonStats.GamesPlayed += 1;
                        awaySeasonStats.TeamPoints += gameVM.Game.AwayTeamScore;
                        awaySeasonStats.OpponentPoints += gameVM.Game.HomeTeamScore;

                        homeSeasonStats.GamesPlayed += 1;
                        homeSeasonStats.OpponentPoints += gameVM.Game.AwayTeamScore;
                        homeSeasonStats.TeamPoints += gameVM.Game.HomeTeamScore;


                    }
                    _unitOfWork.Game.Update(game.GameId, gameVM.Game);
                    TempData["success"] = "Mecz został zmodyfikowany";
                }
                _unitOfWork.Save();

                PopulateTeamSL(ref gameVM);
                return RedirectToAction("Details", "League", new { leagueId = gameVM.LeagueId });

            }
            else
            {
                PopulateTeamSL(ref gameVM);

                Game? actualGame = _unitOfWork.Game.Get(p => p.GameId == gameVM.Game.GameId, "HomeTeam.Players.PlayerStats,AwayTeam.Players.PlayerStats");
                gameVM.Game = actualGame;

                gameVM.AwayTeamGPS = new List<GamePlayerStats>();
                gameVM.HomeTeamGPS = new List<GamePlayerStats>();
                foreach (var homePlayerStats in gameVM.Game.HomeTeam.Players.SelectMany(p => p.PlayerStats))
                {
                    gameVM.HomeTeamGPS.Add(homePlayerStats);
                }

                foreach (var awayPlayerStats in gameVM.Game.AwayTeam.Players.SelectMany(p => p.PlayerStats))
                {
                    gameVM.AwayTeamGPS.Add(awayPlayerStats);
                }


                return View(gameVM);
            }

        }
        [Authorize(Roles = Utility.RoleNames.Role_Admin + "," + Utility.RoleNames.Role_Moderator)]
        [HttpPost]
        public IActionResult Create(GameViewModel gameVM)
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
                    gameVM.Game.GameDate = gameVM.Game.GameDate.Add(gameVM.GameDateHour); // Adding hours and minutes to gameDate

                    _unitOfWork.Game.Add(gameVM.Game);
                    _unitOfWork.Save();

                    var players = _unitOfWork.Player.GetAll("PlayerStats");
                    var homeTeamPlayers = new List<Player>();
                    var awayTeamPlayers = new List<Player>();



                    foreach (var player in players)
                    {
                        if (player.TeamId == gameVM.Game.HomeTeamId) homeTeamPlayers.Add(player);
                        else if (player.TeamId == gameVM.Game.AwayTeamId) awayTeamPlayers.Add(player);
                    }
                    GamePlayerStats playerStats = null;
                    foreach (var player in homeTeamPlayers)
                    {
                        if (player.Positions != PlayerPosition.Coach)
                        {
                            playerStats = new GamePlayerStats
                            {
                                PlayerId = player.PlayerId,
                                GameId = gameVM.Game.GameId

                            };
                        }

                        //gameVM.HomeTeamGPS.Add(playerStats);
                        _unitOfWork.GamePlayerStats.Add(playerStats);
                    }

                    foreach (var player in awayTeamPlayers)
                    {
                        if (player.Positions != PlayerPosition.Coach)
                        {
                            playerStats = new GamePlayerStats
                            {
                                PlayerId = player.PlayerId,
                                GameId = gameVM.Game.GameId

                            };
                        }
                        //gameVM.AwayTeamGPS.Add(playerStats);
                        _unitOfWork.GamePlayerStats.Add(playerStats);
                    }

                    //_unitOfWork.Game.Update(gameVM.Game);
                    TempData["success"] = "Mecz został utworzony.";
                }

                _unitOfWork.Save();
                return RedirectToAction("Details", "League", new { leagueId = _leagueId });

            }
            PopulateTeamSL(ref gameVM);

            return View(gameVM);
        }


        [Authorize(Roles = Utility.RoleNames.Role_Admin + "," + Utility.RoleNames.Role_Moderator)]
        public IActionResult DeletePOST(int? gameId, int? leagueId)
        {
            Game? game = _unitOfWork.Game.Get(p => p.GameId == gameId, null);
            if (game == null)
            {
                return NotFound();
            }

            _unitOfWork.Game.Delete(game);
            _unitOfWork.Save();

            TempData["success"] = "Spotkanie zostało usunięte";

            return RedirectToAction("Details", "League", new { leagueId = leagueId });
        }

        public void PopulateTeamSL(ref GameViewModel gameVM)
        {
            var teams = _unitOfWork.Team.GetAll(null);

            gameVM.HomeTeamSL = new List<SelectListItemWithImage>();
            gameVM.AwayTeamSL = new List<SelectListItemWithImage>();
            foreach (var team in teams)
            {
                if (team.LeagueId == gameVM.LeagueId)
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
            if (gameVM.Game != null)
            {
                int gameId = gameVM.Game.GameId;
                Game? actualGame = _unitOfWork.Game.Get(p => p.GameId == gameId, "HomeTeam.Players.PlayerStats,AwayTeam.Players.PlayerStats");
                gameVM.Game = actualGame;

                gameVM.AwayTeamGPS = new List<GamePlayerStats>();
                gameVM.HomeTeamGPS = new List<GamePlayerStats>();
                foreach (var homePlayerStats in gameVM.Game.HomeTeam.Players.SelectMany(p => p.PlayerStats))
                {
                    gameVM.HomeTeamGPS.Add(homePlayerStats);
                }

                foreach (var awayPlayerStats in gameVM.Game.AwayTeam.Players.SelectMany(p => p.PlayerStats))
                {
                    gameVM.AwayTeamGPS.Add(awayPlayerStats);
                }
            }

        }

    }
}
