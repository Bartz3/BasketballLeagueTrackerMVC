using BasketballLeagueTracker.DataAccess.Data;
using BasketballLeagueTracker.DataAccess.Repository;
using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BasketballLeagueTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, AppDbContext appDb)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _context = appDb;
        }

        public IActionResult Index()
        {
            ViewBag.TeamList = new LinkedList<SelectListItem>();

            return View();
        }
        private readonly AppDbContext _context;
        [HttpGet("api/search")]
        public IActionResult Search(string q, string type)
        {
            if (string.IsNullOrEmpty(q)) return BadRequest("Query cannot be empty.");

            switch (type.ToLower())
            {
                case "player":
                    var players = _context.Players
                        .Where(p => p.Name.Contains(q))
                        .Select(p => new
                        {
                            id = p.PlayerId,
                            name = p.FullName,
                            type = "player"
                        })
                        .ToList();
                    return Ok(players);

                case "team":
                    var teams = _context.Teams
                        .Where(t => t.Name.Contains(q))
                        .Select(t => new
                        {
                            id = t.TeamId,
                            name = t.Name,
                            type = "team"
                        })
                        .ToList();
                    return Ok(teams);

                case "league":
                    var leagues = _context.Leagues
                        .Where(l => l.Name.Contains(q))
                        .Select(l => new
                        {
                            id = l.LeagueId,
                            name = l.Name,
                            type = "league"
                        })
                        .ToList();
                    return Ok(leagues);

                default:
                    return BadRequest("Invalid search type.");
            }
        }
        public JsonResult GetTeams()
        {
            return Json(_context.Teams.ToList());
        }
        public JsonResult SearchJson(string q, string type)
        {
            if (string.IsNullOrEmpty(q)) return Json(Error());

            switch (type.ToLower())
            {
                case "player":
                    var players = _context.Players
                        .Where(p => p.Name.Contains(q))
                        .Select(p => new
                        {
                            id = p.PlayerId,
                            name = p.FullName,
                            type = "player"
                        })
                        .ToList();
                    return Json(players.ToList());

                case "team":
                    var teams = _context.Teams
                        .Where(t => t.Name.Contains(q))
                        .Select(t => new
                        {
                            id = t.TeamId,
                            name = t.Name,
                            type = "team"
                        })
                        .ToList();
                     return Json(teams.ToList());

                case "league":
                    var leagues = _context.Leagues
                        .Where(l => l.Name.Contains(q))
                        .Select(l => new
                        {
                            id = l.LeagueId,
                            name = l.Name,
                            type = "league"
                        })
                        .ToList();
                    return Json(leagues.ToList());

                default:
                    return Json(Error());
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}