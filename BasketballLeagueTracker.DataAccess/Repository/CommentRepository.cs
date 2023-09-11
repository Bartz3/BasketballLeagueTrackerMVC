using BasketballLeagueTracker.DataAccess.Data;
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
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private AppDbContext _db;

        public CommentRepository(AppDbContext db) : base(db) 
        { 
            _db=db;
        }

        public void Update(Comment comment)
        {
            _db.Comments.Update(comment);
        }
    }

}
