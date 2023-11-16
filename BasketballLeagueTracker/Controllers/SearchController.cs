using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace BasketballLeagueTracker.Controllers
{
    public class SearchController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public SearchController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //var playersList = _unitOfWork.Player.GetAll(includeProp: "Team.League");

            return View();
        }

        [HttpGet]
        public IActionResult GetPlayerSearchInfo()
        {
            var playerSearchInfo = _unitOfWork.Player.GetAll(includeProp: "Team.League").ToList();

            var ignoreCyclesInJSON = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            return Json(new { data = playerSearchInfo }, ignoreCyclesInJSON);
        }

        [HttpGet]
        public IActionResult GetTeamSearchInfo()
        {
            var teamSearchInfo = _unitOfWork.Team.GetAll("Players").ToList();

            var ignoreCyclesInJSON = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            return Json(new { data = teamSearchInfo }, ignoreCyclesInJSON);
        }

        [HttpGet]
        public IActionResult GetLeagueSearchInfo()
        {
            var leagueSearchInfo = _unitOfWork.League.GetAll("Teams").ToList();

            var ignoreCyclesInJSON = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            return Json(new { data = leagueSearchInfo }, ignoreCyclesInJSON);
        }
    }
}
