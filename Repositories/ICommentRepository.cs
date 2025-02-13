using System.Collections.Generic;
using System.Threading.Tasks;
using PrimeAspApi.Models;

namespace PrimeAspApi.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllCommentsAsync();
        Task<Comment> GetCommentByIdAsync(int id);
        Task AddCommentAsync(Comment comment);
        Task<bool> DeleteCommentAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
