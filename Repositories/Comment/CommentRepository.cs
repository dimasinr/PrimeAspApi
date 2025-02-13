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

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            var comments = await _context.Comments
                .Include(c => c.Author) // Ensure Author is included
                .ToListAsync();

            // Debugging: Print loaded comments
            foreach (var comment in comments)
            {
                Console.WriteLine($"Comment: {comment.Content}, Author: {comment.Author?.Name ?? "NULL"}");
            }

            return comments;
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Comments
                .Include(c => c.Author) // Ensure Author is loaded
                .FirstOrDefaultAsync(c => c.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
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
