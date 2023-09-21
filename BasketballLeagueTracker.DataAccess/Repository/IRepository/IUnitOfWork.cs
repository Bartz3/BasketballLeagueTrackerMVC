using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IArticleRepository Article { get; }
        ICommentRepository Comment { get; }
        ILeagueRepository League { get; }
        ITeamRepository Team { get; }
        IPlayerRepository Player { get; }
        IGameRepository Game { get; }
       
        void Save();

    }
}
