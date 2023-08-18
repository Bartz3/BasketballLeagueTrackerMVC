using BasketballLeagueTracker.DataAccess.Data;
using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.DataAccess.Repository
{
    public class LeagueRepository : Repository<League>, ILeagueRepository
    {
        private AppDbContext _db;

        public LeagueRepository(AppDbContext db) : base(db) 
        { 
            _db=db;
        }


        public void Update(League league)
        {
            _db.Leagues.Update(league);
        }
    }

}
