using Application.DTOs.Car;
using Domain.Entities;


namespace Application.Mappers
{
    public static class CarMapper
    {
        public static Car ToEntity(this CreateCarDto dto)
        {
            return new Car
            {
                PlateNumber = dto.PlateNumber,
                CustomerId = dto.CustomerId
            };
        }

        public static CarDto ToDto(this Car entity)
        {
            return new CarDto
            {
                Id = entity.Id,
                PlateNumber = entity.PlateNumber
            };
        }

        public static CarDetailedDto ToDetailedDto(this Car entity)
        {
            return new CarDetailedDto
            {
                Id = entity.Id,
                PlateNumber = entity.PlateNumber,
                CustomerId = entity.CustomerId,
                CustomerFullName = $"{entity.Customer.FirstName} {entity.Customer.LastName}"
            };
        }
    }
}
