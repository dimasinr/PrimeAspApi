using System.Collections.Generic;
using System.Threading.Tasks;
using PrimeAspApi.Models;

namespace PrimeAspApi.Repositories
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentsAsync();
        Task<Comment> GetCommentByIdAsync(int id);
        // Task AddCommentAsync(Comment comment);
        Task<Comment> CreateCommentAsync(Comment comment);

        Task<bool> DeleteCommentAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
