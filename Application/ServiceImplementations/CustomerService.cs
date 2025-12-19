
using Application.DTOs.Car;
using Application.DTOs.Customer;
using Application.Mappers;
using Application.ServiceInterfaces;
using Domain.Common;
using Domain.Entities;
using Domain.UnitOfWorksInterfaces;
using Microsoft.VisualBasic;

namespace Application.ServiceImplementations;

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
            Cars = customer.Cars.Select(CarMapper.ToDto).ToList()
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
            Cars = c.Cars.Select(CarMapper.ToDto).ToList()
        });
    }

    public async Task<Result<int>> CreateAsync(CreateCustomerDto dto)
    {
        var customerResult = Customer.TryCreate(dto.NationalId, dto.FirstName, dto.LastName);

        if(!customerResult.IsSuccess)
            return Result<int>.Failure(customerResult.Error);

        var customer = customerResult.Value;

        await _unitOfWork.Customers.AddAsync(customer);
        await _unitOfWork.CommitAsync();

        return Result<int>.Success(customer.Id);
    }

    public async Task<Result<CustomerDto>> GetByNationalIdAsync(string nationalId)
    {
        var customer = await _unitOfWork.Customers.GetByNationalIdAsync(nationalId);

        if(customer == null)
            return Result<CustomerDto>.Failure("Customer not found");

        return Result<CustomerDto>.Success(CustomerMapper.ToDto(customer)); 
    }

    public async Task<Result<CustomerDto>> GetDetailedByNationalIdAsync(string nationalId)
    {
        var customer = await _unitOfWork.Customers.GetDetailedByNationalIdAsync(nationalId);

        if(customer == null)
            return Result<CustomerDto>.Failure("Customer not found");

        return Result<CustomerDto>.Success(CustomerMapper.ToDto(customer));
    }
}
