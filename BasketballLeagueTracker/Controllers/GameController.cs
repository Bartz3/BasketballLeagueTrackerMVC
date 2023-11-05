using BasketballLeagueTracker.DataAccess.Migrations;
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

        public IActionResult Create(int? id, int? leagueId)
        {

            var gameVM = new GameViewModel();

            if (leagueId.HasValue)
            {
                gameVM.LeagueId = leagueId.Value;
            }
            PopulateTeamSL(ref gameVM);
            gameVM.Game = new Game();

            return View(gameVM);


        }
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
            gameVM.Game.GameDate = DateTime.Now; // TO DELETE
                                                 //{[Game.HomeTeam.Name, SubKey={Name}, Key=Game.HomeTeam.Name, ValidationState=Invalid]}

            List<Player> xd = new List<Player>();

            foreach (var homePlayerStats in gameVM.Game.HomeTeam.Players.SelectMany(p => p.PlayerStats).Where(x=>x.GameId==game.GameId))
            {
                gameVM.HomeTeamGPS.Add(homePlayerStats);
            }

            foreach (var awayPlayerStats in gameVM.Game.AwayTeam.Players.SelectMany(p => p.PlayerStats).Where(x => x.GameId == game.GameId))
            {
                gameVM.AwayTeamGPS.Add(awayPlayerStats);
            }


            //foreach (var homePlayerStats in gameVM.Game.HomeTeam.Players.SelectMany(p => p.PlayerStats))
            //{
            //    gameVM.HomeTeamGPS.Add(homePlayerStats);
            // Znajdź odpowiedni PlayerStats w kolekcji
            //var dbHomePlayerStats = game.HomeTeam.Players
            //.FirstOrDefault(p => p.PlayerId == homePlayerStats.PlayerId)
            //?.PlayerStats
            //.FirstOrDefault(s => s.PlayerId == homePlayerStats.PlayerId && s.GameId == homePlayerStats.GameId);

            //if (dbHomePlayerStats != null)
            //{
            //    // Aktualizuj dane PlayerStats
            //    dbHomePlayerStats.Points = homePlayerStats.Points;
            //    dbHomePlayerStats.TimeSpend = homePlayerStats.TimeSpend;
            //    // Dodaj inne właściwości PlayerStats
            //}
            //}

            //gameVM.Game.HomeTeam = game.HomeTeam;
            //gameVM.Game.AwayTeam = game.AwayTeam;

            //gameVM.Game.AwayTeam.Players = game.AwayTeam.Players;
            //gameVM.Game.HomeTeam.Players = game.HomeTeam.Players;

            return View(gameVM);

        }

        [HttpPost]
        public IActionResult Update(GameViewModel gameVM)
        {

            if (ModelState.IsValid)
            {
                var game = _unitOfWork.Game.Get(x => x.GameId == gameVM.Game.GameId, "HomeTeam.Players.PlayerStats,AwayTeam.Players.PlayerStats");
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
                    _unitOfWork.Game.Add(gameVM.Game);
                    _unitOfWork.Save();

                    var players = _unitOfWork.Player.GetAll("PlayerStats");
                    var homeTeamPlayers = new List<Player>();
                    var awayTeamPlayers = new List<Player>();

                    var test = gameVM.Game.GameId;

                    foreach (var player in players)
                    {
                        if (player.TeamId == gameVM.Game.HomeTeamId) homeTeamPlayers.Add(player);
                        else if (player.TeamId == gameVM.Game.AwayTeamId) awayTeamPlayers.Add(player);
                    }

                    foreach (var player in homeTeamPlayers)
                    {
                        var playerStats = new GamePlayerStats
                        {
                            PlayerId = player.PlayerId,
                            GameId = gameVM.Game.GameId

                        };
                        //gameVM.HomeTeamGPS.Add(playerStats);
                        _unitOfWork.GamePlayerStats.Add(playerStats);
                    }

                    foreach (var player in awayTeamPlayers)
                    {
                        var playerStats = new GamePlayerStats
                        {
                            PlayerId = player.PlayerId,
                            GameId = gameVM.Game.GameId

                        };
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
            if (gameVM.Game!=null)
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
