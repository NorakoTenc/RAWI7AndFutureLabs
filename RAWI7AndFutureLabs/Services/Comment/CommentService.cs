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
            _comments = Enumerable.Range(1, 10).Select(i => new Comments
            {
                Id = i,
                Content = $"Comment {i} content",
                CreatedAt = DateTime.Now.AddDays(-i),
                UserId = i % 3 + 1,
                PostId = i % 5 + 1
            }).ToList();
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
