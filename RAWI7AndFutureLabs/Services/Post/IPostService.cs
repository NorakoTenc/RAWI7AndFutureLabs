using RAWI7AndFutureLabs.Models;
namespace RAWI7AndFutureLabs.Services.Post
{
    public interface IPostsService
    {
        Task<List<Posts>> GetPostsAsync();
        Task<Posts> GetPostByIdAsync(int id);
        Task<Posts> CreatePostAsync(Posts post);
        Task<Posts> UpdatePostAsync(int id, Posts post);
        Task<bool> DeletePostAsync(int id);
    }
}
