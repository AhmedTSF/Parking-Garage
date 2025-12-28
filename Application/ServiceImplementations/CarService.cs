

using Application.DTOs.Car;
using Application.Mappers;
using Application.ServiceInterfaces;
using Domain.Common;
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


 
        public async Task<Result<int>> CreateAsync(CreateCarDto dto)
        {
            var existingCar = await _unitOfWork.Cars.GetByPlateNumberAsync(dto.PlateNumber);
            if (existingCar != null)
                return Result<int>.Failure(
                    $"A car with plate number '{dto.PlateNumber}' already exists.");

            var customer = await _unitOfWork.Customers.GetByIdAsync(dto.CustomerId.Value);
            if (customer is null)
            {
                if(dto.Customer is null)
                {
                    return Result<int>.Failure(
                        $"Customer with ID '{dto.CustomerId}' does not exist.");
                }

                customer = Customer.TryCreate(
                    dto.Customer.NationalId,
                    dto.Customer.FirstName,
                    dto.Customer.LastName,
                    dto.Customer.PhoneNumber).Value;


                await _unitOfWork.Customers.AddAsync(customer);

            }

            var car = Car.TryCreate(
                dto.PlateNumber,
                customer).Value;

            await _unitOfWork.Cars.AddAsync(car);
            await _unitOfWork.CommitAsync();

            return Result<int>.Success(car.Id);

        }

    }
}
