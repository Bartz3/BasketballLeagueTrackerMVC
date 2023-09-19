using BasketballLeagueTracker.Models;
using Microsoft.AspNetCore.Identity;

namespace BasketballLeagueTracker.DataAccess.Repository.IRepository
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        void Save();
        List<IdentityUserRole<string>> GetUsersRoles();
        List<IdentityRole> GetRoles();
    }
}
