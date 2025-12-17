using Application.DTOs.Payment;
using Domain.Common;


namespace Application.ServiceInterfaces;

public interface IPaymentService
{
    Task<Result<PaymentDto>> CreatePaymentAsync(CreatePaymentDto dto);
    Task<Result<bool>> PayAsync(PayPaymentDto dto);
    Task<Result<PaymentDto>> GetBySessionIdAsync(int sessionId);
    Task<List<PaymentDetailedDto>> GetAllDetailedAsync(int pageNumber, int pageSize);
}
