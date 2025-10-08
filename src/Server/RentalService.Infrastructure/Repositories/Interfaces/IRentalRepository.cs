using RentalService.Domain.Entities;

namespace RentalService.Infrastructure.Repositories.Interfaces;

public interface IRentalRepository
{
    Task<Rental?> GetAsync(string bookingNumber, CancellationToken ct = default);
    Task AddAsync(Rental rental, CancellationToken ct = default);
    Task UpdateAsync(Rental rental, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}