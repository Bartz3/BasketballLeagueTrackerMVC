using BasketballLeagueTracker.DataAccess.Repository;
using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.Models.ModelsValidation;
using BasketballLeagueTracker.Utility.DataGenerator;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BasketballLeagueTracker.DataAccess.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IFavouritePlayerRepository, FavouritePlayerRepository>();
            services.AddScoped<IFavouriteTeamRepository, FavouriteTeamRepository>();
            services.AddScoped<IFavouriteLeagueRepository, FavouriteLeagueRepository>();
 

            services.AddScoped<IUserRepository, UserRepository>();
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
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;
            });
        }
    }
}