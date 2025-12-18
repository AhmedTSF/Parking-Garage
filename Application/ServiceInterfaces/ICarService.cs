
using Application.DTOs.Car;
using Domain.Common;
using Domain.Entities;

namespace Application.ServiceInterfaces;

public interface ICarService
{
    Task<CarDto> GetByIdAsync(int id);
    Task<IEnumerable<CarDto>> GetAllAsync(int pageNumber, int pageSize);
    Task<IEnumerable<CarDetailedDto>> GetAllDetailedAsync(int pageNumber, int pageSize);
    Task<Result<int>> CreateAsync(CreateCarDto dto);
}
