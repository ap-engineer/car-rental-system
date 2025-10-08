
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RentalService.Infrastructure;

public sealed class CarRentalDbContextFactory : IDesignTimeDbContextFactory<CarRentalDbContext>
{
    public CarRentalDbContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../RentalService.Api");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<CarRentalDbContext>();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? "Data Source=car-rental.db";

        optionsBuilder.UseSqlite(connectionString);

        return new CarRentalDbContext(optionsBuilder.Options);
    }
}