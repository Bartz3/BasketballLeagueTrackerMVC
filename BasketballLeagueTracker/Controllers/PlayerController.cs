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

        public IActionResult Upsert(int? id)
        {
            if(id == null || id == 0)
            {
                return View();
            }
            else
            {
                Player? player = _unitOfWork.Player.Get(p => p.PlayerId == id);
                return View(player);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Player player)
        {

            if (ModelState.IsValid)
            {
                if(player.PlayerId ==0)
                {

                _unitOfWork.Player.Add(player);
                TempData["success"] = "Zawodnik został dodany";
                }
                else
                {
                    _unitOfWork.Player.Update(player);
                    TempData["success"] = "Zawodnik został zmodyfikowany";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Player? player = _unitOfWork.Player.Get(p => p.PlayerId == id);
        //    if (player == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(player);
        //}

        //[HttpPost]
        //public IActionResult Edit(Player player)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Player.Update(player);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Zawodnik został zmodyfikowany";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

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
