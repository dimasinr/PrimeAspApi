using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrimeAspApi.Data;
using PrimeAspApi.Models;

namespace PrimeAspApi.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Comments.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task AddCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            // var comment = await _context.Comments.FindAsync(id);
            // return await _context.Comments.ExecuteDeleteAsync(comment);
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return false; // Not Found
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
