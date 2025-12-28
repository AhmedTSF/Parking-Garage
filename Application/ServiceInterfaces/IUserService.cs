
using Application.DTOs.Car;
using Application.DTOs.User;
using Domain.Common;

namespace Application.ServiceInterfaces;

public interface IUserService 
{
    Task<Result<string>> LoginAsync(string username, string password);
    Task<Result<UserDto>> GetByIdAsync(int id);
    Task<IEnumerable<UserDto>> GetAllAsync(int pageNumber, int pageSize);
    Task<Result<int>> CreateAsync(CreateUserDto dto);
    Task<Result<bool>> Update(UpdateUserDto dto); 
    Task<Result> ChangePassword(string username, string oldPassword, string newPassword);
    Task<Result<bool>> DeleteAsync(int id);
}
