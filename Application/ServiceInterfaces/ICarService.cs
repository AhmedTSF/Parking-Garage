
using Application.DTOs.Car;
using Domain.Entities;

namespace Application.ServiceInterfaces;

public interface ICarService
{
    Task<CarDto> GetByIdAsync(int id);
    Task<IEnumerable<CarDto>> GetAllAsync(int pageNumber, int pageSize);
    Task<Car> CreateAsync(CreateCarDto dto);
}
