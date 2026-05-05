using Love4AnimalsApi.Dtos;

namespace Love4AnimalsApi.Interfaces;

public interface IPostService
{
    Task<List<GetPostDto>> GetPostsAsync();
    Task<GetPostDto?> GetPostByIdAsync(int id);
    Task<GetPostDto> CreatePostAsync(CreatePostDto dto);
    Task<GetPostDto?> UpdatePostAsync(int id, UpdatePostDto dto);
    Task<GetPostDto?> DeletePostAsync(int id);
}