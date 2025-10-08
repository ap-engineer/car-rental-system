using RentalService.Domain.Entities;

namespace RentalService.Domain.Services.Interfaces;

public interface IRentalPricingService
{
    decimal Calculate(CarCategory category, int days, long km);
}