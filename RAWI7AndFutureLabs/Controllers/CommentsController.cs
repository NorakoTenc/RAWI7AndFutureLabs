using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RAWI7AndFutureLabs.Models;
using RAWI7AndFutureLabs.Services;
using RAWI7AndFutureLabs.Services.Comment;

namespace RAWI7AndFutureLabs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Comments>>> GetComments()
        {
            var comments = await _commentsService.GetCommentsAsync();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comments>> GetComment(int id)
        {
            var comment = await _commentsService.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound();
            return Ok(comment);
        }

        [HttpPost]
        public async Task<ActionResult<Comments>> CreateComment(Comments comment)
        {
            var createdComment = await _commentsService.CreateCommentAsync(comment);
            return CreatedAtAction(nameof(GetComment), new { id = createdComment.Id }, createdComment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Comments>> UpdateComment(int id, Comments comment)
        {
            var updatedComment = await _commentsService.UpdateCommentAsync(id, comment);
            if (updatedComment == null)
                return NotFound();
            return Ok(updatedComment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            var result = await _commentsService.DeleteCommentAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
