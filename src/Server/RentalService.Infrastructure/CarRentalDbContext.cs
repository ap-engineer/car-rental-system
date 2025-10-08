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
            entity.HasKey(r => r.Id);
            entity.HasIndex(r => r.BookingNumber).IsUnique();
            entity.Property(r => r.BookingNumber).IsRequired().HasMaxLength(50);
            entity.Property(r => r.RegistrationNumber).IsRequired() .HasMaxLength(20);
            entity.Property(r => r.CustomerId).IsRequired().HasMaxLength(50);
            entity.Property(r => r.Category).IsRequired();
        });
    }
}