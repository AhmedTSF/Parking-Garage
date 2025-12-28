using Domain.Common;
using Application.DTOs.Session;
using Domain.Constants;
using Domain.Entities;
using Domain.UnitOfWorksInterfaces;
using Domain.Extensions;
using Application.Mappers;
using Application.ServiceInterfaces;

namespace Application.ServiceImplementations;

public class SessionService : ISessionService
{
    private readonly IUnitOfWork _unitOfWork;

    public SessionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private Result ValidateSessionDto(CreateSessionDto dto)
    {
        if (dto == null)
            return Result.Failure("Session data cannot be null.");

        if (string.IsNullOrEmpty(dto.PlateNumber) && dto.CreateCar == null)
            return Result.Failure("Either PlateNumber or CreateCar must be provided.");

        if (string.IsNullOrEmpty(dto.SpotNumber))
            return Result.Failure("SpotNumber must be provided.");

        return Result.Success();
    }

    private Result ValidateSpot(Spot spot, string spotNumber)
    {
        if (spot == null)
            return Result.Failure($"Spot '{spotNumber}' does not exist.");

        if (spot.IsOccupied)
            return Result.Failure($"Spot '{spotNumber}' is currently occupied.");

        return Result.Success();
    }

    public async Task<Result<int>> StartSessionAsync(CreateSessionDto createSessionDto)
    {
        // Validate input
        var dtoValidation = ValidateSessionDto(createSessionDto);

        if (!dtoValidation.IsSuccess)
            return Result<int>.Failure(dtoValidation.Error);

        // Fetch required data
        var spot = await _unitOfWork.Spots.GetBySpotNubmerAsync(createSessionDto.SpotNumber);

        var spotValidation = ValidateSpot(spot, createSessionDto.SpotNumber);

        if (!spotValidation.IsSuccess)
            return Result<int>.Failure(spotValidation.Error);

        var costPerHour = await _unitOfWork.Sittings.GetValueAsync(SittingKeys.CostPerHour);

        try
        {
            Car car;

            // Get existing car or create new one
            if (!string.IsNullOrEmpty(createSessionDto.PlateNumber))
            {
                if (!StringExtension.IsValidPlateNumber(createSessionDto.PlateNumber))
                {
                    return Result<int>.Failure("PlateNumber format is invalid.");
                }


                car = await _unitOfWork.Cars.GetByPlateNumberAsync(createSessionDto.PlateNumber);
                if (car == null)
                    return Result<int>.Failure($"Car with plate number '{createSessionDto.PlateNumber}' does not exist.");
            }
            else
            {
                // Create new car (without committing)
                if (createSessionDto.CreateCar.CustomerId is null && createSessionDto.CreateCar.Customer is null)
                    return Result<int>.Failure("Either CustomerId or Customer information must be provided to create a new car.");

                var customer = await _unitOfWork.Customers.GetByIdAsync(createSessionDto.CreateCar!.CustomerId.Value);

                // Apply transactional behavior

                if (customer is null && createSessionDto.CreateCar.Customer is null)
                    return Result<int>.Failure("Customer information must be provided to create a new car.");


                if (customer is null)
                {
                    var customerResult = Customer.TryCreate(
                        createSessionDto.CreateCar.Customer!.NationalId,
                        createSessionDto.CreateCar.Customer.FirstName,
                        createSessionDto.CreateCar.Customer.LastName, 
                        createSessionDto.CreateCar.Customer!.PhoneNumber);

                    if (!customerResult.IsSuccess)
                        return Result<int>.Failure(customerResult.Error);

                    customer = customerResult.Value;

                    await _unitOfWork.Customers.AddAsync(customer);
                }

                var carResult = Car.TryCreate(
                    createSessionDto.CreateCar!.PlateNumber,
                    customer);

                if (!carResult.IsSuccess)
                    return Result<int>.Failure(carResult.Error);

                car = carResult.Value;

                await _unitOfWork.Cars.AddAsync(car);
            }

            // Create session
            var sessionResult = Session.TryCreate(
                DateTime.Now,
                spot.Id,
                car,
                Convert.ToDecimal(costPerHour));

            if (!sessionResult.IsSuccess)
                return Result<int>.Failure(sessionResult.Error);

            var session = sessionResult.Value;

            var occupyResult = spot.Occupy();

            if (!occupyResult.IsSuccess)
                return Result<int>.Failure(occupyResult.Error);


            var paymentResult = Payment.TryCreate(session);

            if (!paymentResult.IsSuccess)
                return Result<int>.Failure(paymentResult.Error);

            var payment = paymentResult.Value;

            await _unitOfWork.Sessions.AddAsync(session);
            _unitOfWork.Spots.Update(spot);
            await _unitOfWork.Payments.AddAsync(payment);
            await _unitOfWork.CommitAsync();  

            return Result<int>.Success(session.Id);
        }
        catch (Exception ex)
        {
            return Result<int>.Failure($"Failed to create session: {ex.Message}");
        }

    }

