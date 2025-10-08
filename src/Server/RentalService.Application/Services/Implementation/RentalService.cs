using RentalService.Application.DTOs;
using RentalService.Application.Services.Interfaces;
using RentalService.Domain.Entities;
using RentalService.Domain.Services.Interfaces;
using RentalService.Infrastructure.Repositories.Interfaces;

namespace RentalService.Application.Services.Implementation;

public sealed class RentalService(IRentalPricingService pricing, IRentalRepository repo) : IRentalService
{
    //FUNCTION TO GENERATE BOOKING NUM
    // private static string GenerateBookingNumber()
    //     => $"B{DateTime.UtcNow.Ticks.ToString()[^6..]}";

    public async Task RegisterPickup(PickupRequest req)
    {
        // Check if booking number already exists
        var existingRental = await repo.GetByBookingNumberAsync(req.BookingNumber);
        if (existingRental != null)
        {
            throw new InvalidOperationException($"Booking number {req.BookingNumber} already exists.");
        }

        var category = Enum.Parse<CarCategory>(req.Category, true);
        var rental = new Rental(req.BookingNumber, req.RegistrationNumber, req.CustomerId, category);

        rental.RegisterPickup(req.PickupDate, req.PickupKm);

        await repo.AddAsync(rental);
        await repo.SaveChangesAsync();
    }

    public async Task<RentalResult> RegisterReturn(ReturnRequest req)
    {
        var rental = await repo.GetByBookingNumberAsync(req.BookingNumber)
                     ?? throw new KeyNotFoundException($"Booking {req.BookingNumber} not found.");

        rental.RegisterReturn(req.ReturnDate, req.ReturnKm);
        var price = pricing.Calculate(rental.Category, rental.NumberOfDays, rental.KilometersDriven);

        await repo.UpdateAsync(rental);
        await repo.SaveChangesAsync();

        return new RentalResult(
            rental.Id,
            rental.BookingNumber,
            price,
            rental.NumberOfDays,
            rental.KilometersDriven,
            rental.IsReturned,
            rental.PickupDate
        );
    }

    public async Task<RentalResult?> GetRentalById(Guid id)
    {
        var rental = await repo.GetByIdAsync(id);
        if (rental == null) return null;

        return new RentalResult(
            rental.Id,
            rental.BookingNumber,
            rental.IsReturned
                ? pricing.Calculate(rental.Category, rental.NumberOfDays, rental.KilometersDriven)
                : 0,
            rental.NumberOfDays,
            rental.KilometersDriven,
            rental.IsReturned,
            rental.PickupDate
        );
    }

    public async Task<IReadOnlyList<RentalResult>> GetAllRentals()
    {
        var rentals = await repo.GetAllAsync();
        return rentals.Select(r =>
                new RentalResult(
                    r.Id,
                    r.BookingNumber,
                    r.IsReturned
                        ? pricing.Calculate(r.Category, r.NumberOfDays, r.KilometersDriven)
                        : 0,
                    r.NumberOfDays,
                    r.KilometersDriven,
                    r.IsReturned,
                    r.PickupDate))
            .ToList();
    }
}