using BasketballLeagueTracker.DataAccess.Data;
using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.Models;
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
       public  IArticleRepository Article { get; set; }
        public IPlayerRepository Player {get;private set;}
        public ITeamRepository Team {get;private set;}
        public ILeagueRepository League {get;private set;}
        public ICommentRepository Comment {get;private set;}
        public IGameRepository Game {get;private set;}
        public IGamePlayerStatsRepository GamePlayerStats { get;private set;}
        public IStadiumRepository Stadium{ get;private set;}
        public ISeasonStatisticsRepository SeasonStatistics{ get;private set;}

        private  AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            this._db = db;
            Player= new PlayerRepository(_db);
            Team = new TeamRepository(_db);
            League = new LeagueRepository(_db);
            Article = new ArticleRepository(_db);
            Comment= new CommentRepository(_db);
            Game= new GameRepository(_db);
            GamePlayerStats= new GamePlayerStatsRepository(_db);
            Stadium= new StadiumRepository(_db);
            SeasonStatistics= new SeasonStatisticsRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
