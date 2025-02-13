using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using PrimeAspApi.Models;
using PrimeAspApi.Services;

namespace PrimeAspApi.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAllComments()
        {
            var comments = await _commentService.GetAllComments();

            var response = comments.Select(comment => new CommentsDto
            {
                Id = comment.Id,
                Content = comment.Content,
                AuthorId = comment.AuthorId,
                CreatedAt = comment.CreatedAt,
                Author = comment.Author == null ? null : new UserDto
                {
                    Id = comment.Author.Id,
                    Name = comment.Author.Name,
                    Email = comment.Author.Email
                }
            }).ToList();

            return Ok(response);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetCommentById(int id)
        {
            var comment = await _commentService.GetCommentById(id);
            if (comment == null) return NotFound();
            return Ok(comment);
        }
        // public async Task<CommentDto> GetCommentByIdAsync(int id)
        // {
        //     var comment = await _commentRepository.GetCommentByIdAsync(id);
        //     if (comment == null) return null;

        //     return new CommentDto
        //     {
        //         Id = comment.Id,
        //         Content = comment.Content,
        //         AuthorId = comment.AuthorId,
        //         CreatedAt = comment.CreatedAt,
        //         Author = new UserDto
        //         {
        //             Id = comment.Author.Id,
        //             Name = comment.Author.Name,
        //             Email = comment.Author.Email
        //         }
        //     };
        // }

        [HttpPost]
        public async Task<IActionResult> PostComment([FromBody] CommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentService.CreateCommentAsync(commentDto.Content, commentDto.AuthorId);

            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                var comment = await _commentService.GetCommentById(id);

                if (comment == null)
                {
                    // return NotFound(new {message = "Komentar tidak ditemukan"});
                    // return StatusCode(StatusCodes.Status404NotFound, "Komentar tidak ditemukan");
                    return StatusCode(StatusCodes.Status404NotFound, new {message = "Komentar tidak ditemukan"});

                }

                await _commentService.DeleteComment(id);
                return StatusCode(StatusCodes.Status200OK, new {message = "Berhasil menghapus komentar"});
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new {message="Error deleting data"});
            }
        }
    }
    public class CommentDto
    {
        public required string Content { get; set; }
        public int AuthorId { get; set; }
    }

    public class CommentsDto
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int AuthorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDto? Author { get; set; } 
    }

    public class UserDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
}
