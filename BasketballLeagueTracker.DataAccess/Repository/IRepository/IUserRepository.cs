using BasketballLeagueTracker.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Services.UserAccountMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.DataAccess.Repository.IRepository
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        void Save();
        List<Microsoft.AspNetCore.Identity.IdentityUserRole<string>> GetUsersRoles();
        List<Microsoft.AspNetCore.Identity.IdentityRole> GetRoles();
    }
}
