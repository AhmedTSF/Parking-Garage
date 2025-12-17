
using Application.DTOs.Payment;
using Domain.Entities;

namespace Application.Mappers
{
    public static class PaymentMapper
    {
        public static PaymentDto ToDto(this Payment payment)
        {
            return new PaymentDto
            {
                Id = payment.Id,
                Amount = payment.Amount,
                PaidAt = payment.PaidAt,
                PaymentMethod = payment.PaymentMethod,
                SessionId = payment.SessionId
            };
        }


        public static PaymentDetailedDto ToDetailedDto(this Payment payment)
        {
            return new PaymentDetailedDto
            {
                Id = payment.Id,
                Amount = payment.Amount,
                PaidAt = payment.PaidAt,
                PaymentMethod = payment.PaymentMethod,
                SessionId = payment.SessionId,
                EntryTimestamp = payment.Session.DateTimeSlot.EntryTimestamp,
                ExitTimestamp = payment.Session.DateTimeSlot.ExitTimestamp,
                CostPerHour = payment.Session.CostPerHour,
                FinalCost = payment.Session.FinalCost,
                CarPlateNumber = payment.Session.Car?.PlateNumber ?? string.Empty,
                CustomerName = payment.Session.Car?.Customer != null ? payment.Session.Car.Customer.FullName() : string.Empty,
                SpotNumber = payment.Session.Spot?.SpotNumber ?? string.Empty
            };
        }
    }
}
