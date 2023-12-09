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
    public class GameRepository : Repository<Game>, IGameRepository
    {
        private AppDbContext _db;

        public GameRepository(AppDbContext db) : base(db) 
        { 
            _db=db;
        }


        public void Update(int gameID,Game game)
        {

            var existingGame = _db.Games.FirstOrDefault(g => g.GameId == gameID);
            if (existingGame == null) return;
            existingGame.AwayTeamScore = game.AwayTeamScore;
            existingGame.HomeTeamScore = game.HomeTeamScore;
            existingGame.Status = game.Status;

            _db.Games.Update(existingGame);
        }
    }

}
