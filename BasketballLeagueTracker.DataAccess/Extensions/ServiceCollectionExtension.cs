using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.DataAccess.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BasketballLeagueTracker.Utility.DataGenerator;

namespace BasketballLeagueTracker.DataAccess.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFavouriteTeamRepository, FavouriteTeamRepository>();
            services.AddScoped<IUserRepository,UserRepository>();

            services.AddTransient<TeamGenerator>(); // Generation of random Teams
            // Identity password options
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 6;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber=false;
                options.SignIn.RequireConfirmedAccount = false;

            });
        }
    }
}
