using System.Collections.Generic;
using System.Threading.Tasks;
using PrimeAspApi.Models;
using PrimeAspApi.Repositories;

namespace PrimeAspApi.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            return await _commentRepository.GetAllCommentsAsync();
        }

        public async Task<Comment> GetCommentById(int id)
        {
            return await _commentRepository.GetCommentByIdAsync(id);
        }

        public async Task AddComment(Comment comment)
        {
            await _commentRepository.AddCommentAsync(comment);
            await _commentRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteComment(int id)
        {
            return await _commentRepository.DeleteCommentAsync(id);
        }
    }
}
