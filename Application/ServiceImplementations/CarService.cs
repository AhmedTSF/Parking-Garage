

using Application.DTOs.Car;
using Application.Mappers;
using Application.ServiceInterfaces;
using Domain.Entities;
using Domain.UnitOfWorksInterfaces;

namespace Application.ServiceImplementations
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CarDto> GetByIdAsync(int id)
        {
            var car = await _unitOfWork.Cars.GetByIdAsync(id);

            if (car == null)
                throw new KeyNotFoundException($"Car with ID {id} not found.");

            return CarMapper.ToDto(car);
        }

        public async Task<IEnumerable<CarDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            var cars = await _unitOfWork.Cars
                .GetAllAsync(pageNumber, pageSize);

            return cars.Select(c => new CarDto
            {
                Id = c.Id,
                PlateNumber = c.PlateNumber
            });
        }

        public async Task<IEnumerable<CarDetailedDto>> GetAllDetailedAsync(int pageNumber, int pageSize)
        {
            var cars = await _unitOfWork.Cars.GetAllDetailedAsync(pageNumber, pageSize);

            return cars.Select(CarMapper.ToDetailedDto);
        }


 
        public async Task<Car> CreateAsync(CreateCarDto dto)
        {
            var existingCar = await _unitOfWork.Cars.GetByPlateNumberAsync(dto.PlateNumber);
            if (existingCar != null)
                throw new InvalidOperationException(
                    $"Car with Plate Number {dto.PlateNumber} already exists.");

            var customer = await _unitOfWork.Customers.GetByIdAsync(dto.CustomerId);
            if (customer == null)
                throw new InvalidOperationException(
                    $"Customer with ID {dto.CustomerId} does not exist.");

            var car = CarMapper.ToEntity(dto);
            await _unitOfWork.Cars.AddAsync(car);

            // No commit - caller is responsible for transaction management
            return car; // ✅ Return entity, caller can access Id after their commit
        }

    }
}
