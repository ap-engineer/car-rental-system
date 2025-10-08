using RentalService.Application.DTOs;

namespace RentalService.Application.Services.Interfaces;

public interface IRentalService
{
    void RegisterPickup(PickupRequest request);
    RentalResult RegisterReturn(ReturnRequest request);
}