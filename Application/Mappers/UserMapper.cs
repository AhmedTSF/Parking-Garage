

using Application.DTOs.User;
using Domain.Entities;

namespace Application.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(this User entity)
        {
            return new UserDto
            {
                Id = entity.Id,
                NationalId = entity.NationalId,
                FullName = entity.FullName(),
                PhoneNumber = entity.PhoneNumber,
                Username = entity.Username,
                Role = entity.Role
            };
        }
    }
}
