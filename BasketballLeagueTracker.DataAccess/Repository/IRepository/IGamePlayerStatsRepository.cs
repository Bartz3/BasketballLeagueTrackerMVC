using BasketballLeagueTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.DataAccess.Repository.IRepository
{
    public interface IGamePlayerStatsRepository : IRepository<GamePlayerStats>
    {
        void Update(GamePlayerStats stats);
        void Save();
      
    }
}
