using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetCommentsByPostIdAsync(int postId);
    Task<Comment?> GetCommentByIdAsync(int postId, int id);
    Task<Comment> CreateCommentAsync(Comment comment);
    Task UpdateCommentAsync(Comment comment);
    Task DeleteCommentAsync(Comment comment);
}