using BasketballLeagueTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.DataAccess.Repository.IRepository
{
    public interface IFavouriteTeamRepository : IRepository<FavouriteTeam>
    {
        void Update(FavouriteTeam favTeam);
        void Save();
      
    }
}
