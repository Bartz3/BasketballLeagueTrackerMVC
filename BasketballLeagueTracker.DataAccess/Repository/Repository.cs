using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BasketballLeagueTracker.DataAccess.Data;
using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace BasketballLeagueTracker.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        internal DbSet<T> dbSet;
        
        public Repository(AppDbContext appDb)
        {
            _db = appDb;
            this.dbSet = _db.Set<T>();
            //_db.Players.Include(x => x.Team);
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);            
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProp)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);

            if (includeProp != null)
            {
                AddIncludedProperties(includeProp,ref query);

            }

            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProp)
        {
            IQueryable<T> query = dbSet;

            if (includeProp != null)
            {
                AddIncludedProperties(includeProp, ref query);
            }
        
            if (query == null)
            {
                return Enumerable.Empty<T>();
            }else return query.ToList();
        }

        private void AddIncludedProperties(string includeProp, ref IQueryable<T> query)
        {
            if (!string.IsNullOrEmpty(includeProp))
            {
                string[] propPaths = includeProp.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var path in propPaths)
                {
                    var nestedProperties = path.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

                    string builtPath = "";
                    for (int i = 0; i < nestedProperties.Length; i++)
                    {
                        if (i > 0) builtPath += ".";
                        builtPath += nestedProperties[i];
                        query = query.Include(builtPath);
                    }
                }
            }
        }
    }
}
