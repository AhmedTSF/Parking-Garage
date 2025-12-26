
using Application.DTOs.Car;
using Application.DTOs.User;
using Domain.Common;

namespace Application.ServiceInterfaces;

public interface IUserService 
{
    Task<Result<UserDto>> GetByIdAsync(int id);
    Task<IEnumerable<UserDto>> GetAllAsync(int pageNumber, int pageSize);
    Task<Result<int>> CreateAsync(CreateUserDto dto);
    Task<Result<bool>> Update(UpdateUserDto dto); 
    Task<Result<bool>> ChangePassword(string nationalId, string oldPassword, string newPassword);
    Task<Result<bool>> DeleteAsync(int id);
}
