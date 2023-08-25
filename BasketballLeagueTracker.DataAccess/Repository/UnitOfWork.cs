using BasketballLeagueTracker.DataAccess.Data;
using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        public IPlayerRepository Player {get;private set;}
        public ITeamRepository Team {get;private set;}
        public ILeagueRepository League {get;private set;}
        private  AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            this._db = db;
            Player= new PlayerRepository(_db);
            Team = new TeamRepository(_db);
            League = new LeagueRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
