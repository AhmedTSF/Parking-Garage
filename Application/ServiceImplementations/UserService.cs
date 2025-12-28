
using Application.DTOs.User;
using Application.Mappers;
using Application.Security;
using Application.Security.Jwt;
using Application.ServiceInterfaces;
using Domain.Common;
using Domain.Entities;
using Domain.UnitOfWorksInterfaces;
using Microsoft.Extensions.Logging;

namespace Application.ServiceImplementations;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHasher _hasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ILogger<UserService> _logger;

    public UserService(
        IUnitOfWork unitOfWork, 
        IHasher hasher, 
        IJwtTokenGenerator jwtTokenGenerator,
        ILogger<UserService> logger)
    {
        _unitOfWork = unitOfWork;
        _hasher = hasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _logger = logger;
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

        _logger.LogInformation("New user created with id {UserId} and username {Username}", user.Id, user.Username); 

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
    public async Task<Result> ChangePassword(string username, string oldPassword, string newPassword)
    {
        var user = await _unitOfWork.Users.GetByUsernameAsync(username);

        if (user == null)
            return Result.Failure("User not found.");

        if (!_hasher.Verification(oldPassword, user.HashedPassword))
            return Result.Failure("Old password is incorrect.");

        user.HashedPassword = _hasher.Hash(newPassword);
        await _unitOfWork.CommitAsync();

        return Result.Success();
    }

    // Delete user
    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null)
            return Result<bool>.Failure("User not found.");

        await _unitOfWork.Users.DeleteAsync(id);
        await _unitOfWork.CommitAsync();

        _logger.LogInformation("User with id {Id} and username {Username} has been deleted", user.Id, user.Username);

        return Result<bool>.Success(true);
    }


    public async Task<Result<string>> LoginAsync(string username, string password)
    {
        var user = await _unitOfWork.Users.GetByUsernameAsync(username);

        if (user == null)
            return Result<string>.Failure("Invalid credentials.");

        if (!_hasher.Verification(password, user.HashedPassword))
            return Result<string>.Failure("Invalid credentials.");

        var token = _jwtTokenGenerator.GenerateToken(user);

        return Result<string>.Success(token);
    }

}
