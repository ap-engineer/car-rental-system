using RentalService.Application.DTOs;
using RentalService.Application.Services.Interfaces;
using RentalService.Domain.Entities;
using RentalService.Domain.Services.Interfaces;

namespace RentalService.Application.Services.Implementation;

public sealed class RentalService(IRentalPricingService pricing) : IRentalService
{
    private readonly IRentalPricingService _pricing = pricing;
    private readonly Dictionary<string, Rental> _rentals = new();

    public void RegisterPickup(PickupRequest req)
    {
        var category = Enum.Parse<CarCategory>(req.Category, true);
        var rental = new Rental(req.BookingNumber, req.RegistrationNumber, req.CustomerId, category);
        rental.RegisterPickup(req.PickupDate, req.PickupKm);
        _rentals[req.BookingNumber] = rental;
    }

    public RentalResult RegisterReturn(ReturnRequest req)
    {
        if (!_rentals.TryGetValue(req.BookingNumber, out var rental))
            throw new KeyNotFoundException($"Booking {req.BookingNumber} not found.");

        rental.RegisterReturn(req.ReturnDate, req.ReturnKm);

        var price = _pricing.CalculatePrice(
            rental.Category,
            rental.NumberOfDays,
            rental.KilometersDriven,
            baseDayRental: 100, // configurable
            baseKmPrice: 2 // configurable
        );

        return new RentalResult(
            rental.BookingNumber,
            price,
            rental.NumberOfDays,
            rental.KilometersDriven);
    }
}