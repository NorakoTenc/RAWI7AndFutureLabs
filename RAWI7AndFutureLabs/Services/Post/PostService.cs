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
            _posts = new List<Posts>
            {
                new Posts {Id=1, Title="Post 1 title", Content="Post 1 content", CreatedAt=DateTime.Now.AddDays(-1), UserId=1},
                new Posts {Id=2, Title="Post 2 title", Content="Post 2 content", CreatedAt=DateTime.Now.AddDays(-2), UserId=2},
                new Posts {Id=3, Title="Post 3 title", Content="Post 3 content", CreatedAt=DateTime.Now.AddDays(-3), UserId=3},
                new Posts {Id=4, Title="Post 4 title", Content="Post 4 content", CreatedAt=DateTime.Now.AddDays(-4), UserId=4},
                new Posts {Id=5, Title="Post 5 title", Content="Post 5 content", CreatedAt=DateTime.Now.AddDays(-5), UserId=5},
                new Posts {Id=6, Title="Post 6 title", Content="Post 6 content", CreatedAt=DateTime.Now.AddDays(-6), UserId=6},
                new Posts {Id=7, Title="Post 7 title", Content="Post 7 content", CreatedAt=DateTime.Now.AddDays(-7), UserId=7},
                new Posts {Id=8, Title="Post 8 title", Content="Post 8 content", CreatedAt=DateTime.Now.AddDays(-8), UserId=8},
                new Posts {Id=9, Title="Post 9 title", Content="Post 9 content", CreatedAt=DateTime.Now.AddDays(-9), UserId=9},
                new Posts {Id=10, Title="Post 10 title", Content="Post 10 content", CreatedAt=DateTime.Now.AddDays(-10), UserId=10},
            };
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