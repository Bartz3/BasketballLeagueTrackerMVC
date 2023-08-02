using BasketballLeagueTracker.DataAccess.Data;
using BasketballLeagueTracker.DataAccess.Repository;
using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace BasketballLeagueTracker.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlayerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var playersList = _unitOfWork.Player.GetAll();

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
                _unitOfWork.Player.Add(player);
                _unitOfWork.Save();
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
            Player? player = _unitOfWork.Player.Get(p => p.PlayerId == id);
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
                _unitOfWork.Player.Update(player);
                _unitOfWork.Save();
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
            Player? player = _unitOfWork.Player.Get(p => p.PlayerId == id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Player? player = _unitOfWork.Player.Get(p => p.PlayerId == id);
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
