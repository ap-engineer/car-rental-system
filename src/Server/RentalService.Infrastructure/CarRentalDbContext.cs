using Microsoft.EntityFrameworkCore;
using RentalService.Domain.Entities;

namespace RentalService.Infrastructure;

public sealed class CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : DbContext(options)
{
    public DbSet<Rental> Rentals => Set<Rental>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rental>(entity =>
        {
            entity.HasKey(r => r.BookingNumber);
            entity.Property(r => r.BookingNumber).IsRequired();
            entity.Property(r => r.RegistrationNumber).IsRequired();
            entity.Property(r => r.CustomerId).IsRequired();
            entity.Property(r => r.Category).IsRequired();
        });
    }
}