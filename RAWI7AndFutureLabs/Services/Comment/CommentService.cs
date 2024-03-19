using RAWI7AndFutureLabs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RAWI7AndFutureLabs.Models;
using RAWI7AndFutureLabs.Services.Comment;

namespace RAWI7AndFutureLabs.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly List<Comments> _comments;

        public CommentsService()
        {
            _comments = new List<Comments>
            {
                new Comments {Id = 1, Content = "Comment 1 content",CreatedAt = DateTime.Now.AddDays(-1),UserId = 1, PostId = 1 },
                new Comments {Id = 2, Content = "Comment 2 content",CreatedAt = DateTime.Now.AddDays(-2),UserId = 2, PostId = 2 },
                new Comments {Id = 3, Content = "Comment 3 content",CreatedAt = DateTime.Now.AddDays(-3),UserId = 3, PostId = 3 },
                new Comments {Id = 4, Content = "Comment 4 content",CreatedAt = DateTime.Now.AddDays(-4),UserId = 4, PostId = 4 },
                new Comments {Id = 5, Content = "Comment 5 content",CreatedAt = DateTime.Now.AddDays(-5),UserId = 5, PostId = 5 },
                new Comments {Id = 6, Content = "Comment 6 content",CreatedAt = DateTime.Now.AddDays(-6),UserId = 6, PostId = 6 },
                new Comments {Id = 7, Content = "Comment 7 content",CreatedAt = DateTime.Now.AddDays(-7),UserId = 7, PostId = 7 },
                new Comments {Id = 8, Content = "Comment 8 content",CreatedAt = DateTime.Now.AddDays(-8),UserId = 8, PostId = 8 },
                new Comments {Id = 9, Content = "Comment 9 content",CreatedAt = DateTime.Now.AddDays(-9),UserId = 9, PostId = 9 },
                new Comments {Id = 10, Content = "Comment 10 content",CreatedAt = DateTime.Now.AddDays(-10),UserId = 10, PostId = 10 },
            };
        }
        public async Task<List<Comments>> GetCommentsAsync()
        {
            return await Task.FromResult(_comments);
        }
        public async Task<Comments> GetCommentByIdAsync(int id)
        {
            return await Task.FromResult(_comments.FirstOrDefault(c => c.Id == id));
        }
        public async Task<Comments> CreateCommentAsync(Comments comment)
        {
            comment.Id = _comments.Count + 1;
            comment.CreatedAt = DateTime.Now;
            _comments.Add(comment);
            return await Task.FromResult(comment);
        }
        public async Task<Comments> UpdateCommentAsync(int id, Comments comment)
        {
            var existingComment = _comments.FirstOrDefault(c => c.Id == id);
            if (existingComment != null)
            {
                existingComment.Content = comment.Content;
                existingComment.CreatedAt = DateTime.Now;
            }
            return await Task.FromResult(existingComment);
        }
        public async Task<bool> DeleteCommentAsync(int id)
        {
            var existingComment = _comments.FirstOrDefault(c => c.Id == id);
            if (existingComment != null)
            {
                _comments.Remove(existingComment);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}
