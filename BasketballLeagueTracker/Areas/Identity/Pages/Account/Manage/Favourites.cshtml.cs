// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BasketballLeagueTracker.Areas.Identity.Pages.Account.Manage
{
    public class FavouritesModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserRepository _userRepo;

        public FavouritesModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserRepository userRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepo = userRepo;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public FavModel Fav { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class FavModel
        {
            public List<FavouriteTeam> FavouriteTeams { get; set; }
            public List<FavouriteLeague> FavouriteLeagues { get; set; }
            public List<FavouritePlayer> FavouritePlayers { get; set; }

        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);

            var userTeams = _userRepo.Get(u => u.Id == user.Id, "FavouriteTeams.Team");
            List<FavouriteTeam> favouriteTeams = new List<FavouriteTeam>();

            foreach (var favTeam in userTeams.FavouriteTeams)
            {
                favouriteTeams.Add(favTeam);
            }

            var userPlayers = _userRepo.Get(u => u.Id == user.Id, "FavouritePlayers.Player");
            List<FavouritePlayer> favouritePlayers = new List<FavouritePlayer>();
            foreach (var favPlayer in userPlayers.FavouritePlayers)
            {
                favouritePlayers.Add(favPlayer);
                
            }

            var userLeagues= _userRepo.Get(u => u.Id == user.Id, "FavouriteLeagues.League");
            List<FavouriteLeague> favouriteLeagues = new List<FavouriteLeague>();
            foreach (var favLeague in userPlayers.FavouriteLeagues)
            {
                favouriteLeagues.Add(favLeague);
            }


            Fav = new FavModel
            {
                FavouritePlayers = favouritePlayers,
                FavouriteTeams = favouriteTeams,
                FavouriteLeagues = favouriteLeagues
            };

        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            //if (Input.PhoneNumber != phoneNumber)
            //{
            //    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            //    if (!setPhoneResult.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set phone number.";
            //        return RedirectToPage();
            //    }
            //}

            await _signInManager.RefreshSignInAsync(user);
            return RedirectToPage();
        }
    }
}
