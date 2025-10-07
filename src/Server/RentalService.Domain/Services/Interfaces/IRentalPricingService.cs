using RentalService.Domain.Entities;

namespace RentalService.Domain.Services.Interfaces;

public interface IRentalPricingService
{
    decimal CalculatePrice(CarCategory category, int numberOfDays, long kilometers, decimal baseDayRental, decimal baseKmPrice);
}