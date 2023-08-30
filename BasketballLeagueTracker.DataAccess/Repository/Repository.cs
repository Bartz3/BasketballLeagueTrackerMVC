﻿using System;
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

            AddIncludedProperties(includeProp,ref query);

            return query.FirstOrDefault();
        }



        public IEnumerable<T> GetAll(string? includeProp)
        {
            IQueryable<T> query = dbSet;

            AddIncludedProperties(includeProp,ref query);

            if (query == null)
            {
                return Enumerable.Empty<T>();
            }else return query.ToList();
        }

        //private void AddIncludedProperties(string includeProp, ref IQueryable<T> query)
        //{
        //    if (!string.IsNullOrEmpty(includeProp))
        //    {
        //        string[] propArray = includeProp.Split(',');
        //        propArray = propArray.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
        //        foreach (var prop in propArray)
        //        {
        //            query = query.Include(prop);
        //        }
        //    }
        //}
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


        //private void AddIncludedProperties(string includeProp, ref IQueryable<T> query)
        //{
        //    if (!string.IsNullOrEmpty(includeProp))
        //    {
        //        string[] propArray = includeProp.Split(',');
        //        foreach (var prop in propArray)
        //        {
        //            var nestedProps = prop.Split("->");
        //            var mainProperty = nestedProps[0];
        //            query = query.Include(mainProperty);
        //            for (int i = 1; i < nestedProps.Length; i++)
        //            {
        //                var subProperty = nestedProps[i];
        //                query = ((IIncludableQueryable<T, object>)query).Include(e => EF.Property<object>(e, subProperty));
        //            }
        //        }
        //    }
        //}


    }
}
