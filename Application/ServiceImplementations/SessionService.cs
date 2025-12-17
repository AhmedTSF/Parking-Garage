using Domain.Common;
using Application.DTOs.Session;
using Domain.Constants;
using Domain.Entities;
using Domain.UnitOfWorksInterfaces;
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
                car = await _unitOfWork.Cars.GetByPlateNumberAsync(createSessionDto.PlateNumber);
                if (car == null)
                    return Result<int>.Failure($"Car with plate number '{createSessionDto.PlateNumber}' does not exist.");
            }
            else
            {
                // Create new car (without committing)
                var customer = await _unitOfWork.Customers.GetByIdAsync(createSessionDto.CreateCar!.CustomerId);

                // Apply transactional behavior

                if ( customer is null && createSessionDto.CreateCar.CreateCustomer is null)
                    return Result<int>.Failure("Customer information must be provided to create a new car.");


                if (customer is null)
                {
                    var customerResult = Customer.TryCreate(
                        createSessionDto.CreateCar.CreateCustomer!.NationalId,
                        createSessionDto.CreateCar.CreateCustomer.FirstName,
                        createSessionDto.CreateCar.CreateCustomer.LastName);

                    if (!customerResult.IsSuccess)
                        return Result<int>.Failure(customerResult.Error);

                    customer = customerResult.Value;
                }

                var carResult = Car.TryCreate(
                    createSessionDto.CreateCar!.PlateNumber,
                    customer);

                if (!carResult.IsSuccess)
                    return Result<int>.Failure(carResult.Error);

                car = carResult.Value;
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


            await _unitOfWork.Sessions.AddAsync(session);
            _unitOfWork.Spots.Update(spot); 
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
        var activeSessionResult = await _unitOfWork.Sessions.GetAcitveDetailedByIdAsync(sessionId);

        if (!activeSessionResult.IsSuccess)
            return Result<SessionDetailedDto>.Failure($"Session with ID {sessionId} not found.");


        var session = activeSessionResult.Value;

        var endResult = session.EndSession();

        if (!endResult.IsSuccess)
            return Result<SessionDetailedDto>.Failure(endResult.Error);

        var spot = await _unitOfWork.Spots.GetByIdAsync(session.SpotId);

        if (spot is null)
            return Result<SessionDetailedDto>.Failure($"Spot with ID {session.SpotId} not found.");

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
        var sessionResult = await _unitOfWork.Sessions.GetDetailedByIdAsync(sessionId);

        if (!sessionResult.IsSuccess)
            return Result<SessionDetailedDto>.Failure($"Session with ID {sessionId} not found.");

        return Result<SessionDetailedDto>.Success(SessionMapper.ToDetailedDto(sessionResult.Value));
    }

    public async Task<List<SessionDetailedDto>> GetAllAsync(int pageNumber, int pageSize)
    {
        var list = await _unitOfWork.Sessions.GetAllDetailedAsync(pageNumber, pageSize);

        return list.Select(SessionMapper.ToDetailedDto).ToList();

    }
}

