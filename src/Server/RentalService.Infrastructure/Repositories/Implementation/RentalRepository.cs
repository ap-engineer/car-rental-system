using Microsoft.EntityFrameworkCore;
using RentalService.Domain.Entities;
using RentalService.Infrastructure.Repositories.Interfaces;

namespace RentalService.Infrastructure.Repositories.Implementation;

public sealed class RentalRepository(CarRentalDbContext db) : IRentalRepository
{
    public async Task<Rental?> GetAsync(string bookingNumber, CancellationToken ct = default)
        => await db.Rentals.FirstOrDefaultAsync(r => r.BookingNumber == bookingNumber, ct);

    public async Task<IReadOnlyList<Rental>> GetAllAsync(CancellationToken ct = default)
        => await db.Rentals.AsNoTracking().ToListAsync(ct);

    public async Task AddAsync(Rental rental, CancellationToken ct = default)
        => await db.Rentals.AddAsync(rental, ct);

    public Task UpdateAsync(Rental rental, CancellationToken ct = default)
    {
        db.Rentals.Update(rental);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
        => db.SaveChangesAsync(ct);
}