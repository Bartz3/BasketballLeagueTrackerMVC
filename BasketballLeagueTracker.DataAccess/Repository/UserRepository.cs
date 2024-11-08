﻿using BasketballLeagueTracker.DataAccess.Data;
using BasketballLeagueTracker.DataAccess.Repository.IRepository;
using BasketballLeagueTracker.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.DataAccess.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private AppDbContext _db;

        public UserRepository(AppDbContext db) : base(db) // Passing db to Repository<Player>
        {
            _db = db;


        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public List<IdentityUserRole<string>> GetUsersRoles()
        {
            return _db.UserRoles.ToList();
        }

        public List<IdentityRole> GetRoles()
        {
            return _db.Roles.ToList();
        }


    }
}
