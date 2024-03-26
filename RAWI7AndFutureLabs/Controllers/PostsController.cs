using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RAWI7AndFutureLabs.Models;
using RAWI7AndFutureLabs.Services;
using RAWI7AndFutureLabs.Services.Post;

namespace RAWI7AndFutureLabs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postsService;

        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Posts>>> GetPosts()
        {
            var posts = await _postsService.GetPostsAsync();
            return Ok(posts);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Posts>> GetPost(int id)
        {
            var post = await _postsService.GetPostByIdAsync(id);
            if (post == null)
                return NotFound();
            return Ok(post);
        }
        [HttpPost]
        public async Task<ActionResult<Posts>> CreatePost(Posts post)
        {
            var createdPost = await _postsService.CreatePostAsync(post);
            return CreatedAtAction(nameof(GetPost), new { id = createdPost.Id }, createdPost);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Posts>> UpdatePost(int id, Posts post)
        {
            var updatedPost = await _postsService.UpdatePostAsync(id, post);
            if (updatedPost == null)
                return NotFound();
            return Ok(updatedPost);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            var result = await _postsService.DeletePostAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
