using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.Utility.DataGenerator;
using Microsoft.AspNetCore.Mvc;

namespace BasketballLeagueTracker.Controllers
{
    public class AdminController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public AdminController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //GenerateTeam(8);
            GeneratePlayer(8);

            return View();
        }

        private void GenerateTeam(int length)
        {

            TeamGenerator teamGenerator = new TeamGenerator();
            Team team;
            for (int i = 0; i < length; i++)
            {
                team = teamGenerator.GenerateTeam();
                _unitOfWork.Team.Add(team);

            }
            _unitOfWork.Save();
        }

        private void GeneratePlayer(int length)
        {
            PlayerGenerator playerGenerator = new PlayerGenerator();
            Player player;
            for (int i = 0; i < length; i++)
            {
                player = playerGenerator.GeneratePlayer();
                _unitOfWork.Player.Add(player);
            }
            _unitOfWork.Save();

        }
    }
}
