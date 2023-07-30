using BasketballLeagueTracker.Data;
using BasketballLeagueTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace BasketballLeagueTracker.Controllers
{
    public class PlayerController : Controller
    {
        private readonly AppDbContext _db;

        public PlayerController(AppDbContext db)
        {
            _db= db;
        }

        public IActionResult Index()
        {
            var playersList = _db.Players.ToList();

            return View(playersList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Player player)
        {
            _db.Players.Add(player);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
