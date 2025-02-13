using System.Collections.Generic;
using System.Threading.Tasks;
using PrimeAspApi.Models;
using PrimeAspApi.Repositories;

namespace PrimeAspApi.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;

        public CommentService(ICommentRepository commentRepository, IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            return await _commentRepository.GetAllCommentsAsync();
        }

        public async Task<Comment> GetCommentById(int id)
        {
            return await _commentRepository.GetCommentByIdAsync(id);
        }

        public async Task<Comment> CreateCommentAsync(string content, int authorId)
        {
            // Pastikan user ada sebelum menambahkan komentar
            var user = await _userRepository.GetUserByIdAsync(authorId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            var comment = new Comment { Content = content, AuthorId = authorId };
            return await _commentRepository.CreateCommentAsync(comment);
        }

        public async Task<bool> DeleteComment(int id)
        {
            return await _commentRepository.DeleteCommentAsync(id);
        }
    }
}
