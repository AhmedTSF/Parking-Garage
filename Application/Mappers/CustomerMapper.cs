using Application.DTOs.Car;
using Application.DTOs.Customer;
using Domain.Common;
using Domain.Entities;

namespace Application.Mappers;

public static class CustomerMapper
{
    public static Customer ToEntity(this CreateCustomerDto dto)
    {
        return new Customer
        { 
            NationalId = dto.NationalId,
            FirstName = dto.FirstName,
            LastName = dto.LastName

        };
    }

    public static CustomerDto ToDto(this Customer entity)
    {
        return new CustomerDto
        {
            Id = entity.Id,
            NationalId = entity.NationalId,
            FullName = $"{entity.FirstName} {entity.LastName}",
            Cars = entity.Cars.Select(c => c.ToDto()).ToList()
        };

    }

}
