using RAWI7AndFutureLabs.Models;

namespace RAWI7AndFutureLabs.Services.Comment
{
    public interface ICommentsService
    {
        Task<List<Comments>> GetCommentsAsync();
        Task<Comments> GetCommentByIdAsync(int id);
        Task<Comments> CreateCommentAsync(Comments comment);
        Task<Comments> UpdateCommentAsync(int id, Comments comment);
        Task<bool> DeleteCommentAsync(int id);
    }
}
