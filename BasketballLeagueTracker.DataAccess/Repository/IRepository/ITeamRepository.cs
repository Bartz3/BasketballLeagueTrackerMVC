using BasketballLeagueTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.DataAccess.Repository.IRepository
{
    public interface ITeamRepository : IRepository<Team>
    {
        void Update(int teamId,Team team );
        void RemovePlayerFromTeam(int teamId, int playerId);
    }
}
