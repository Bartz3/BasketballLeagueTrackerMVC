﻿using BasketballLeagueTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.DataAccess.Repository.IRepository
{
    public interface IPlayerRepository : IRepository<Player>
    {
        void Update(int playerId,Player player);
      
    }
}
