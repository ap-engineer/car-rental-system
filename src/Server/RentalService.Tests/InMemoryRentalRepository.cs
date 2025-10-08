using RentalService.Domain.Entities;
using RentalService.Infrastructure.Repositories.Interfaces;

namespace RentalService.Tests;

/// <summary>
/// Simple in-memory repository implementation for testing
/// </summary>
public class InMemoryRentalRepository : IRentalRepository
{
    private readonly List<Rental> _rentals = new();

    public Task<Rental?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var rental = _rentals.FirstOrDefault(r => r.Id == id);
        return Task.FromResult(rental);
    }

    public Task<Rental?> GetByBookingNumberAsync(string bookingNumber, CancellationToken ct = default)
    {
        var rental = _rentals.FirstOrDefault(r => r.BookingNumber == bookingNumber);
        return Task.FromResult(rental);
    }

    public Task<IReadOnlyList<Rental>> GetAllAsync(CancellationToken ct = default)
    {
        return Task.FromResult<IReadOnlyList<Rental>>(_rentals.AsReadOnly());
    }

    public Task AddAsync(Rental rental, CancellationToken ct = default)
    {
        _rentals.Add(rental);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Rental rental, CancellationToken ct = default)
    {
        // In-memory implementation doesn't need explicit update since objects are references
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
    {
        // In-memory implementation doesn't need explicit save
        return Task.CompletedTask;
    }

    /// <summary>
    /// Helper method to clear all data between tests
    /// </summary>
    public void Clear()
    {
        _rentals.Clear();
    }
}
