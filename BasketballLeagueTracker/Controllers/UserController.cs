using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BasketballLeagueTracker.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Details(string? userId)
        {
            var user = _userRepository.Get(t => t.Id == userId, null);

            return View(user);
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAll(null).ToList();

            var ignoreCyclesInJSON = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            return Json(new { data = users }, ignoreCyclesInJSON);
        }


        public IActionResult Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ApplicationUser? user = _userRepository.Get(t => t.Id == id, null);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(string? id)
        {
            ApplicationUser? user = _userRepository.Get(p => p.Id == id, null);
            if (user == null)
            {
                return NotFound();
            }

            _userRepository.Delete(user);
            _userRepository.Save();

            TempData["success"] = "Użytkownik został usunięty";

            return RedirectToAction("Index");
        }


    }
}
