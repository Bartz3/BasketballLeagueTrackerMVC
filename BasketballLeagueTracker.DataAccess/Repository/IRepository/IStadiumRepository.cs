using BasketballLeagueTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.DataAccess.Repository.IRepository
{
    public interface IStadiumRepository : IRepository<Stadium>
    {
        void Update(Stadium stadium);
        void Save();
      
    }
}
