
using Application.DTOs.Car;
using Application.DTOs.Customer;
using Application.ServiceInterfaces;
using Domain.Entities;
using Domain.UnitOfWorksInterfaces;

namespace Application.ServiceImplementations
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);

            if (customer == null)
                throw new KeyNotFoundException($"Customer with ID {id} not found.");

            return new CustomerDto
            {
                Id = customer.Id,
                NationalId = customer.NationalId,
                FullName = $"{customer.FirstName} {customer.LastName}",
                Cars = customer.Cars.Select(c => new CarDto
                {
                    Id = c.Id,
                    PlateNumber = c.PlateNumber
                }).ToList()
            };
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            var customers = await _unitOfWork.Customers
                .GetAllAsync(pageNumber, pageSize);

            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                NationalId = c.NationalId,
                FullName = $"{c.FirstName} {c.LastName}",
                Cars = c.Cars.Select(car => new CarDto
                {
                    Id = car.Id,
                    PlateNumber = car.PlateNumber
                }).ToList()
            });
        }

        public async Task<int> CreateAsync(CreateCustomerDto dto)
        {
            if (dto.Car == null)
                throw new InvalidOperationException(
                    "Customer must have exactly one car at creation.");

            var customer = new Customer
            {
                NationalId = dto.NationalId,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            var car = new Car
            {
                PlateNumber = dto.Car.PlateNumber
            };

            customer.Cars.Add(car);

            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.CommitAsync();

            return customer.Id;
        }


    }

}
