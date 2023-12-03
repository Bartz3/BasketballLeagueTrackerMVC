using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace BasketballLeagueTracker.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(IUserRepository userRepository,UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
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
            var usersRoles = _userRepository.GetUsersRoles();
            var roles = _userRepository.GetRoles();

            foreach (var user in users)
            {
                var roleId = usersRoles.FirstOrDefault(x => x.UserId == user.Id).RoleId;

                user.Role = roles.FirstOrDefault(x => x.Id == roleId).Name;

            }

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

        public IActionResult EditUserRole(string userId)
        {
            var user = _userRepository.Get(x=>x.Id==userId, null);
            bool isLockedOut=false;
            if (user != null)
            {
                isLockedOut = user.LockoutEnd.HasValue && user.LockoutEnd > DateTimeOffset.UtcNow;
            }
            ViewBag.isLockedOut = isLockedOut;
            var roleId = _userRepository.GetUsersRoles().FirstOrDefault(x => x.UserId == user.Id).RoleId;
            RoleViewModel roleVM = new RoleViewModel()
            {
                User = user,
                Roles = _userRepository.GetRoles().Select(
                    x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Name
                    })
            };
            if(user.Nickname==null)
            {
                user.Nickname = "---";
            }
            roleVM.User.Role = _userRepository.GetRoles().FirstOrDefault(x => x.Id == roleId).Name;
            return View(roleVM);
        }

        [HttpPost]
        public IActionResult EditUserRole(RoleViewModel roleVM)
        {
            // Receving old roleId
            var roleId = _userRepository.GetUsersRoles().FirstOrDefault(x => x.UserId == roleVM.User.Id).RoleId;
            // Old role Name
            var roleName = _userRepository.GetRoles().FirstOrDefault(x => x.Id == roleId).Name;

            var user = _userRepository.Get(x => x.Id == roleVM.User.Id, null);
            //Remove old role
            _userManager.RemoveFromRoleAsync(user, roleName).GetAwaiter().GetResult();
            // Add user to new role
            _userManager.AddToRoleAsync(user, roleVM.User.Role).GetAwaiter().GetResult();

            TempData["success"] = "Rola została zmieniona";
            return RedirectToAction("Index");
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

        [HttpPost]
        public IActionResult LockAccount([FromBody] string Id)
        {
            var user = _userRepository.Get(x => x.Id == Id, null);
            if (user == null)
            {
                return Json(new { success = false, message = "Błąd podczas blokowania konta." });
            }
            //Unlock user account
            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now;
                _userRepository.Save();
                return Json(new { success = true, message = "Konto zostało odblokowane." });
            }
            else // Lock user account
            {
                user.LockoutEnd = DateTime.Now.AddYears(25);
                _userRepository.Save();
                return Json(new { success = true, message = "Konto zostało zablokowane." });
            }



        }
    }
}
