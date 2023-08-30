using BasketballLeagueTracker.DataAccess.Data;
using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.DataAccess.Repository
{
    public class FavouriteTeamRepository : Repository<FavouriteTeam>, IFavouriteTeamRepository
    {
        private AppDbContext _db;
        //private readonly ILogger _logger;

        public FavouriteTeamRepository(AppDbContext db) : base(db)
        {
            _db = db;
            

        }

        public void Update(FavouriteTeam favTeam)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }

}
