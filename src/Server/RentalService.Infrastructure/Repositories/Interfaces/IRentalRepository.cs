using RentalService.Domain.Entities;

namespace RentalService.Infrastructure.Repositories.Interfaces;

public interface IRentalRepository
{
    Task<Rental?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Rental?> GetByBookingNumberAsync(string bookingNumber, CancellationToken ct = default);
    Task<IReadOnlyList<Rental>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(Rental rental, CancellationToken ct = default);
    Task UpdateAsync(Rental rental, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}