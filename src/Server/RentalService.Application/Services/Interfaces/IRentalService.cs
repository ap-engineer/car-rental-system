using RentalService.Application.DTOs;

namespace RentalService.Application.Services.Interfaces;

public interface IRentalService
{
    Task<RentalResult> RegisterPickup(PickupRequest request);
    Task<RentalResult> RegisterReturn(ReturnRequest request);
    Task<RentalResult?> GetRentalById(Guid id);
    Task<IReadOnlyList<RentalResult>> GetAllRentals();
}