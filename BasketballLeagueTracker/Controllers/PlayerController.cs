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
            _db = db;
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
            if (ModelState.IsValid)
            {
                _db.Players.Add(player);
                _db.SaveChanges();
                TempData["success"] = "Zawodnik został dodany";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Player? player = _db.Players.FirstOrDefault(p => p.PlayerId == id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        [HttpPost]
        public IActionResult Edit(Player player)
        {
            if (ModelState.IsValid)
            {
                _db.Players.Update(player);
                _db.SaveChanges();
                TempData["success"] = "Zawodnik został zmodyfikowany";
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
            Player? player = _db.Players.FirstOrDefault(p => p.PlayerId == id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Player? player = _db.Players.FirstOrDefault(p => p.PlayerId == id);
            if (player == null)
            {
                return NotFound();
            }

            _db.Players.Remove(player);

            _db.SaveChanges();
            TempData["success"] = "Zawodnik został usunięty";
            return RedirectToAction("Index");
        }


    }
}
