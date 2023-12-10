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
    public class SeasonStatisticsRepository : Repository<Article>, ISeasonStatisticsRepository
    {
        private AppDbContext _db;

        public SeasonStatisticsRepository(AppDbContext db) : base(db) // Passing db to Repository<Player>
        { 
            _db=db;
        }

        public void Update(Article article)
        {
            _db.Articles.Update(article);
        }
    }

}
