using System.Collections.Generic;
using System.Threading.Tasks;
using PrimeAspApi.Models;

namespace PrimeAspApi.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetAllComments();
        Task<Comment> GetCommentById(int id);
        // Task<Comment> CreateCommentAsync(Comment comment);

        Task<Comment> CreateCommentAsync(string comment, int authorId);
        Task<bool> DeleteComment(int id);
    }
}
