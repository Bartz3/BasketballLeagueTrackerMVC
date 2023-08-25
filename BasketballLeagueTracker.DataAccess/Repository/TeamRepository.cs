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
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        private AppDbContext _db;
        //private readonly ILogger _logger;

        public TeamRepository(AppDbContext db) : base(db)
        {
            _db = db;
            //_logger = logger;
        }

        public void RemovePlayerFromTeam(int teamId, int playerId)
        {

            var player = _db.Players.Where(p => p.PlayerId == playerId && p.TeamId == teamId).FirstOrDefault();

                Team? team = _db.Teams.FirstOrDefault(p => p.TeamId == teamId);
                player.IsInTeam = false;
                team.Players.Remove(player);


            _db.SaveChanges();

        }

        public override void Delete(Team entity)
        {
            var playersInDeletedTeam = _db.Players.Where(p => p.TeamId == entity.TeamId);

            // Removing team props from all players included to the deleted team
            foreach (var player in playersInDeletedTeam)
            {
                player.TeamId = null;
                player.Team = null;
                player.IsInTeam= false;
            }

            _db.Teams.Remove(entity);
        }
        public void Update(Team team)
        {
            _db.Teams.Update(team);
        }
    }

}
