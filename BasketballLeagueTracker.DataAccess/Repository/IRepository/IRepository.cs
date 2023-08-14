using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includeProp);
        T Get(Expression<Func<T, bool>> filter,string? includeProp);
        void Add(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entity);

    }
}
