using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DatabaseContext context)
            : base(context) { }

        public Post FindById(int id)
        {
            return DbSet.Where(p => p.IsActive && p.Id == id)
                .Include(i => i.Images.Where(i => i.PostId == id))
                .Include(likes => likes.Likes.Where(l => l.PostId == id))
                .Include(d => d.Dislikes.Where(d => d.PostId == id))
                .FirstOrDefault();
        }

        public IEnumerable<Post> GetAll()
        {
            return Query(p => p.IsActive)
                .Include(i => i.Images.Take(1));
        }

        public bool Like(int postId, bool isLike, int userId)
        {
            try
            {
                if (isLike)
                {
                    var likeExists = _context.PostLikes.Where(p => p.PostId == postId && p.UserId == userId).Count() > 0;
                    if (likeExists)
                    {
                        _context.Database.ExecuteSqlRaw($"DELETE FROM PostLikes WHERE postId = {postId} AND userId = {userId}");
                        return true;
                    }
                }
                else
                {
                    var dislikeExists = _context.PostDislikes.Where(p => p.PostId == postId && p.UserId == userId).Count() > 0;
                    if (dislikeExists)
                    {
                        _context.Database.ExecuteSqlRaw($"DELETE FROM PostDislikes WHERE postId = {postId} AND userId = {userId}");
                        return true;
                    }
                }

                _context.Database.ExecuteSqlRaw($"DELETE FROM PostLikes WHERE postId = {postId} AND userId = {userId}");
                _context.Database.ExecuteSqlRaw($"DELETE FROM PostDislikes WHERE postId = {postId} AND userId = {userId}");

                string query = "INSERT INTO Post";
                if (isLike)
                    query += "Likes";
                else
                    query += "Dislikes";

                query += $" VALUES ({postId}, {userId})";

                _context.Database.ExecuteSqlRaw(query);
                return true;
            }catch(Exception)
            {
                return false;
            }
        }

    }
}