    public async Task<Result<SessionDetailedDto>> EndSessionAsync(int sessionId)
    {
        var activeSession = await _unitOfWork.Sessions.GetAcitveDetailedByIdAsync(sessionId);

        if (activeSession is null)
            return Result<SessionDetailedDto>.Failure($"Session with ID {sessionId} not found.");

        var endResult = activeSession.EndSession();

        if (!endResult.IsSuccess)
            return Result<SessionDetailedDto>.Failure(endResult.Error);

        var session = activeSession;

        var spot = activeSession.Spot;

        var vacateResult = spot.Vacate();

        if (!vacateResult.IsSuccess)
            return Result<SessionDetailedDto>.Failure(vacateResult.Error);

        _unitOfWork.Spots.Update(spot);
        _unitOfWork.Sessions.Update(session);
        await _unitOfWork.CommitAsync();

        return Result<SessionDetailedDto>.Success(SessionMapper.ToDetailedDto(session));
    }

    public async Task<Result<SessionDetailedDto>> GetByIdAsync(int sessionId)
    {
        var session = await _unitOfWork.Sessions.GetDetailedByIdAsync(sessionId);

        if (session is null)
            return Result<SessionDetailedDto>.Failure($"Session with ID {sessionId} not found.");

        return Result<SessionDetailedDto>.Success(SessionMapper.ToDetailedDto(session));
    }

    public async Task<List<SessionDetailedDto>> GetAllAsync(int pageNumber, int pageSize)
    {
        var list = await _unitOfWork.Sessions.GetAllDetailedAsync(pageNumber, pageSize);

        return list.Select(SessionMapper.ToDetailedDto).ToList();

    }

    public async Task<List<SessionDetailedDto>> GetAllActiveAsync(int pageNumber, int pageSize)
    {
        var list = await _unitOfWork.Sessions.GetAllActiveDetailedAsync(pageNumber, pageSize);
        return list.Select(SessionMapper.ToDetailedDto).ToList();
    }
}


    //public async Task<Result<int>> StartSessionAsync(CreateSessionDto createSessionDto)
    //{
    //    // Validate input
    //    var dtoValidation = ValidateSessionDto(createSessionDto);

    //    if (!dtoValidation.IsSuccess)
    //        return Result<int>.Failure(dtoValidation.Error);

    //    // Fetch required data
    //    var spot = await _unitOfWork.Spots.GetBySpotNubmerAsync(createSessionDto.SpotNumber);

    //    var spotValidation = ValidateSpot(spot, createSessionDto.SpotNumber);

    //    if (!spotValidation.IsSuccess)
    //        return Result<int>.Failure(spotValidation.Error);

    //    var costPerHour = await _unitOfWork.Sittings.GetValueAsync(SittingKeys.CostPerHour);

    //    try
    //    {
    //        Car? car;

    //        // Get existing car or create new one
    //        if (string.IsNullOrEmpty(createSessionDto.PlateNumber))
    //        {
    //            return Result<int>.Failure("PlateNumber must be provided.");
    //        }

    //        if(!StringExtension.IsValidPlateNumber(createSessionDto.PlateNumber))
    //        {
    //            return Result<int>.Failure("PlateNumber format is invalid.");
    //        }

    //        // Check if car exists
    //        car = await _unitOfWork.Cars.GetByPlateNumberAsync(createSessionDto.PlateNumber);
    //        Customer? customer = null;

    //        if (car is null)
    //        {
    //            if (string.IsNullOrEmpty(createSessionDto.Customer.NationalId))
    //            {
    //                return Result<int>.Failure("NationalId must be provided to create a new car.");
    //            }
    //            else if (StringExtension.IsValidNationalId(createSessionDto.Customer.NationalId))
    //            {
    //                return Result<int>.Failure("NationalId is invalid.");
    //            }

    //            // Check if customer exists
    //            customer = await _unitOfWork.Customers.GetByNationalIdAsync(createSessionDto.Customer.NationalId);

    //            if (customer is null)
    //            {
    //                var customerResult = Customer.TryCreate(
    //                    createSessionDto.Customer.NationalId,
    //                    createSessionDto.Customer.FirstName,
    //                    createSessionDto.Customer.LastName);

    //                if (!customerResult.IsSuccess)
    //                    return Result<int>.Failure(customerResult.Error);

    //                customer = customerResult.Value;

    //                await _unitOfWork.Customers.AddAsync(customer);
    //            }

    //            var carResult = Car.TryCreate(
    //                createSessionDto.PlateNumber,
    //                customer);

    //            if (!carResult.IsSuccess)
    //                return Result<int>.Failure(carResult.Error);

    //            car = carResult.Value;

    //            await _unitOfWork.Cars.AddAsync(car);
    //        }

    //        var sessionResult = Session.TryCreate(
    //                DateTime.Now,
    //                spot.Id,
    //                car,
    //                Convert.ToDecimal(costPerHour));


    //        if (!sessionResult.IsSuccess)
    //            return Result<int>.Failure(sessionResult.Error);

    //        var session = sessionResult.Value;

    //        var occupyResult = spot.Occupy();

    //        if (!occupyResult.IsSuccess)
    //            return Result<int>.Failure(occupyResult.Error);

    //        var paymentResult = Payment.TryCreate(session);

    //        if (!paymentResult.IsSuccess)
    //            return Result<int>.Failure(paymentResult.Error);

    //        var payment = paymentResult.Value;

    //        await _unitOfWork.Sessions.AddAsync(session);
    //        _unitOfWork.Spots.Update(spot);
    //        await _unitOfWork.Payments.AddAsync(payment);
    //        await _unitOfWork.CommitAsync();

    //        return Result<int>.Success(session.Id);
    //    }
    //    catch (Exception ex)
    //    {
    //        return Result<int>.Failure($"Failed to create session: {ex.Message}");
    //    }

    //}


