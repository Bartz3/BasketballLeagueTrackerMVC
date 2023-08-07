using BasketballLeagueTracker.DataAccess.Data;
using BasketballLeagueTracker.DataAccess.Repository;
using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.Models;
using BasketballLeagueTracker.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace BasketballLeagueTracker.Controllers
{
    public class TeamController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeamController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var teamsList = _unitOfWork.Team.GetAll();

            return View(teamsList);
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
                Team? team = _unitOfWork.Team.Get(p => p.TeamId == id);

                var availablePlayers = _unitOfWork.Player.GetAll()
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
                Team team;

                if (file != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        teamVM.Team.TeamLogo = memoryStream.ToArray();
                    }
                }
                team = teamVM.Team;

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
            Team? team = _unitOfWork.Team.Get(p => p.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Team? team = _unitOfWork.Team.Get(p => p.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }

            _unitOfWork.Team.Delete(team);
            _unitOfWork.Save();

            TempData["success"] = "Zespół został usunięty";

            return RedirectToAction("Index");
        }


    }
}
