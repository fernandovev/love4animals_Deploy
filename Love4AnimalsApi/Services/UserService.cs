using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Services;

public class UserService : IUserService
{
    private IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public List<GetUserDto> GetUsers()
    {
        return userRepository.GetUsers()
            .Select(user => new GetUserDto(
                user.Id,
                user.Nombre,
                user.Email,
                user.Password,
                user.Rol
            ))
            .ToList();
    }

    public GetUserDto? GetUserById(int id)
    {
        var user = userRepository.GetUserById(id);

        if (user == null)
            return null;

        return new GetUserDto(
            user.Id,
            user.Nombre,
            user.Email,
            user.Password,
            user.Rol
        );
    }

    public GetUserDto CreateUser(CreateUserDto dto)
    {
        var users = userRepository.GetUsers();
        int newId = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;

        User newUser = new User(
            newId,
            dto.Nombre,
            dto.Email,
            dto.Password,
            dto.Rol
        );

        var createdUser = userRepository.CreateUser(newUser);

        return new GetUserDto(
            createdUser.Id,
            createdUser.Nombre,
            createdUser.Email,
            createdUser.Password,
            createdUser.Rol
        );
    }

    public GetUserDto? UpdateUser(int id, UpdateUserDto dto)
    {
        var user = userRepository.GetUserById(id);

        if (user == null)
            return null;

        user.Nombre = dto.Nombre;
        user.Email = dto.Email;
        user.Password = dto.Password;
        user.Rol = dto.Rol;

        return new GetUserDto(
            user.Id,
            user.Nombre,
            user.Email,
            user.Password,
            user.Rol
        );
    }

    public GetUserDto? DeleteUser(int id)
    {
        var user = userRepository.GetUserById(id);

        if (user == null)
            return null;

        var deletedUser = new GetUserDto(
            user.Id,
            user.Nombre,
            user.Email,
            user.Password,
            user.Rol
        );

        userRepository.DeleteUser(id);

        return deletedUser;
    }
}