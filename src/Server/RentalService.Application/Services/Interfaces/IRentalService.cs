using RentalService.Application.DTOs;

namespace RentalService.Application.Services.Interfaces;

public interface IRentalService
{
    Task RegisterPickup(PickupRequest request);
    Task<RentalResult> RegisterReturn(ReturnRequest request);
    Task<IReadOnlyList<RentalResult>> GetAllRentals();
}