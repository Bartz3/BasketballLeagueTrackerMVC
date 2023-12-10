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
        IGameRepository Game { get; }
        IGamePlayerStatsRepository GamePlayerStats { get; }
        ISeasonStatisticsRepository SeasonStatistics { get; }
        ICommentRepository Comment { get; }
        ILeagueRepository League { get; }
        ITeamRepository Team { get; }
        IPlayerRepository Player { get; }
        IStadiumRepository Stadium{ get; }
       
        void Save();

    }
}
