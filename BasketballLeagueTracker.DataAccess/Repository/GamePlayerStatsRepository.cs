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
    public class GamePlayerStatsRepository : Repository<GamePlayerStats>, IGamePlayerStatsRepository
    {
        private AppDbContext _db;

        public GamePlayerStatsRepository(AppDbContext db) : base(db)
        {
            _db = db;
            

        }

        public void Update(GamePlayerStats gps)
        {
           _db.GamePlayerStats.Update(gps);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }

}
