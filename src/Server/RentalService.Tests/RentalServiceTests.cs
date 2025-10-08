using RentalService.Application.DTOs;
using RentalService.Domain.Services.Implementation;
using Xunit;

namespace RentalService.Tests;

public class RentalServiceTests
{
    private readonly Application.Services.Implementation.RentalService _service = new(new RentalPricingService());

    [Fact]
    public void SmallCar_PriceCalculation_IsCorrect()
    {
        _service.RegisterPickup(new PickupRequest("B001", "ABC123", "1234567890", "SmallCar", DateTime.Today, 10000));
        var result = _service.RegisterReturn(new ReturnRequest("B001", DateTime.Today.AddDays(3), 10000));

        Assert.Equal(300, result.Price); // baseDayRental=100 * 3 days
    }

    [Fact]
    public void Truck_PriceCalculation_IsCorrect()
    {
        _service.RegisterPickup(new PickupRequest("B002", "XYZ789", "9876543210", "Truck", DateTime.Today, 10000));
        var result = _service.RegisterReturn(new ReturnRequest("B002", DateTime.Today.AddDays(2), 10200));

        Assert.Equal(100 * 2 * 1.5m + 2 * 200 * 1.5m, result.Price);
    }
}