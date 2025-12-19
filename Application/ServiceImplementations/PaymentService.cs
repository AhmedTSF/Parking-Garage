
using Application.DTOs.Payment;
using Application.Mappers;
using Application.ServiceInterfaces;
using Domain.Common;
using Domain.Entities;
using Domain.UnitOfWorksInterfaces;

namespace Application.ServiceImplementations;

public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork;

    public PaymentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PaymentDto>> CreatePaymentAsync(CreatePaymentDto dto)
    {
        var session = await _unitOfWork.Sessions.GetByIdAsync(dto.SessionId);
        if (session is null)
            return Result<PaymentDto>.Failure("Session not found.");

        var existingPayment = await _unitOfWork.Payments.GetBySessionIdAsync(dto.SessionId);
        if (existingPayment is not null)
            return Result<PaymentDto>.Failure("Payment already exists for this session.");

        var paymentResult = Payment.TryCreate(session);
        if (!paymentResult.IsSuccess)
            return Result<PaymentDto>.Failure(paymentResult.Error);

        var payment = paymentResult.Value;

        await _unitOfWork.Payments.AddAsync(payment);
        await _unitOfWork.CommitAsync(); // commit here as outermost service

        return Result<PaymentDto>.Success(PaymentMapper.ToDto(payment));
    }

    public async Task<Result<bool>> PayAsync(PayPaymentDto dto)
    {
        var payment = await _unitOfWork.Payments.GetBySessionIdAsync(dto.SessionId);
        if (payment is null)
            return Result<bool>.Failure("Payment not found.");


        var payResult = payment.TryPay(payment.Session.FinalCost.Value, dto.Method);
        if (!payResult.IsSuccess)
            return Result<bool>.Failure(payResult.Error);

        _unitOfWork.Payments.Update(payment);
        await _unitOfWork.CommitAsync();

        return Result<bool>.Success(true);
    }

    public async Task<Result<PaymentDto>> GetBySessionIdAsync(int sessionId)
    {
        var payment = await _unitOfWork.Payments.GetBySessionIdAsync(sessionId);
        if (payment is null)
            return Result<PaymentDto>.Failure("Payment not found.");

        return Result<PaymentDto>.Success(PaymentMapper.ToDto(payment));
    }

    public async Task<List<PaymentDetailedDto>> GetAllDetailedAsync(int pageNumber, int pageSize)
    {
        var payments = await _unitOfWork.Payments.GetAllDetailedAsync(pageNumber, pageSize);

        return payments.Select(PaymentMapper.ToDetailedDto).ToList();

    }

    public async Task<List<PaymentDto>> GetByCarPlateNumberAsync(string plateNumber)
    {
        var payments = await _unitOfWork.Payments.GetByCarPlateNumberAsync(plateNumber);

        return payments.Select(PaymentMapper.ToDto).ToList();
    }

}

