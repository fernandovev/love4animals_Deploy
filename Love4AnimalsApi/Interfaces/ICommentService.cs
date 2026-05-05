using Love4AnimalsApi.Dtos;

namespace Love4AnimalsApi.Interfaces;

public interface ICommentService
{
    Task<List<GetCommentDto>> GetCommentsByPostIdAsync(int postId);
    Task<GetCommentDto?> GetCommentByIdAsync(int postId, int id);
    Task<GetCommentDto?> CreateCommentAsync(int postId, CreateCommentDto dto);
    Task<GetCommentDto?> UpdateCommentAsync(int postId, int id, UpdateCommentDto dto);
    Task<GetCommentDto?> DeleteCommentAsync(int postId, int id);
}