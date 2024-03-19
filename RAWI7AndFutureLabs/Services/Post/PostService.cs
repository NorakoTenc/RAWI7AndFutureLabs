using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RAWI7AndFutureLabs.Models;
using RAWI7AndFutureLabs.Services.Post;

namespace RAWI7AndFutureLabs.Services
{
    public class PostsService : IPostsService
    {
        private readonly List<Posts> _posts;

        public PostsService()
        {
            _posts = Enumerable.Range(1, 10).Select(i => new Posts
            {
                Id = i,
                Title = $"Post {i} title",
                Content = $"Post {i} content",
                CreatedAt = DateTime.Now.AddDays(-i),
                UserId = i % 3 + 1
            }).ToList();
        }
        public async Task<List<Posts>> GetPostsAsync()
        {
            return await Task.FromResult(_posts);
        }
        public async Task<Posts> GetPostByIdAsync(int id)
        {
            return await Task.FromResult(_posts.FirstOrDefault(p => p.Id == id));
        }
        public async Task<Posts> CreatePostAsync(Posts post)
        {
            post.Id = _posts.Count + 1;
            post.CreatedAt = DateTime.Now;
            _posts.Add(post);
            return await Task.FromResult(post);
        }
        public async Task<Posts> UpdatePostAsync(int id, Posts post)
        {
            var existingPost = _posts.FirstOrDefault(p => p.Id == id);
            if (existingPost != null)
            {
                existingPost.Title = post.Title;
                existingPost.Content = post.Content;
                existingPost.CreatedAt = DateTime.Now;
            }
            return await Task.FromResult(existingPost);
        }
        public async Task<bool> DeletePostAsync(int id)
        {
            var existingPost = _posts.FirstOrDefault(p => p.Id == id);
            if (existingPost != null)
            {
                _posts.Remove(existingPost);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}