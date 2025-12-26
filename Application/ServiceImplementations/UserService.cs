
using Application.DTOs.User;
using Application.Mappers;
using Application.SecurityInterfaces;
using Application.ServiceInterfaces;
using Domain.Common;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Domain.UnitOfWorksInterfaces;

namespace Application.ServiceImplementations;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHasher _hasher;

    public UserService(IUnitOfWork unitOfWork, IHasher hasher)
    {
        _unitOfWork = unitOfWork;
        _hasher = hasher;
    }

    public async Task<Result<UserDto>> GetByIdAsync(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null)
            return Result<UserDto>.Failure("Not found user with this Id.");

        return Result<UserDto>.Success(user.ToDto());
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync(int pageNumber, int pageSize)
    {
        var users = await _unitOfWork.Users.GetAllAsync(pageNumber, pageSize);
        return users.Select(UserMapper.ToDto);
    }

    // Create new user
    public async Task<Result<int>> CreateAsync(CreateUserDto dto)
    {
        var hashedPassword = _hasher.Hash(dto.Password);

        var userResult = User.TryCreate(
            dto.NationalId,
            dto.FirstName,
            dto.LastName,
            dto.PhoneNumber,
            dto.Username,
            dto.Role,
            hashedPassword
        );

        if (!userResult.IsSuccess)
            return Result<int>.Failure(userResult.Error);

        var user = userResult.Value;
        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.CommitAsync();

        return Result<int>.Success(user.Id);
    }

    public async Task<Result<bool>> Update(UpdateUserDto dto)
    {
        var user = await _unitOfWork.Users.GetByNationalIdAsync(dto.NationalId);
        if (user == null)
            return Result<bool>.Failure("User not found.");

        if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
            user.PhoneNumber = dto.PhoneNumber;

        if (!string.IsNullOrWhiteSpace(dto.Role))
            user.Role = dto.Role;

        await _unitOfWork.CommitAsync();
        return Result<bool>.Success(true);
    }

    // Change password
    public async Task<Result<bool>> ChangePassword(string nationalId, string oldPassword, string newPassword)
    {
        var user = await _unitOfWork.Users.GetByNationalIdAsync(nationalId);

        if (user == null)
            return Result<bool>.Failure("User not found.");

        if (!_hasher.Verification(oldPassword, user.HashedPassword))
            return Result<bool>.Failure("Old password is incorrect.");

        user.HashedPassword = _hasher.Hash(newPassword);
        await _unitOfWork.CommitAsync();

        return Result<bool>.Success(true);
    }

    // Delete user
    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null)
            return Result<bool>.Failure("User not found.");

        await _unitOfWork.Users.DeleteAsync(id);
        await _unitOfWork.CommitAsync();

        return Result<bool>.Success(true);
    }

}
