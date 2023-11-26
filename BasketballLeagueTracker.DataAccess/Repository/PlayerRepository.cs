using BasketballLeagueTracker.DataAccess.Data;
using BasketballLeagueTracker.DataAccess.Migrations;
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
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        private AppDbContext _db;

        public PlayerRepository(AppDbContext db) : base(db) // Passing db to Repository<Player>
        { 
            _db=db;
        }


        //public void Update(Player player)
        //{

        //    _db.Players.Update(player);
        //}
        public void Update(int playerId,Player player)
        {
            var existingPlayer=_db.Players.First(p=>p.PlayerId==playerId);

            existingPlayer.Name = player.Name;
            existingPlayer.Surname = player.Surname;
            existingPlayer.UniformNumber = player.UniformNumber;
            existingPlayer.Birthday = player.Birthday;
            existingPlayer.Country = player.Country;
            existingPlayer.Weight = player.Weight;
            existingPlayer.Height = player.Height;
            existingPlayer.Positions = player.Positions;
            existingPlayer.Photo= player.Photo;

            _db.Players.Update(existingPlayer);
        }
    }

}
