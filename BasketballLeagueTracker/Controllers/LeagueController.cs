using BasketballLeagueTracker.DataAccess.Data;
using BasketballLeagueTracker.DataAccess.Repository;
using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;
using BasketballLeagueTracker.ViewModels;
using BasketballLeagueTracker.DataAccess.Migrations;

namespace BasketballLeagueTracker.Controllers
{
    public class LeagueController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LeagueController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var leagueList = _unitOfWork.League.GetAll(null);

            return View(leagueList);
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                var leagueVM = new LeagueViewModel()
                {
                    League=new League(),
                    Image=null,
                };
                return View(leagueVM);
            }
            else
            {
                League? league = _unitOfWork.League.Get(p => p.LeagueId == id, null);
                var leagueVM = new LeagueViewModel()
                {
                    League = league
                };
              
                return View(leagueVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(LeagueViewModel leagueVM, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        leagueVM.League.Logo = memoryStream.ToArray();
                    }
                }


                if (leagueVM.League.LeagueId == 0)
                {

                    _unitOfWork.League.Add(leagueVM.League);
                    TempData["success"] = "Liga została dodana";
                }
                else
                {
                    _unitOfWork.League.Update(leagueVM.League);
                    TempData["success"] = "Liga została zmodyfikowana";
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
            League? league = _unitOfWork.League.Get(p => p.LeagueId == id, null);

            if (league == null)
            {
                return NotFound();
            }
            return View(league);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            League? league = _unitOfWork.League.Get(p => p.LeagueId == id, null);
            if (league == null)
            {
                return NotFound();
            }

            _unitOfWork.League.Delete(league);
            _unitOfWork.Save();

            TempData["success"] = "Liga została usunięta";

            return RedirectToAction("Index");
        }


    }
}
