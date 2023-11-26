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
    public class StadiumRepository : Repository<Stadium>, IStadiumRepository
    {
        private AppDbContext _db;

        public StadiumRepository(AppDbContext db) : base(db)
        {
            _db = db;
            

        }

        public void Update(Stadium stadium)
        {
            _db.Update(stadium);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }

}
