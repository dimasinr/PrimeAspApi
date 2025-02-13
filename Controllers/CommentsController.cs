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
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetCommentById(int id)
        {
            var comment = await _commentService.GetCommentById(id);
            if (comment == null) return NotFound();
            return Ok(comment);
        }

        [HttpPost]
        public async Task<ActionResult> AddComment([FromBody] Comment comment)
        {
            await _commentService.AddComment(comment);
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
}
