﻿using BasketballLeagueTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.DataAccess.Repository.IRepository
{
    public interface IFavouriteLeagueRepository : IRepository<FavouriteLeague>
    {
        void Update(FavouriteLeague favLeague);
        void Save();
      
    }
}
