using Microsoft.EntityFrameworkCore;
using RentalService.Domain.Entities;
using RentalService.Infrastructure.Repositories.Interfaces;

namespace RentalService.Infrastructure.Repositories.Implementation;

public sealed class RentalRepository(CarRentalDbContext db) : IRentalRepository
{
    public async Task<Rental?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await db.Rentals.FirstOrDefaultAsync(r => r.Id == id, ct);

    public async Task<Rental?> GetByBookingNumberAsync(string bookingNumber, CancellationToken ct = default)
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

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        try
        {
            await db.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("UNIQUE constraint failed") == true)
        {
            throw new InvalidOperationException("Booking number already exists.", ex);
        }
    }
}