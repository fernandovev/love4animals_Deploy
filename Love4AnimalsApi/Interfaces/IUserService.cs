using Love4AnimalsApi.Dtos;

namespace Love4AnimalsApi.Interfaces;

public interface IUserService
{
    Task<List<GetUserDto>> GetUsersAsync();
    Task<GetUserDto?> GetUserByIdAsync(int id);
    Task<GetUserDto> CreateUserAsync(CreateUserDto dto);
    Task<GetUserDto?> UpdateUserAsync(int id, UpdateUserDto dto);
    Task<GetUserDto?> DeleteUserAsync(int id);
}